using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class CEOofSpidersAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Running,
        Shooting,
        SpawningSpiders,
        WasHit,
        Idling,
        Repositioning,
        Dead
    }
    
    private State state;

    public Transform player;
    public GameObject gameManager;

    public Rigidbody2D rb;

    public GameObject web, spike, spiders, shootPoint;

    public GameObject[] tbPoints;

    public Animator anim;

    public int health;

    public bool isSpiderGranny;

    private bool isAlreadyDying = false, isAlreadySpawningSpiders = false;

    public float speed, rangedDistanceI, rangedDistanceII, startTimeBTWWebShot, startTimeToSpawnSpiders, startRunningTime, startTimeToReposition;
    private float timeBTWWebShots, timeToSpawnSpiders, timeToDie, runningTime, timeToReposition;

    public Animator faseDois, faseDoisHalf;

    public static bool spider_boss_died = false;

    public AudioSource audioSource;
    public AudioClip spider_hurt;
    public AudioClip spider_intro;
    public AudioClip spider_attack_1;
    public AudioClip spider_attack_2;
    public AudioClip spider_attack_3;
    public AudioClip spider_death;
    private bool canTakeDamage = true;
    public GateChecker gc;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        timeToDie = .1f;
        runningTime = startRunningTime;

        timeBTWWebShots = startTimeBTWWebShot;
        timeToSpawnSpiders = startTimeToSpawnSpiders;
        timeToReposition = startTimeToReposition;

        HealthBar_Manager.instance.boss = this.gameObject;
        HealthBar_Manager.instance.refreshBoss = true;

        audioSource.PlayOneShot(spider_intro, audioSource.volume);
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

                anim.SetBool("Walk", true);
                anim.SetBool("ATK1", false);

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                SwitchToShooting();
                SwitchToSpawningSpiders();
                break;

            case State.Running:
                Flip();

                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

                if (isSpiderGranny)
                {
                    if (runningTime <= 0)
                    {
                        anim.SetTrigger("ATK3");
                        audioSource.PlayOneShot(spider_attack_3, audioSource.volume);
                        runningTime = startRunningTime;
                    }
                    else
                    {
                        runningTime -= Time.deltaTime;
                    }
                }

                if (!isSpiderGranny)
                {
                    if (timeToReposition <= 0)
                    {
                        state = State.Repositioning;

                        timeToReposition = startTimeToReposition;
                    }
                    else
                    {
                        timeToReposition -= Time.deltaTime;
                    }
                }
                anim.SetBool("Walk", true);
                anim.SetBool("ATK1", false);

                SwitchToChasing();
                SwitchToShooting();
                SwitchToSpawningSpiders();
                break;

            case State.Repositioning:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Walk", true);
                anim.SetBool("ATK1", false);

                anim.SetTrigger("Disappear");
                break;

            case State.Shooting:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("ATK1", true);
                //audioSource.PlayOneShot(spider_attack_1, audioSource.volume);
                anim.SetBool("Walk", false);

                if (timeBTWWebShots <= 0)
                {
                    anim.SetTrigger("ATK2");
                    audioSource.PlayOneShot(spider_attack_2, audioSource.volume);
                    startTimeBTWWebShot = Random.Range(3f, 5f);
                    timeBTWWebShots = startTimeBTWWebShot;
                }
                else
                {
                    timeBTWWebShots -= Time.deltaTime;
                }

                SwitchToRunning();
                SwitchToChasing();
                SwitchToSpawningSpiders();
                break;

            case State.SpawningSpiders:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("ATK1", true);
                anim.SetBool("Walk", false);
                //audioSource.PlayOneShot(spider_attack_1, audioSource.volume);

                if (!isAlreadySpawningSpiders)
                {
                    anim.SetTrigger("ATK3");
                    audioSource.PlayOneShot(spider_attack_3, audioSource.volume);
                    isAlreadySpawningSpiders = true;    
                }
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                anim.SetBool("ATK1", true);
                anim.SetBool("Walk", false);

                timeToReposition = startTimeToReposition;

                if (timeToDie <= 0)
                {
                    anim.SetTrigger("Die");
                    audioSource.PlayOneShot(spider_death, audioSource.volume);
                    timeToDie = 10000;
                }
                else
                {
                    timeToDie -= Time.deltaTime;
                }

                isAlreadyDying = true;
                spider_boss_died = true;

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                break;

            case State.Idling:
                rb.velocity = Vector2.zero;

                anim.SetBool("Walk", true);
                anim.SetBool("ATK1", false);
                break;
        }

        if (!gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Idling;
        }
    }

    void BeginCombat()
    {
        SwitchToRunning();
        SwitchToChasing();
        SwitchToShooting();
    }

    void SwitchToChasing()
    {
        if (Vector2.Distance(transform.position, player.position) > rangedDistanceII && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToRunning()
    {
        if (Vector2.Distance(transform.position, player.position) <= rangedDistanceI && health > 0)
        {
            state = State.Running;
        }
    }
    void SwitchToShooting()
    {
        if (Vector2.Distance(transform.position, player.position) > rangedDistanceI && Vector2.Distance(transform.position, player.position) <= rangedDistanceII && health > 0)
        {
            state = State.Shooting;
        }
    }
    void SwitchToSpawningSpiders()
    {
        if (isSpiderGranny)
        {
            if (timeToSpawnSpiders <= 0)
            {
                state = State.SpawningSpiders;

                timeToSpawnSpiders = startTimeToSpawnSpiders;
            }
            else
            {
                timeToSpawnSpiders -= Time.deltaTime;
            }
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            state = State.Dead;
            GameManager.instance.SwitchToDefaultCam();
            if (!isSpiderGranny)
            {
                FaseDoisTriggerController.Instance.GateOpener();
                faseDois.SetTrigger("ON");
                GameManager.instance.SetHasCleared(2, true);
            }
            else if (isSpiderGranny)
            {
                gc.isTheBossDead= true;
            }
        }
    }

    void Flip()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void ShootSpike()
    {
        Instantiate(spike, shootPoint.transform.position, Quaternion.identity);
        audioSource.PlayOneShot(spider_attack_2, audioSource.volume);
    }
    void ShootWeb()
    {
        Instantiate(web, shootPoint.transform.position, Quaternion.identity);
        audioSource.PlayOneShot(spider_attack_1, audioSource.volume);
    }
    void SpawnSpiders()
    {
        Instantiate(spiders, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(spider_attack_3, audioSource.volume);
        isAlreadySpawningSpiders = false;

        SwitchToChasing();
        SwitchToRunning();
        SwitchToShooting();
        SwitchToSpawningSpiders();
    }
    void HasDisappeared()
    {
        int rand = Random.Range(0, 6);

        transform.position = tbPoints[rand].transform.position;

        anim.SetTrigger("Reappear");
    }

    void HasReappeared()
    {
        SwitchToChasing();
        SwitchToRunning();
        SwitchToShooting();
    }

    public void TakeDamage(bool isProjectile = false)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            StartCoroutine(CanTakeDamageCD());
            gameObject.GetComponent<ColorChanger>().ChangeColor();
            int damage = 10;
            health -= damage;
            audioSource.PlayOneShot(spider_hurt, audioSource.volume);
            anim.SetTrigger("Hit");

            if (!isAlreadyDying)
            {
                SwitchToDead();
            }
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
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
    private IEnumerator CanTakeDamageCD()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    }
}
