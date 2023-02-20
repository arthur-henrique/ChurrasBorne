using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
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
    float canAttackChecker = 0.9f;
    public float x, y;
    public float rollSpeed, attackTimer;
    public float attackAnimCd, healingAnimCd;
    public float healsLeft;
    private readonly int amountToHeal = 50;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private static Animator anim;
    public Animator reflAnim;
    private Vector3 rollDirection;
    public Vector3 lastMovedDirection;
    private Vector2 direcao;
    private Vector2 moveVelocity;
    bool attackPressed = false;
    bool healingPressed = false;
    bool takingDamage = false;
    bool canAttack = true;
    bool isOnFaseDois = false;
    private static State state;
    public static PlayerController pc;

    public ParticleSystem walkParticles, snowWalkParticles, dashParticlesOne, dashParticlesTwo, snowDashParticlesOne, snowDashParticlesTwo;
    private ParticleSystem.EmissionModule particleEmission, snowParticleEmission;
    public float particleRate;

    private AudioSource audioSource;
    public AudioClip player_dash;
    public AudioClip player_punch;
    public AudioClip player_swing;
    public AudioClip player_eat;
    public AudioClip player_hurt;

    public bool isOnIce, isOnWeb, isOnBossWeb;
    // Start is called before the first frame update
    private void Awake()
    {
        state = State.Normal;
        pc = new PlayerController();
        instance = this;
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
        audioSource = GetComponent<AudioSource>();
        isOnIce = false;
        isOnWeb = false;
        isOnBossWeb = false;
        speed = 10f;

        particleEmission = walkParticles.emission;
        snowParticleEmission = snowWalkParticles.emission;
    }
    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case State.Normal:
                if (!GameManager.isInDialog)
                {
                    x = pc.Movimento.LesteOeste.ReadValue<float>();
                    y = pc.Movimento.NorteSul.ReadValue<float>();

                    if (x != 0 || y != 0)
                    {
                        particleEmission.rateOverTime = particleRate;
                        snowParticleEmission.rateOverTime = particleRate;
                    }
                    else
                    {
                        particleEmission.rateOverTime = 0f;
                        snowParticleEmission.rateOverTime = 0f;
                    }

                    if (pc.Movimento.Rolar.WasPressedThisFrame())
                    {
                        if (Dash_Manager.dash_fill_global >= 60)
                        {
                            rollDirection = lastMovedDirection;
                            rollSpeed = 70f;
                            state = State.Rolling;
                            anim.SetTrigger("isRolling");
                            reflAnim.SetTrigger("isRolling");
                            //print("Rolei");
                            StartCoroutine(DustWait());
                            audioSource.PlayOneShot(player_dash, audioSource.volume);
                            Dash_Manager.dash_fill_global -= 60;
                            Dash_Manager.dash_light_global = 0;
                        }
                    }

                    if (pc.Movimento.Attack.WasPressedThisFrame() && canAttack)
                    {
                        canAttack = false;
                        state = State.Attacking;
                        anim.SetTrigger("isAttacking");
                        reflAnim.SetTrigger("isAttacking");
                        if (anim.GetBool("isHoldingSword") == true)
                        {
                            audioSource.PlayOneShot(player_swing, audioSource.volume);
                        } else
                        {
                            audioSource.PlayOneShot(player_punch, audioSource.volume);
                        }
                        
                    }

                    if (pc.Movimento.Curar.WasPressedThisFrame() && healsLeft >= 0)
                    {
                        healingAnimCd = 1f;
                        state = State.Healing;
                        anim.SetTrigger("isHealing");
                        reflAnim.SetTrigger("isHealing");
                        audioSource.PlayOneShot(player_eat, audioSource.volume);
                    }
                } else
                {
                    direcao = new Vector2(0, 0);
                    x = 0;
                    y = 0;
                }
                
                direcao = new Vector2(x, y);
                direcao.Normalize();
                if (x != 0 || y != 0)
                {
                    lastMovedDirection = direcao;
                    anim.SetFloat("lastMoveX", lastMovedDirection.x);
                    anim.SetFloat("lastMoveY", lastMovedDirection.y);
                    reflAnim.SetFloat("lastMoveX", lastMovedDirection.x);
                    reflAnim.SetFloat("lastMoveY", lastMovedDirection.y);
                }
                healsLeft = GameManager.instance.GetHeals();
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
                {
                    state = State.Normal;
                    PostProcessingControl.Instance.TurnOffLens();
                    PlayDashParticlesEnd();
                }
                if (rollSpeed < rollSpeedMinimun && attackPressed)
                {
                    state = State.Attacking;
                    anim.SetTrigger("isAttacking");
                    reflAnim.SetTrigger("isAttacking");
                    PostProcessingControl.Instance.TurnOffLens();
                    PlayDashParticlesEnd();

                }
                if (rollSpeed < rollSpeedMinimun && healingPressed)
                {
                    state = State.Healing;
                    anim.SetTrigger("isHealing");
                    reflAnim.SetTrigger("isHealing");
                    PostProcessingControl.Instance.TurnOffLens();
                    PlayDashParticlesEnd();

                }
                break;
            case State.Attacking:
                anim.SetBool("attackIsPlaying", true);
                reflAnim.SetBool("attackIsPlaying", true);
                StartCoroutine(ReturnToNormal());
                
                attackPressed = false;
                break;
            case State.Healing:
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
                    reflAnim.SetFloat("lastMoveX", lastMovedDirection.x);
                    reflAnim.SetFloat("lastMoveY", lastMovedDirection.y);
                }
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
                    reflAnim.SetFloat("lastMoveX", lastMovedDirection.x);
                    reflAnim.SetFloat("lastMoveY", lastMovedDirection.y);
                }
                if (takingDamage)
                {
                    timer = GameManager.instance.GetDamagetime();
                    print(timer);
                    audioSource.PlayOneShot(player_hurt, audioSource.volume);
                    takingDamage = false;
                }
                timer -= Time.deltaTime;
                if (pc.Movimento.Curar.WasPressedThisFrame() && healsLeft >= 0)
                {
                    healingAnimCd = 1f;
                    healingPressed = true;
                    attackPressed = false;
                }
                if (timer <= 0f)
                {
                    if (healingPressed)
                    {
                        state = State.Healing;
                        anim.SetTrigger("isHealing");
                        reflAnim.SetTrigger("isHealing");
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
                particleEmission.rateOverTime = 0f;
                snowParticleEmission.rateOverTime = 0f;
                anim.SetFloat("moveX", 0);
                anim.SetFloat("moveY", 0);
                reflAnim.SetFloat("moveX", 0);
                reflAnim.SetFloat("moveY", 0);
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                moveVelocity = direcao * speed;
                if(isOnIce && !isOnWeb)
                {
                    rb.AddForce(moveVelocity * 0.7f, ForceMode2D.Force);
                }
                else if(isOnWeb && !isOnIce)
                {
                    moveVelocity *= 0.6f;
                    rb.velocity = moveVelocity;
                }
                else if(isOnIce && isOnWeb)
                    {
                        rb.AddForce(moveVelocity * 0.5f, ForceMode2D.Force);
                    }
                else if (isOnBossWeb)
                {
                    moveVelocity *= 0.5f;
                    rb.velocity = moveVelocity;
                }
                else
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
                reflAnim.SetFloat("moveX", rb.velocity.x);
                reflAnim.SetFloat("moveY", rb.velocity.y);


                if (canAttackChecker > 0 && !canAttack)
                    canAttackChecker -= Time.deltaTime;
                else if (canAttackChecker > 0 && canAttack)
                    canAttackChecker = 0.9f;
                else if (canAttackChecker <= 0)
                {
                    canAttack = true;
                    canAttackChecker = 0.9f;
                }
                break;
            case State.Rolling:
                if (isOnIce)
                {
                    rb.velocity = rollDirection * (rollSpeed * 1.3f);
                }
                else if (isOnWeb)
                {
                    rb.velocity = rollDirection * (rollSpeed * 0.6f);
                }
                else if (isOnBossWeb)
                {
                    rb.velocity = rollDirection * (rollSpeed * 0.5f);
                }
                else
                    rb.velocity = rollDirection * rollSpeed;
                break;
            case State.Attacking:
                if(!isOnIce)
                    rb.velocity = Vector2.zero;
                
                break;
            case State.Healing:
                moveVelocity = 0.8f * speed * direcao;
                if (isOnIce)
                {
                    rb.AddForce(moveVelocity, ForceMode2D.Force);
                }
                else if (isOnWeb)
                {
                    moveVelocity *= 0.6f;
                    rb.velocity = moveVelocity;
                }
                else
                    rb.velocity = moveVelocity;
                if (rb.velocity.x < 0)
                {
                    sr.flipX = true;
                }
                else if (rb.velocity.x > 1f)
                {
                    sr.flipX = false;
                }
                break;
            case State.TakingDamage:
                moveVelocity = 0.8f * speed * direcao;
                if (isOnIce)
                {
                    rb.AddForce(moveVelocity, ForceMode2D.Force);
                }
                else if (isOnWeb)
                {
                    moveVelocity *= 0.6f;
                    rb.velocity = moveVelocity;
                }
                else if (isOnBossWeb)
                {
                    moveVelocity *= 0.5f;
                    rb.velocity = moveVelocity;
                }
                else
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
                reflAnim.SetFloat("moveX", rb.velocity.x);
                reflAnim.SetFloat("moveY", rb.velocity.y);
                break;
            case State.Dead:
                rb.velocity = Vector2.zero;
                break;
        }
    }

    public static void DisableControl()
    {
        pc.Movimento.Disable();
    }

    public static void EnableControl()
    {
        pc.Movimento.Enable();
    }
    public static void SetDamageState()
    {
        state = State.TakingDamage;
    }
    public static void SetDead()
    {
        state = State.Dead;
    }
    public static void SetStateAlive()
    {
        state = State.Normal;
    }

    public void CantAttack()
    {
        StartCoroutine(StupidAttackCD());
        anim.SetBool("attackIsPlaying", false);
        reflAnim.SetBool("attackIsPlaying", false);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("GELO"))
        {
            isOnIce = true;
        }
        else if(other.CompareTag("TEIA"))
        {
            isOnWeb = true;
        }
        else if (other.CompareTag("TEIABOSS"))
        {
            isOnBossWeb = true;
        }
        else if (other.CompareTag("CLEANSER"))
        {
            isOnIce = false;
            isOnWeb = false;
            isOnBossWeb = false;
            isOnFaseDois = false;
            GameManager.instance.SwitchToDefaultCam();
            ExitSnowParticles();
        }
        else if (other.CompareTag("CLEANSERTINY"))
        {
            isOnIce = false;
            isOnWeb = false;
            isOnBossWeb = false;
        }
        //else if (other.CompareTag("Fish"))
        //{
        //    gameObject.GetComponent<PlayerTempPowerUps>().enabled = true;
        //}
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("GELO"))
        {
            isOnIce = true;
        }
        else if (other.CompareTag("TEIA"))
        {
            isOnWeb = true;
        }
        else if (other.CompareTag("TEIABOSS"))
        {
            isOnBossWeb = true;
        }
        else if (other.CompareTag("CLEANSER"))
        {
            isOnIce = false;
            isOnWeb = false;
            isOnBossWeb = false;
            isOnFaseDois = false;
            GameManager.instance.SwitchToDefaultCam();
            ExitSnowParticles();
        }
        else if (other.CompareTag("Fish"))
        {
            gameObject.GetComponent<PlayerTempPowerUps>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("GELO"))
        {
            isOnIce = false;
        }
        else if (other.CompareTag("TEIA"))
        {
            isOnWeb = false;
        }
        else if (other.CompareTag("TEIABOSS"))
        {
            isOnBossWeb = false;
        }
        else if (other.CompareTag("Fish"))
        {
            other.enabled = false;
        }
    }

    private void PlayDashParticlesStart()
    {
        if (!isOnFaseDois)
        {
            dashParticlesOne.gameObject.SetActive(true);
            dashParticlesOne.Stop();
            dashParticlesOne.Play();
        }
        else
        {
            snowDashParticlesOne.gameObject.SetActive(true);
            snowDashParticlesOne.Stop();
            snowDashParticlesOne.Play();
        }
    }
    private void PlayDashParticlesEnd()
    {
        if (!isOnFaseDois)
        {
            dashParticlesTwo.gameObject.SetActive(true);
            dashParticlesTwo.Stop();
            dashParticlesTwo.Play();
        }
        else
        {
            snowDashParticlesTwo.gameObject.SetActive(true);
            snowDashParticlesTwo.Stop();
            snowDashParticlesTwo.Play();
        }
    }

    public void SetFaseDois()
    {
        isOnFaseDois = true;
    }

    public void EnterSnowParticles()
    {
        walkParticles.gameObject.SetActive(false);
        snowWalkParticles.gameObject.SetActive(true);
    }
    public void ExitSnowParticles()
    {
        walkParticles.gameObject.SetActive(true);
        snowWalkParticles.gameObject.SetActive(false);
    }

    IEnumerator StupidAttackCD()
    {
        yield return new WaitForSeconds(0.035f);
        canAttackChecker = 0.9f;
        canAttack = true;
    }
    IEnumerator ReturnToNormal()
    {
        yield return new WaitForSeconds(0.15f);
        if(state == State.Attacking && state != State.Dead)
            state = State.Normal;
    }


    public IEnumerator Knockback(float kbDuration, float kbPower, Transform obj)
    { 
        float timer = 0;
        while (kbDuration > timer)
        {
            timer += Time.deltaTime;
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1.7f,
                this.transform.position.z);
            Vector2 direction = (obj.transform.position - pos).normalized;
            if (isOnIce)
            {
                rb.AddForce(-direction * kbPower / 5f);
            }
            else
                rb.AddForce(-direction * kbPower);
        }
        yield return 0;
        
    }
    private IEnumerator DustWait()
    {
        yield return new WaitForSeconds(0.15f);
        PlayDashParticlesStart();
    }

    //private IEnumerator IsOnWeb()
    //{

    //}

}