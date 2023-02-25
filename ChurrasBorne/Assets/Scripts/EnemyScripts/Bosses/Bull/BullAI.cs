using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Summon,
        AxeSlam,
        Scream,
        Idling,
        Dead
    }
    private State state;

    public GameObject gameManager;
    public Transform player;
    public Rigidbody2D rb;
    public Animator anim;

    public GameObject bullSpikesI, bullSpikesII, protectiveSpikes;
    public Collider2D portal;

    public AudioSource audioSource;
    public AudioClip bull_attack;
    public AudioClip bull_charge;
    public AudioClip bull_death;
    public AudioClip bull_roar;
    public AudioClip bull_hurt;

    public float health;

    public float chasingSpeed, meleeDistance, rangedDistance, spikesSummonDistance;
    private float timeBTWMeleeATKs, timeBTWRangedATKs, timeBTWScreamATKs, randNum;

    public bool isOnTut, isAlive = true;

    private float knockbackDuration = 1.5f, knockbackPower = 50f;
    private bool canTakeDamage = true, hasScreamed = false, isSummoningSpikes = false;

    public ParticleSystem bloodSpatter, stepDust, stompDust;
    private ParticleSystemRenderer psr;
    private float armor, playerDamage;

    private void Awake()
    {
        psr = bloodSpatter.GetComponent<ParticleSystemRenderer>();
        state = State.Spawning;
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Para SPAWN, MOVEMENT, BASH, AXE
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        timeBTWMeleeATKs = 0.5f;
        timeBTWRangedATKs = 1.5f;
        timeBTWScreamATKs = Random.Range(15f, 21f);

        if (isOnTut)
        {
            health = 400;
        }
        else
        {
            health = 800;
        }
        audioSource.PlayOneShot(bull_roar, audioSource.volume);
        HealthBar_Manager.instance.boss = this.gameObject;
        HealthBar_Manager.instance.refreshBoss = true;

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
                anim.SetBool("Idle", false);

                timeBTWRangedATKs -= Time.deltaTime;

                if (timeBTWRangedATKs <= 0)
                {
                    isSummoningSpikes = true;
                    state = State.Summon;
                }

                SwitchBTWCombatStates();
                SwitchToSummonATK();
                break;

            case State.AxeSlam:
                Flip();
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (timeBTWMeleeATKs <= 0)
                {
                    anim.SetTrigger("Slam");
                    audioSource.PlayOneShot(bull_attack, audioSource.volume);
                    timeBTWMeleeATKs = Random.Range(1.5f, 2.5f);
                }
                else
                {
                    timeBTWMeleeATKs -= Time.deltaTime;
                }

                SwitchBTWCombatStates();
                SwitchToSummonATK();
                break;

            case State.Summon:
                Flip();
                rb.velocity = Vector2.zero;

                randNum = Random.Range(1, 3);

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                anim.SetTrigger("Summon");
                audioSource.PlayOneShot(bull_charge, audioSource.volume);

                SwitchToSummonATK();
                break;

            case State.Scream:
                Flip();
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (!hasScreamed)
                {
                    anim.SetTrigger("Scream");
                }
                break;

            case State.Dead:
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                rb.velocity = Vector2.zero;

                isAlive = false;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                anim.SetTrigger("Die");
                audioSource.PlayOneShot(bull_death, audioSource.volume);
                break;

            case State.Idling:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                break;
        }

        if(hasScreamed)
        {
            timeBTWScreamATKs -= Time.deltaTime;
        }
        if(timeBTWScreamATKs <= 0)
        {
            hasScreamed = false;
        }

        if (!gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Idling;
        }
    }

    //STATES
    void SwitchBTWCombatStates()
    {
        if(Vector2.Distance(transform.position, player.position) <= rangedDistance && health > 0 && !isSummoningSpikes)
        {
            state = State.AxeSlam;
        }
        if(Vector2.Distance(transform.position, player.position) > rangedDistance && health > 0 && !isSummoningSpikes)
        {
            state = State.Chasing;
        }
    }
    void SwitchToSummonATK()
    {
        if (health <= 200 && Vector2.Distance(transform.position, player.position) <= rangedDistance && !hasScreamed)
        {
            state = State.Scream;
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            isAlive = false;
            state = State.Dead;
            GameManager.instance.SwitchToDefaultCam();
            if (isOnTut)
            {
                TutorialTriggerController.Instance.SecondGateTriggerOut();
                portal.enabled = true;
                portal.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);    
    }

    //FLIP
    void Flip()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            psr.flip = new Vector3(0, 0, 0);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            psr.flip = new Vector3(1, 0, 0);
        }
    }

    //MELEE
    public void DamagePlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= meleeDistance && isOnTut)
        {
            //StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
            GameManager.instance.TakeDamage(15);

        }
        else if (Vector2.Distance(transform.position, player.position) <= meleeDistance && !isOnTut)
        {
            //StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
            GameManager.instance.TakeDamage(30);
        }
        timeBTWRangedATKs = Random.Range(1.5f, 2.5f);
    }

    //SPIKES
    public void SummonSpike()
    {
        if(Vector2.Distance(transform.position, player.position) > spikesSummonDistance)
        {
            if(randNum == 1)
            {
                Instantiate(bullSpikesI, player.position, Quaternion.identity);
                randNum = 0;
            }
            if(randNum == 2)
            {
                Instantiate(bullSpikesII, player.position, Quaternion.identity);
                randNum = 0;
            }
        }
        else
        {
            GameManager.instance.TakeDamage(15);
        }
        timeBTWMeleeATKs = 0.5f;
        timeBTWRangedATKs = Random.Range(2f, 3.6f);
        isSummoningSpikes = false;
        SwitchBTWCombatStates();
    }
    public void HasScreamed()
    {
        hasScreamed = true;
    }
    public void SummonProtectiveSpikes()
    {
        timeBTWScreamATKs = Random.Range(15f, 21f);
        timeBTWMeleeATKs = Random.Range(1.5f, 2.5f);
        timeBTWRangedATKs = Random.Range(2.5f, 3.5f);
        Instantiate(protectiveSpikes, transform.position, Quaternion.identity);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

        if(Vector2.Distance(transform.position, player.position) <= meleeDistance)
        {
            GameManager.instance.TakeDamage(20);
        }

        SwitchBTWCombatStates();
    }

    //HEALTH
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            StartCoroutine(CanTakeDamageCD());
            gameObject.GetComponent<ColorChanger>().ChangeColor();
            DrawBlood();
            float damage = GameManager.instance.GetDamage() / armor;


            if (GameManager.instance.GetMeat() >= 0)
            {
                playerDamage = GameManager.instance.GetDamage() * (1 + GameManager.instance.GetMeat() / 6.2f) / armor;
            }
            else
            {
                playerDamage = GameManager.instance.GetDamage() / armor;
            }
            health -= playerDamage;
            audioSource.PlayOneShot(bull_hurt, audioSource.volume);

            if(isAlive)
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
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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
