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
    }
    [SerializeField]

    public float speed;
    private float x, y;
    public float rollSpeed, attackTimer;
    public float attackAnimCd;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Vector3 rollDirection;
    public Vector3 lastMovedDirection;
    private Vector2 direcao;
    private Vector2 moveVelocity;

    private bool isDashing;
    bool attackPressed = false;
    private State state;


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
                if (x != 0 || y !=0)
                {
                    lastMovedDirection = direcao;
                    anim.SetFloat("lastMoveX", lastMovedDirection.x);
                    anim.SetFloat("lastMoveY", lastMovedDirection.y);
                }
                //if (Input.GetKeyDown(KeyCode.Q))
                //{
                //    isDashing = true;
                //}
                if (pc.Movimento.Rolar.WasPressedThisFrame())
                {
                    rollDirection = lastMovedDirection;
                    rollSpeed = 70f;
                    state = State.Rolling;
                    anim.SetTrigger("isRolling");
                }
                if (pc.Movimento.Attack.WasPressedThisFrame())
                {
                    attackAnimCd = 0.3f;
                    state = State.Attacking;
                    anim.SetTrigger("isAttacking");
                }
                break;
            case State.Rolling:
                float rollSpeedMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedMultiplier * Time.deltaTime;

                if (pc.Movimento.Attack.WasPressedThisFrame())
                {
                    attackAnimCd = 0.3f;
                    attackPressed = true;
                }

                float rollSpeedMinimun = 10f;
                if (rollSpeed < rollSpeedMinimun)
                    state = State.Normal;
                if (rollSpeed < rollSpeedMinimun && attackPressed)
                {
                    state = State.Attacking;
                    anim.SetTrigger("isAttacking");
                }
                break;
            case State.Attacking:
                attackAnimCd -= Time.deltaTime;
                if (attackAnimCd <= 0f)
                {
                    state = State.Normal;
                    attackPressed = false;
                }
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
        }

    }
}
