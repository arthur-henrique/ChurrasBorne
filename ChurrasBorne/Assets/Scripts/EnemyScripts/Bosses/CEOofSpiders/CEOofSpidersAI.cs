using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class CEOofSpidersAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Shooting,
        Idling,
        Dead
    }
    
    private State state;

    public Transform player;
    public GameObject gameManager, wandTip;

    public Rigidbody2D rb;

    public GameObject web, glassSpike, spiders;

    public GameObject[] tbPoints;

    public Animator anim;

    public float health;

    public bool isSpiderGranny;

    private bool isAlive = true, isAlreadyDying = false;

    public float speed, chaseDistance, startTimeBTWWebShot, repositionDistance;
    private float timeBTWWebShots, timeToReposition;

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
    private float playerDamage;
    private float armor = 1f;
    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        timeBTWWebShots = startTimeBTWWebShot;
        timeToReposition = 2f;

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

                SwitchCombatState();
                break;

            case State.Shooting:
                Flip();

                rb.velocity = Vector2.zero;

                if(Vector2.Distance(transform.position, player.position) <= repositionDistance)
                {
                    timeToReposition -= Time.deltaTime;
                }
                else
                {
                    timeToReposition = 2f;
                }

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

                SwitchCombatState();
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("Die");
                audioSource.PlayOneShot(spider_death, audioSource.volume);

                isAlive = false;
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

    void aeHasSpawned()
    {
        SwitchCombatState();
    }

    void SwitchCombatState()
    {
        if (Vector2.Distance(transform.position, player.position) > chaseDistance && health > 0)
        {
            state = State.Chasing;
        }
        if (Vector2.Distance(transform.position, player.position) <= chaseDistance && health > 0)
        {
            state = State.Shooting;
        }
    }

    void Die()
    {
        if(health <= 0)
        {
            
            if (!isSpiderGranny)
            {
                FaseDoisTriggerController.Instance.GateOpener();
                faseDois.SetTrigger("ON");
                GameManager.instance.SetHasCleared(2, true);
            }
            else if (isSpiderGranny)
            {
                gc.isTheBossDead= true;
                gc.SetGeloPos(gameObject.transform.position);
            }
            isAlreadyDying = true;
            state = State.Dead;
            GameManager.instance.SwitchToDefaultCam();
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

    void aeShootSpike()
    {
        Instantiate(glassSpike, wandTip.transform.position, Quaternion.identity);
        audioSource.PlayOneShot(spider_attack_2, audioSource.volume);

        if(timeToReposition <= 0)
        {
            timeToReposition = 2f;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.SetTrigger("Disappear");
        }
    }
    void aeShootWeb()
    {
        Instantiate(web, wandTip.transform.position, Quaternion.identity);
        audioSource.PlayOneShot(spider_attack_1, audioSource.volume);

        if (timeToReposition <= 0)
        {
            timeToReposition = 2f;
            anim.SetTrigger("Disappear");
        }
    }

    void aeHasDisappeared()
    {
        int rand = Random.Range(0, 6);

        transform.position = tbPoints[rand].transform.position;

        if(isSpiderGranny)
        {
            Instantiate(spiders, transform.position, Quaternion.identity);  
        }

        anim.SetTrigger("Reappear");
    }
    void aeHasReappeared()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        SwitchCombatState();
    }

    public void TakeDamage(bool isProjectile = false)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            StartCoroutine(CanTakeDamageCD());
            gameObject.GetComponent<ColorChanger>().ChangeColor();
            if (GameManager.instance.GetMeat() >= 0)
            {
                playerDamage = GameManager.instance.GetDamage() * (1 + GameManager.instance.GetMeat() / 6.2f) / armor;
            }
            else
            {
                playerDamage = GameManager.instance.GetDamage() / armor;
            }
            health -= playerDamage;
            audioSource.PlayOneShot(spider_hurt, audioSource.volume);

            if (!isAlreadyDying)
            {
                Die();
            }
        }
    }

    void aeDestroySelf()
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
