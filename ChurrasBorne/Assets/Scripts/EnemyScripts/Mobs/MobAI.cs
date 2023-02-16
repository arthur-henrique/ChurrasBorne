using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Idling,
        GazingIntoTheNightSky,
        Chasing,
        Attacking,
        Shooting,
        Stunned,
        Dashing,
        RecoveringFromDash,
        Dead
    }
    private State state, lastState;

    public Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer sr;

    public Transform player;
    private Vector3 target;
    public Vector3 dashTarget;

    public GameObject projectile;

    public GameObject gameManager;

    private AudioSource audioSource;
    public AudioClip monster_death;
    public AudioClip monster_hurt;
    public AudioClip monster_punch;
    public AudioClip monster_spit;

    public float agroDistance, meleeDistance, canDashDistance, dashMeleeDistance, chaseDistance, chasingSpeed, dashingSpeed, startTimeBTWAttacks, startTimeBTWShots, startStunTime, startDashRecoveryTime;
    private float TimeBTWAttacks, timeBTWShots, stunTime, dashRecoveryTime;

    

    public bool isASpitter,
        isADasher,
        isASpider,
        isAPoisonSpider,
        isATebas,
        isAGeletebas,
        isAShatebas,
        isAGigantebas;
    private bool canDash = false, isDashing = false, canBeStunned = true;

    public bool isOnTutorial, isOnFaseUm, isOnFaseDois, isOnFaseTres;

    private float yOffset = 1.7f;
    private float knockbackDuration = 1f;
    private float knockbackPower = 25f;

    private bool canBeKbed = true;
    private bool canTakeDamage = true;

    public ParticleSystem bloodSpatter, stepDust, stompDust;
    private ParticleSystemRenderer psr;

    // Controle de dano:
    private float damage, armor, health;
    private float playerDamage;
    private float stunCD;

    public GameObject spriteCenter;
    
    private void Awake()
    {
        psr = bloodSpatter.GetComponent<ParticleSystemRenderer>();
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        sr = gameObject.GetComponent<SpriteRenderer>();
        target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        audioSource = GetComponent<AudioSource>();

        TimeBTWAttacks = 0.1f;

        timeBTWShots = startTimeBTWShots;

        stunTime = startStunTime;

        stunCD = Random.Range(3, 5);

        dashRecoveryTime = startDashRecoveryTime;
        
        if(isASpitter)
        {
            health = 75f;
            damage = 15f;
            armor = 1f;
        }
        else if(isADasher)
        {
            health = 50f;
            damage = 30f;
            armor = 1f;
        }
        else if (isASpider)
        {
            health = 60f;
            damage = 5f;
            armor = 1f;
            isAPoisonSpider = true;
            //int poisonChance = Random.Range(0, 4);
            //if(poisonChance > 2)
            //{
            //    isAPoisonSpider = true;
            //}

            //if (isAPoisonSpider)
            //{
            //    sr.color = new Color(0.1792207f, 0.5943396f, 0.1031684f, 1f);
            //}
        }
        else if(isATebas)
        {
            health = 100f;
            damage = 10f;
            armor = 1f;
        }
        else if(isAGeletebas)
        {
            health = 100f;
            damage = 10f;
            armor = 1.25f;
        }
        else if(isAShatebas)
        {
            health = 100f;
            damage = 25f;
            armor = 0.75f;
        }
        else if(isAGigantebas)
        {
            health = 200f;
            damage = 25f;
            armor = 1.25f;
        }
    }

    void Update()
    {
        switch(state)
        {
            case State.Spawning:
                Flip();
                break;
            
            case State.Idling:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                TimeBTWAttacks -= Time.deltaTime;

                SwitchToChasing();
                SwitchToShooting();
                SwitchToDead();
                break;

            case State.Chasing:
                Flip();
                Vector2 direcao = (target - this.transform.position).normalized;
                rb.velocity = direcao * chasingSpeed;
                
                //transform.position = Vector2.MoveTowards(transform.position, target, chasingSpeed * Time.deltaTime);

                anim.SetBool("Walk", true);
                anim.SetBool("Idle", false);
                TimeBTWAttacks -= Time.deltaTime;

                SwitchToIdling();
                SwitchToAttacking();
                SwitchToShooting();
                SwitchToDashing();
                SwitchToDead();
                break;

            case State.Attacking:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (TimeBTWAttacks <= 0)
                {
                    anim.SetTrigger("Melee");
                    //audioSource.PlayOneShot(monster_punch, audioSource.volume);
                    TimeBTWAttacks = startTimeBTWAttacks;
                }
                else
                {
                    TimeBTWAttacks -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToDead();
                break;

            case State.Shooting:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (timeBTWShots <= 0)
                {
                    anim.SetTrigger("Ranged");
                    audioSource.PlayOneShot(monster_spit, audioSource.volume);
                    timeBTWShots = startTimeBTWShots;
                }
                else
                {
                    timeBTWShots -= Time.deltaTime; 
                }

                SwitchToIdling();
                SwitchToChasing();
                SwitchToDead();
                TimeBTWAttacks -= Time.deltaTime;
                break;

            case State.Dashing:
                transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashingSpeed * Time.deltaTime);

                canDash = false;
                isDashing = true;

                anim.SetBool("Dash", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);
                TimeBTWAttacks -= Time.deltaTime;

                if (dashTarget.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (dashTarget.x > transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                if (Vector2.Distance(transform.position, target) <= dashMeleeDistance && isDashing)
                {
                    anim.SetTrigger("DashMelee");

                    SwitchToRecoveringFromDash();
                }
                else if (transform.position.x == dashTarget.x && transform.position.y == dashTarget.y && isDashing)
                {
                    SwitchToRecoveringFromDash();
                    
                    isDashing = false;
                }
                break;

            case State.RecoveringFromDash:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if (dashRecoveryTime <= 0)
                {
                    SwitchToIdling();
                    SwitchToChasing();
                    SwitchToAttacking();

                    dashRecoveryTime = startDashRecoveryTime;
                }
                else
                {
                    dashRecoveryTime -= Time.deltaTime;
                }
                SwitchToDead();
                break;

            case State.Stunned:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                canBeStunned = false;

                if (stunTime <= 0)
                {
                    stunTime = startStunTime;
                    
                    SwitchToChasing();
                    SwitchToIdling();
                    SwitchToAttacking();
                    SwitchToShooting();
                    SwitchToDead();
                }
                else
                {
                    //print(stunTime);
                    stunTime -= Time.deltaTime; 
                }
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                anim.SetTrigger("Die");
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                if (isOnTutorial)
                {
                    EnemyControlTutorial.Instance.KilledEnemy(gameObject);
                }
                else if (isOnFaseUm)
                {
                    EnemyControl.Instance.KilledEnemy(gameObject);
                }
                else if (isOnFaseDois)
                {
                    EnemyControlFaseDois.Instance.KilledEnemy(gameObject);
                }
                else if(isOnFaseTres)
                {
                    EnemyControllerFaseTres.Instance.KilledEnemy(gameObject);
                }
                break;
            case State.GazingIntoTheNightSky:
                rb.velocity = Vector2.zero;
                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                break;
        }

        if(!canBeStunned)
        {
            stunCD -= Time.deltaTime;

            if(stunCD <= 0)
            {
                stunCD = Random.Range(3, 5);
                canBeStunned = true;
            }
        }

        //DASH
        if(isDashing == false)
        {
            dashTarget = target;

            Vector3 fator = target - transform.position;

            dashTarget.x = target.x + fator.x * 2;

            dashTarget.y = target.y + fator.y * 2;
        }

        if (!gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Idling;
        }
    }

    private void FixedUpdate()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    void BeginCombat()
    {
        SwitchToIdling();
        SwitchToChasing();
        SwitchToAttacking();
        SwitchToShooting();
    }

    //STATES
    void SwitchToChasing()
    {
        if (Vector2.Distance(transform.position, target) <= agroDistance && Vector2.Distance(transform.position, target) > meleeDistance && health > 0 && !isASpitter && gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Chasing;
        }
        else if (Vector2.Distance(transform.position, target) <= chaseDistance && Vector2.Distance(transform.position, target) > meleeDistance && health > 0 && isASpitter && gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Chasing;
        }
    }
    void SwitchToIdling()
    {
        if (Vector2.Distance(transform.position, target) > agroDistance && health > 0)
        {
            state = State.Idling;
        }
    }
    void SwitchToAttacking()
    {
        if (Vector2.Distance(transform.position, target) <= meleeDistance && health > 0 && gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Attacking;
        }
    }
    void SwitchToShooting()
    {
        if (Vector2.Distance(transform.position, target) <= agroDistance && Vector2.Distance(transform.position, target) > chaseDistance && health > 0 && isASpitter && gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Shooting;
        }
    }
    void SwitchToDashing()
    {
        if (Vector2.Distance(transform.position, target) < canDashDistance && isADasher)
        {
            canDash = true;
        }
        else if (Vector2.Distance(transform.position, target) >= canDashDistance && canDash && isADasher)
        {
            state = State.Dashing;
        }
    }
    void SwitchToRecoveringFromDash()
    {
        state = State.RecoveringFromDash;
    }
    void SwitchToDead()
    {
        if (health <= 0)
        {
            audioSource.PlayOneShot(monster_death, audioSource.volume);
            state = State.Dead;
        }
    }

    public void OpenYourEyesToTheNight()
    {
        lastState = state;
        state = State.GazingIntoTheNightSky;
        StartCoroutine(SnapOutOfIt());
    }

    //MELEE
    void DamagePlayer()
    {
        if (Vector2.Distance(transform.position, target) <= meleeDistance && !isDashing)
        {
            //if (GameManager.instance.canTakeDamage)
            //    StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
            if(isAPoisonSpider)
            {
                if(GameManager.instance.canTakeDamage)
                {
                    GameManager.instance.TakeDamage(3);
                    GameManager.instance.Poison(1f);
                }
            }
            else
            {
                GameManager.instance.TakeDamage(damage / GameManager.instance.GetArmor());
            }
        }
        else if (Vector2.Distance(transform.position, target) <= dashMeleeDistance && isDashing)
        {
            //if (GameManager.instance.canTakeDamage)
                //StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower*1.2f, this.transform));
            GameManager.instance.TakeDamage(damage * 1.15f / GameManager.instance.GetArmor());

            isDashing = false;
        }
    }

    //RANGED
    void InstantiateProjectile()
    {
        Instantiate(projectile, spriteCenter.transform.position, Quaternion.identity);
    }

    //FLIP
    void Flip()
    {
        if (target.x < transform.position.x && !isDashing)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            psr.flip = new Vector3(0, 0, 0);
        }
        else if (target.x > transform.position.x && !isDashing)
        {
            transform.localScale = new Vector3(1, 1, 1);
            psr.flip = new Vector3(1, 0, 0);
        }
    }

    //HEALTH
    public void TakeDamage(bool isProjectile = false)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            StartCoroutine(CanTakeDamageCD());
            gameObject.GetComponent<ColorChanger>().ChangeColor();
            if (health >= 0)
            {
                //anim.SetTrigger("Hit");
                DrawBlood();
            }
            if(GameManager.instance.GetMeat() >= 0)
            {
                playerDamage = GameManager.instance.GetDamage() * (1 + GameManager.instance.GetMeat() / 6.2f) / armor;
            }
            else
            {
                playerDamage = GameManager.instance.GetDamage() / armor;
            }
            print(playerDamage);
            health -= playerDamage;
            audioSource.PlayOneShot(monster_hurt, audioSource.volume);

            if (canBeStunned && !isAGigantebas)
            {
                anim.SetTrigger("Hit");
                state = State.Stunned;
            }
        }           
    }

    private void PlayStepDust()
    {
        if(stepDust != null)
        {
            stepDust.gameObject.SetActive(true);
            stepDust.Stop();
            stepDust.Play();
        } 
    }
    private void PlayStompDust()
    {
        if(stompDust != null)
        {
            stompDust.gameObject.SetActive(true);
            stompDust.Stop();
            stompDust.Play();
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //GameManager.instance.TakeDamage(5);

            if (!isDashing)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }

        }

        if (isADasher && isDashing)
        {
            if (collision.gameObject.CompareTag("PAREDE"))
            {
                state = State.RecoveringFromDash;

                isDashing = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PROJECTILE"))
        {
            if (collision.transform.GetComponent<Projectile>().hasBeenParried)
            {
                TakeDamage(true);
            }
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject, 1.5f);
    }

    void KnockBackSide()
    {
        if (canBeKbed)
        {
            canBeKbed = false;
            StartCoroutine(Knockback(knockbackDuration/2, 25f, player));
        }
    }

    private void DrawBlood()
    {
        bloodSpatter.gameObject.SetActive(true);
        bloodSpatter.Stop();
        bloodSpatter.Play();
    }

    private IEnumerator CanTakeDamageCD()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    }

    public IEnumerator Knockback(float kbDuration, float kbPower, Transform obj)
    {
        float timer = 0;
        rb.velocity = Vector2.zero;
        while (kbDuration > timer)
        {
            timer += Time.deltaTime;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            //Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1.7f,
            //this.transform.position.z);
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * kbPower);
        }
        canBeKbed = true;
        yield return 0;
    }
    public IEnumerator SnapOutOfIt()
    {
        yield return new WaitForSeconds(4f);
        state = lastState;
    }

    public void PlayThePunchAudio()
    {
        audioSource.PlayOneShot(monster_punch, audioSource.volume);
    }
}
