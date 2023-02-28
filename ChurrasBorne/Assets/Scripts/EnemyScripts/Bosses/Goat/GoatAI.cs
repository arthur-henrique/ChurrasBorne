using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class GoatAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Stomping,
        Dashing,
        RecoveringFromDash,
        SummoningSpikes,
        Idling,
        Dead       
    }
    private State state;

    public GameObject gameManager, dashDrills;
    public Transform player;
    private Vector2 dashTarget;

    public float chasingSpeed, dashingSpeed, meleeDistance, dashDistance, dashATKDistance, canDashDistance;

    public float startTimeBTWMeleeATKs, startDashRecoveryTime;
    private float timeBTWMeleeATKs, dashRecoveryTime, timeToDie, dashCooldown;

    private bool isDashing = false, isAlreadyDying = false, canDash = true;
    public bool isAlive = true;

    public float health;
    public static bool goat_boss_died = false;

    public Rigidbody2D rb;
    public Animator anim;

    public bool isP1, isP2;
    public Collider2D coll;

    public AudioSource audioSource;
    public AudioClip goat_roar;
    public AudioClip goat_stomp;
    public AudioClip goat_death;
    public AudioClip goat_dashattack;
    public AudioClip goat_hurt;


    private float knockbackDuration = 1.0f, knockbackPower = 30f;
    private bool canTakeDamage = true;

    public ParticleSystem bloodSpatter, stepDust, stompDust;
    private ParticleSystemRenderer psr;

    private float armor;
    private float playerDamage;
    public GateChecker gc;

    private void Awake()
    {
        psr = bloodSpatter.GetComponent<ParticleSystemRenderer>();
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        audioSource = GetComponent<AudioSource>();

        timeBTWMeleeATKs = .5f;
        dashCooldown = 0.25f; // Random.Range(3, 6);

        timeToDie = .1f;

        dashRecoveryTime = startDashRecoveryTime;

        HealthBar_Manager.instance.boss = this.gameObject;
        HealthBar_Manager.instance.refreshBoss = true;

        audioSource.PlayOneShot(goat_roar, audioSource.volume);
        armor = 1f;
    }

    void Update()
    {
        switch (state)
        {
            case State.Spawning:
                Flip();
                break;
                
            case State.Chasing:
                Flip();

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                anim.SetBool("Walk", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Idle", false);

                SwitchToStomping();
                SwitchToDashing();
                break;

            case State.Stomping:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if(timeBTWMeleeATKs <= 0)
                {
                    anim.SetTrigger("Stomp");
                    audioSource.PlayOneShot(goat_stomp, audioSource.volume);
                    timeBTWMeleeATKs = startTimeBTWMeleeATKs;
                }
                else
                {
                    timeBTWMeleeATKs -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToDashing();
                break;

            case State.Dashing:
                isDashing = true;   

                DashFlip();

                transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashingSpeed * Time.deltaTime);

                anim.SetBool("Dash", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                if (Vector2.Distance(transform.position, player.position) <= dashATKDistance && isDashing)
                {
                    isDashing = false;
                    canDash = false;

                    anim.SetTrigger("DashATK");
                    audioSource.PlayOneShot(goat_dashattack, audioSource.volume);
                    state = State.RecoveringFromDash;
                }
                else if (transform.position.x == dashTarget.x && transform.position.y == dashTarget.y && isDashing)
                {
                    isDashing = false;
                    canDash = false;

                    state = State.RecoveringFromDash;
                }
                break;
                
            case State.RecoveringFromDash:
                rb.velocity = Vector2.zero;

                Debug.Log("rawr x3");

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if (dashRecoveryTime <= 0)
                {
                    SwitchToStomping();
                    SwitchToChasing();
                    SwitchToDashing();

                    dashRecoveryTime = startDashRecoveryTime;
                }
                else
                {
                    dashRecoveryTime -= Time.deltaTime;
                }
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                isAlive = false;
                isAlreadyDying = true;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);
                gc.SetFerramentasPos(gameObject.transform.position);

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                if (timeToDie <= 0)
                {
                    anim.SetTrigger("Die");
                    audioSource.PlayOneShot(goat_death, audioSource.volume);
                    goat_boss_died = true;
                    timeToDie = 1000;
                }
                else
                {
                    timeToDie -= Time.deltaTime;
                }

                if(isP1)
                {
                    GameManager.instance.SetHasCleared(0, true);
                    FaseUmTriggerController.Instance.SecondGateOpen();
                    coll.enabled = true;
                    coll.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (isP2)
                {
                    gc.isTheBossDead = true;
                    //GameManager.instance.SetHasCleared(1, true);
                    //FaseUmTriggerController.Instance.SecondGateOpen();
                }
                
                break;

            case State.Idling:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);
                break;
        }

        if (!gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Idling;
        }

        if (!isDashing)
        {
            dashTarget = player.transform.position;

            Vector3 fator = player.position - transform.position;

            dashTarget.x = player.position.x + fator.x * 2;

            dashTarget.y = player.position.y + fator.y * 2;
        }

        if(!canDash)
        {
            dashCooldown -= Time.deltaTime;
        }
        if(dashCooldown <= 0)
        {
            canDash = true;
            dashCooldown = Random.Range(3, 6);
        }
    }

    void SwitchToChasing()
    {
        if (Vector2.Distance(transform.position, player.position) > dashDistance && health > 0)
        {
            state = State.Chasing;
        }
        if (Vector2.Distance(transform.position, player.position) <= canDashDistance && Vector2.Distance(transform.position, player.position) > meleeDistance && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToDashing()
    {
        if (Vector2.Distance(transform.position, player.position) <= dashDistance && Vector2.Distance(transform.position, player.position) > canDashDistance && health > 0 && canDash)
        {
            state = State.Dashing;
        }
    }
    void SwitchToStomping()
    {
        if (Vector2.Distance(transform.position, player.position) <= meleeDistance && health > 0)
        {
            state = State.Stomping;
        }
    }
    void SwitchToDead()
    {
        if (health <= 0 && isAlive)
        {
            state = State.Dead;
            GameManager.instance.SwitchToDefaultCam();
        }
    }

    void BeginCombat()
    {
        SwitchToChasing();
        SwitchToDashing();
        SwitchToStomping();
    }

    void StompDamage()
    {
        if(Vector2.Distance(transform.position, player.position) <= meleeDistance)
        {
            //StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
            GameManager.instance.TakeDamage(30);
        }
    }

    void DashDamage()
    {
        if (Vector2.Distance(transform.position, player.position) <= dashATKDistance)
        {
            StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
            GameManager.instance.TakeDamage(70);
        }

        Instantiate(dashDrills, transform.position, Quaternion.identity);
    }

    void Flip()
    {
        if (player.position.x < transform.position.x && !isDashing)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            psr.flip = new Vector3(0, 0, 0);
        }
        else if (player.position.x > transform.position.x && !isDashing)
        {
            transform.localScale = new Vector3(1, 1, 1);
            psr.flip = new Vector3(1, 0, 0);
        }
    }
    void DashFlip()
    {
        if (dashTarget.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            psr.flip = new Vector3(0, 0, 0);
        }
        else if (dashTarget.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            psr.flip = new Vector3(1, 0, 0);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);    
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            StartCoroutine(CanTakeDamageCD());
            gameObject.GetComponent<ColorChanger>().ChangeColor();
            DrawBlood();
            if (GameManager.instance.GetMeat() >= 0)
            {
                playerDamage = GameManager.instance.GetDamage() * (1 + GameManager.instance.GetMeat() / 6.2f) / armor;
            }
            else
            {
                playerDamage = GameManager.instance.GetDamage() / armor;
            }
            health -= playerDamage;
            audioSource.PlayOneShot(goat_hurt, audioSource.volume);
            if (!isAlreadyDying)
            {
                SwitchToDead();
            }
        }
    }

    private void DrawBlood()
    {
        bloodSpatter.gameObject.SetActive(true);
        bloodSpatter.Stop();
        bloodSpatter.Play();
    }

    private void PlayStompDust()
    {
        stompDust.gameObject.SetActive(true);
        stompDust.Stop();
        stompDust.Play();
    }

    private void PlayStepDust()
    {
        stepDust.gameObject.SetActive(true);
        stepDust.Stop();
        stepDust.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }

        if (isDashing)
        {
            if (collision.gameObject.CompareTag("PAREDE"))
            {
                isDashing = false;

                state = State.RecoveringFromDash;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    private IEnumerator CanTakeDamageCD()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    }
}
