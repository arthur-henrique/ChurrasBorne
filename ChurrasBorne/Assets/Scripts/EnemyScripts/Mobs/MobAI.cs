using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    private enum State
    {
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

    public Transform player, armoredTebasSpeartip, trickstebasStaff;
    private Vector3 target;
    public Vector3 dashTarget;

    public GameObject projectile, armoredTebasProjectile;
    public ArmoredTebasProjectile armored;

    public GameObject gameManager;

    public GameObject[] tpPoints;

    private AudioSource audioSource;
    public AudioClip monster_death;
    public AudioClip monster_hurt;
    public AudioClip monster_punch;
    public AudioClip monster_spit;

    public float agroDistance, meleeDistance, canDashDistance, dashMeleeDistance, chaseDistance, chasingSpeed, dashingSpeed, startStunTime, startTimeBTWAttacks, startTimeBTWShots;
    private float TimeBTWAttacks, timeBTWShots, stunTime, dashRecoveryTime, timeBTWWindupATKs;  

    public bool isASpitter,
        isADasher,
        isASpider,
        isAPoisonSpider,
        isATebas,
        isAGeletebas,
        isAShatebas,
        isAGigantebas,
        isASkeletebas,
        isAnArmoredTebas,
        isATrickstebas,
        isASkully;

    private bool canDash = false, isDashing = false, canBeStunned = true, isUsingWindupATK = false;

    public bool isOnTutorial, isOnFaseUm, isOnFaseDois, isOnFaseTres, isOnFaseQuatro;
    public FaseQuatroRoomController controller;

    private float yOffset = 1.7f;
    private float knockbackDuration = 1f;
    private float knockbackPower = 25f;

    private bool canBeKbed = true;
    private bool canTakeDamage = true;

    public bool isFlipped;

    public ParticleSystem bloodSpatter, stepDust, stompDust;
    private ParticleSystemRenderer psr;

    // Controle de dano:
    private float damage, armor, health;
    private float playerDamage;
    private float stunCD;

    public GameObject spriteCenter;

    private float parriedProjectileDamage = 15;

    private void Awake()
    {
        psr = bloodSpatter.GetComponent<ParticleSystemRenderer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        sr = gameObject.GetComponent<SpriteRenderer>();
        target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        audioSource = GetComponent<AudioSource>();

        TimeBTWAttacks = 0.1f;
        timeBTWShots = 1.5f;

        timeBTWWindupATKs = Random.Range(2f, 4f);

        stunTime = startStunTime;
        stunCD = Random.Range(1, 3);

        dashRecoveryTime = 1.5f;
        
        if(isASpitter)
        {
            health = 45f;
            damage = 15f;
            armor = 1f;
        }
        if(isADasher)
        {
            health = 50f;
            damage = 30f;
            armor = 1f;
        }
        if (isASpider)
        {
            health = 75f;
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
        if(isATebas)
        {
            health = 100f;
            damage = 10f;
            armor = 1f;
        }
        if(isAGeletebas)
        {
            health = 100f;
            damage = 10f;
            armor = 1.25f;
        }
        if(isAShatebas)
        {
            health = 100f;
            damage = 25f;
            armor = 0.75f;
        }
        if(isAGigantebas)
        {
            health = 200f;
            damage = 25f;
            armor = 1.15f;
        }
        if(isASkeletebas)
        {
            health = 75f;
            damage = 30f;
            armor = 0.75f;
        }
        if(isAnArmoredTebas)
        {
            health = 130f;
            damage = 30f;
            armor = 1.5f;
        }
        if(isATrickstebas)
        {
            health = 90f;
            damage = 15f;
            armor = 1f;
        }
        if(isASkully)
        {
            health = 60f;
            damage = 15f;
            armor = 0.5f;
        }

    }

    void Update()
    {
        switch(state)
        {
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

                if(TimeBTWAttacks <= 0 && !isAnArmoredTebas)
                {
                    anim.SetTrigger("Melee");
                    //audioSource.PlayOneShot(monster_punch, audioSource.volume);
                    TimeBTWAttacks = startTimeBTWAttacks;
                }
                else
                {
                    TimeBTWAttacks -= Time.deltaTime;
                }

                if(isAnArmoredTebas)
                {
                    if (TimeBTWAttacks <= 0 && !isUsingWindupATK)
                    {
                        anim.SetTrigger("Melee");
                        //audioSource.PlayOneShot(monster_punch, audioSource.volume);
                        TimeBTWAttacks = startTimeBTWAttacks;
                    }
                    else
                    {
                        TimeBTWAttacks -= Time.deltaTime;
                    }

                    if (timeBTWWindupATKs <= 0)
                    {
                        anim.SetTrigger("WindupATK");
                        timeBTWWindupATKs = Random.Range(5f, 7f);
                        isUsingWindupATK = true;
                    }
                    else
                    {
                        timeBTWWindupATKs -= Time.deltaTime;    
                    }
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
                    if (!isASkully)
                    {
                        anim.SetTrigger("Ranged");
                    }
                    if(isASkully)
                    {
                        anim.SetTrigger("Melee");
                    }
                    //apagar depois a condicao
                    if (!isATrickstebas)
                    {
                        audioSource.PlayOneShot(monster_spit, audioSource.volume);
                    }

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

                    dashRecoveryTime = 1.5f;
                }
                else
                {
                    dashRecoveryTime -= Time.deltaTime;
                }

                print(dashRecoveryTime);
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
                else if(isOnFaseQuatro)
                {
                    controller.KilledEnemy(gameObject);
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
                stunCD = Random.Range(1, 3);
                canBeStunned = true;
            }
        }

        //DASH
        if(!isDashing)
        {
            dashTarget = target;

            Vector3 fator = target - transform.position;

            dashTarget.x = target.x + fator.x;

            dashTarget.y = target.y + fator.y;
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
        if (Vector2.Distance(transform.position, target) <= agroDistance && Vector2.Distance(transform.position, target) > meleeDistance && health > 0 && !isASpitter && !isATrickstebas && !isASkully && gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Chasing;
        }
        if (Vector2.Distance(transform.position, target) <= chaseDistance && Vector2.Distance(transform.position, target) > meleeDistance && health > 0 && (isASpitter || isATrickstebas || isASkully) && gameManager.GetComponent<GameManager>().isAlive)
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
        if (Vector2.Distance(transform.position, target) <= agroDistance && Vector2.Distance(transform.position, target) > chaseDistance && health > 0 && (isASpitter || isATrickstebas || isASkully) && gameManager.GetComponent<GameManager>().isAlive)
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
        if (Vector2.Distance(transform.position, target) <= dashMeleeDistance && isDashing)
        {
            //if (GameManager.instance.canTakeDamage)
                //StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower*1.2f, this.transform));
            GameManager.instance.TakeDamage(damage * 1.15f / GameManager.instance.GetArmor());

            isDashing = false;
        }
    }

    void WindupATK()
    {
        if (Vector2.Distance(transform.position, target) <= meleeDistance)
        {
            GameManager.instance.TakeDamage(damage);
            armored = Instantiate(armoredTebasProjectile, armoredTebasSpeartip.position, Quaternion.identity).GetComponent<ArmoredTebasProjectile>();
            armored.ArmorTebasSetter(this);
        }
        else
        {
            armored = Instantiate(armoredTebasProjectile, armoredTebasSpeartip.position, Quaternion.identity).GetComponent<ArmoredTebasProjectile>();
            armored.ArmorTebasSetter(this);
        }

        isUsingWindupATK = false;

        TimeBTWAttacks = startTimeBTWAttacks;            
;    }

    //RANGED
    void InstantiateProjectile()
    {
        if(isASkully)
        {
            if(Vector2.Distance(transform.position, player.position) > chaseDistance)
            {
                Instantiate(projectile, spriteCenter.transform.position, Quaternion.identity);
            }
        }
        
        if (!isATrickstebas)
        {
            Instantiate(projectile, spriteCenter.transform.position, Quaternion.identity);
        }


        if(isATrickstebas)
        {
            Instantiate(projectile, trickstebasStaff.position, Quaternion.identity);
        }
    }

    void Disappear()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetTrigger("Disappear");
    }
    void HasDisappeared()
    {
        int rand = Random.Range(0, 2);

        transform.position = tpPoints[rand].transform.position;

        anim.SetTrigger("Reappear");
    }
    void HasReappeared()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

        canTakeDamage = true;

        SwitchToChasing();
        SwitchToIdling();
        SwitchToAttacking();
        SwitchToShooting();
    }

    //FLIP
    void Flip()
    {
        if (target.x < transform.position.x && !isDashing)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            psr.flip = new Vector3(0, 0, 0);
            isFlipped = true;
        }
        else if (target.x > transform.position.x && !isDashing)
        {
            transform.localScale = new Vector3(1, 1, 1);
            psr.flip = new Vector3(1, 0, 0);
            isFlipped = false;
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

            if (!isProjectile)
            {
                if (health >= 0)
                {
                    //anim.SetTrigger("Hit");
                    if (!isASkeletebas && !isASkully)
                        DrawBlood();
                }
                if (GameManager.instance.GetMeat() >= 0)
                {
                    playerDamage = GameManager.instance.GetDamage() * (1 + GameManager.instance.GetMeat() / 6.2f) / armor;
                }
                else
                {
                    playerDamage = GameManager.instance.GetDamage() / armor;
                }
            }
            else
            {
                playerDamage = parriedProjectileDamage;
            }


            health -= playerDamage;

            audioSource.PlayOneShot(monster_hurt, audioSource.volume);

            if (canBeStunned && !isAGigantebas && !isATrickstebas)
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
        if (!isATrickstebas)
        {
            if (collision.gameObject.CompareTag("PROJECTILE"))
            {
                if (collision.transform.GetComponent<Projectile>().hasBeenParried)
                {
                    
                    TakeDamage(true);
                }
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
