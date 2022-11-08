using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private enum State
    {
        Normal,
        Rolling,
        Attacking,
        Healing,
        TakingDamage,
        Dead
    }
    public float speed;
    float timer;
    private float x, y;
    public float rollSpeed, attackTimer;
    public float attackAnimCd, healingAnimCd;
    public float healsLeft;
    private int amountToHeal = 20;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private static Animator anim;
    private Vector3 rollDirection;
    public Vector3 lastMovedDirection;
    private Vector2 direcao;
    private Vector2 moveVelocity;
    bool attackPressed = false;
    bool healingPressed = false;
    bool takingDamage = false;
    bool canAttack = true;
    private static State state;
    PlayerController pc;
    // Start is called before the first frame update
    private void Awake()
    {
        state = State.Normal;
        pc = new PlayerController();
    }
    private void OnEnable()
    {
        pc.Enable();
    }
    private void OnDisable()
    {
        pc.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                x = pc.Movimento.LesteOeste.ReadValue<float>();
                y = pc.Movimento.NorteSul.ReadValue<float>();
                direcao = new Vector2(x, y);
                direcao.Normalize();
                if (x != 0 || y != 0)
                {
                    lastMovedDirection = direcao;
                    anim.SetFloat("lastMoveX", lastMovedDirection.x);
                    anim.SetFloat("lastMoveY", lastMovedDirection.y);
                }
                healsLeft = GameManager.instance.GetHeals();
                if (pc.Movimento.Rolar.WasPressedThisFrame())
                {
                    if (Dash_Manager.dash_fill_global >= 60)
                    {
                        rollDirection = lastMovedDirection;
                        rollSpeed = 70f;
                        state = State.Rolling;
                        anim.SetTrigger("isRolling");
                        Dash_Manager.dash_fill_global -= 60;
                        Dash_Manager.dash_light_global = 0;
                    }
                }
                if (pc.Movimento.Attack.WasPressedThisFrame() && canAttack)
                {
                    canAttack = false;
                    state = State.Attacking;
                    anim.SetTrigger("isAttacking");
                }
                if (pc.Movimento.Curar.WasPressedThisFrame() && healsLeft >= 0)
                {
                    healingAnimCd = 1f;
                    state = State.Healing;
                    anim.SetTrigger("isHealing");
                }
                break;
            case State.Rolling:
                float rollSpeedMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedMultiplier * Time.deltaTime;
                GameManager.instance.RollInvuln();
                if (pc.Movimento.Attack.WasPressedThisFrame() && canAttack)
                {
                    canAttack = false;
                    attackPressed = true;
                }
                if (pc.Movimento.Curar.WasPressedThisFrame() && healsLeft >= 0)
                {
                    healingAnimCd = 1f;
                    healingPressed = true;
                    attackPressed = false;
                }
                float rollSpeedMinimun = 10f;
                if (rollSpeed < rollSpeedMinimun)
                    state = State.Normal;
                if (rollSpeed < rollSpeedMinimun && attackPressed)
                {
                    state = State.Attacking;
                    anim.SetTrigger("isAttacking");
                }
                if (rollSpeed < rollSpeedMinimun && healingPressed)
                {
                    state = State.Healing;
                    anim.SetTrigger("isHealing");
                }
                break;
            case State.Attacking:
                anim.SetBool("attackIsPlaying", true);
                state = State.Normal;
                anim.SetBool("attackIsPlaying", false);
                attackPressed = false;
                break;
            case State.Healing:
                StartCoroutine(HealthBar_Manager.Alpha_Control_Enable());
                healingAnimCd -= Time.deltaTime;
                if (healingAnimCd <= 0f)
                {
                    GameManager.instance.HealPlayer(amountToHeal);
                    state = State.Normal;
                    healingPressed = false;
                }
                break;
            case State.TakingDamage:
                StartCoroutine(HealthBar_Manager.Alpha_Control_Enable());
                x = pc.Movimento.LesteOeste.ReadValue<float>();
                y = pc.Movimento.NorteSul.ReadValue<float>();
                direcao = new Vector2(x, y);
                direcao.Normalize();
                if (x != 0 || y != 0)
                {
                    lastMovedDirection = direcao;
                    anim.SetFloat("lastMoveX", lastMovedDirection.x);
                    anim.SetFloat("lastMoveY", lastMovedDirection.y);
                }
                if (takingDamage)
                {
                    timer = GameManager.instance.GetDamagetime();
                    takingDamage = false;
                }
                timer -= Time.deltaTime;
                if (pc.Movimento.Attack.WasPressedThisFrame())
                {
                    attackPressed = true;
                }
                if (pc.Movimento.Curar.WasPressedThisFrame() && healsLeft >= 0)
                {
                    healingAnimCd = 1f;
                    healingPressed = true;
                    attackPressed = false;
                }
                if (timer <= 0f)
                {
                    if (attackPressed)
                    {
                        state = State.Attacking;
                        anim.SetTrigger("isAttacking");
                        takingDamage = true;
                    }
                    else if (healingPressed)
                    {
                        state = State.Healing;
                        anim.SetTrigger("isHealing");
                        takingDamage = true;
                    }
                    else
                    {
                        state = State.Normal;
                        takingDamage = true;
                    }
                }
                break;
            case State.Dead:
                anim.SetFloat("moveX", 0);
                anim.SetFloat("moveY", 0);
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                moveVelocity = direcao * speed;
                rb.velocity = moveVelocity;
                if (rb.velocity.x < 0)
                {
                    sr.flipX = true;
                }
                else if (rb.velocity.x > 1f)
                {
                    sr.flipX = false;
                }
                anim.SetFloat("moveX", rb.velocity.x);
                anim.SetFloat("moveY", rb.velocity.y);
                break;
            case State.Rolling:
                rb.velocity = rollDirection * rollSpeed;
                break;
            case State.Attacking:
                rb.velocity = Vector2.zero;
                break;
            case State.Healing:
                rb.velocity = Vector2.zero;
                break;
            case State.TakingDamage:
                moveVelocity = direcao * speed * 0.2f;
                rb.velocity = moveVelocity;
                if (rb.velocity.x < 0)
                {
                    sr.flipX = true;
                }
                else if (rb.velocity.x > 1f)
                {
                    sr.flipX = false;
                }
                anim.SetFloat("moveX", rb.velocity.x);
                anim.SetFloat("moveY", rb.velocity.y);
                break;
            case State.Dead:
                rb.velocity = Vector2.zero;
                break;
        }
    }
    public static void SetDamageState()
    {
        state = State.TakingDamage;
    }
    public static void SetDead()
    {
        state = State.Dead;
    }

    public void CantAttack()
    {
        canAttack = true;
    }
}