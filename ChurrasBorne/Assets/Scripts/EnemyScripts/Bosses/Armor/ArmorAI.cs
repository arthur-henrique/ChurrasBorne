using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        BladeSlash,
        BladeSpin,
        Idling,
        Dead
    }
    private State state;

    public Transform player;
    public GameObject gameManager;

    public Rigidbody2D rb;
    public Animator anim;

    public GameObject bladeWave;
    public GameObject waveSpawnPoint;

    public AudioSource audioSource;
    public AudioClip armor_slash;
    public AudioClip armor_death;
    public AudioClip armor_hurt;
    public AudioClip armor_bladespin;

    private bool isAlreadyDying = false;

    public int health;

    public float chasingSpeed, timeBTWSlashATKs, slashMeleeDistance, slashRangedDistance, timeBTWSpinATKs, spinDistance;
    private float currentTimeBTWSlashATKs, currentTimeBTWSpinATKs, timeToDie;

    private float knockbackDuration1 = 0.75f, knockbackPower1 = 75f;
    private float knockbackDuration2 = 1.5f, knockbackPower2 = 125f;

    // to remove
    public bool isOnFaseDois, isOnFaseDoisHalf;
    public Animator faseDois, faseDoisHalf;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        currentTimeBTWSlashATKs = .1f;
        currentTimeBTWSpinATKs = .1f;
        timeToDie = .1f;

        HealthBar_Manager.instance.boss = this.gameObject;
        HealthBar_Manager.instance.refreshBoss = true;
    }

    void Update()
    {
        switch(state)
        {
            case State.Spawning:
                Flip();
                break;

            case State.Chasing:
                Flip();

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                anim.SetBool("Walk", true);
                anim.SetBool("Idle", false);

                SwitchToBladeSlash();
                break;

            case State.BladeSlash:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if(currentTimeBTWSlashATKs <= 0)
                {
                    anim.SetTrigger("BladeSlash");
                    audioSource.PlayOneShot(armor_slash, audioSource.volume);
                    currentTimeBTWSlashATKs = timeBTWSlashATKs;
                }
                else
                {
                    currentTimeBTWSlashATKs -= Time.deltaTime;  
                }

                SwitchToChasing();
                SwitchToBladeSpin();
                break;

            case State.BladeSpin:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (currentTimeBTWSpinATKs <= 0)
                {
                    anim.SetTrigger("BladeSpin");
                    audioSource.PlayOneShot(armor_bladespin, audioSource.volume);
                    currentTimeBTWSpinATKs = timeBTWSpinATKs;
                }
                else
                {
                    currentTimeBTWSpinATKs -= Time.deltaTime;
                }

                SwitchToBladeSlash();
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                isAlreadyDying = true;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                if (timeToDie <= 0)
                {
                    anim.SetTrigger("Die");
                    audioSource.PlayOneShot(armor_death, audioSource.volume);
                    timeToDie = 1000;
                }
                else
                {
                    timeToDie -= Time.deltaTime;
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
    }

    void SwitchToChasing()
    {
        if (Vector2.Distance(transform.position, player.position) > slashMeleeDistance && health > 50)
        {
            state = State.Chasing;
        }
        else if (Vector2.Distance(transform.position, player.position) > slashRangedDistance && health <= 50 && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToBladeSlash()
    {
        if (Vector2.Distance(transform.position, player.position) <= slashMeleeDistance && Vector2.Distance(transform.position, player.position) > spinDistance && health > 50)
        {
            state = State.BladeSlash;
        }
        else if (Vector2.Distance(transform.position, player.position) <= slashRangedDistance && Vector2.Distance(transform.position, player.position) > spinDistance && health <= 50 && health > 0)
        {
            state = State.BladeSlash;
        }
    }
    void SwitchToBladeSpin()
    {
        if (Vector2.Distance(transform.position,player.position) <= spinDistance && health > 0)
        {
            state = State.BladeSpin;
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            state = State.Dead;
            if (isOnFaseDois)
            {
                FaseDoisTriggerController.Instance.GateOpener();
                faseDois.SetTrigger("ON");
                GameManager.instance.SetHasCleared(2, true);
            }
            else if (isOnFaseDoisHalf)
            {
                FaseDoisTriggerController.Instance.GateOpener();
                faseDoisHalf.SetTrigger("ON");
                GameManager.instance.SetHasCleared(3, true);
            }
        }
    }

    void BeginCombat()
    {
        SwitchToChasing();
        SwitchToBladeSlash();
        SwitchToBladeSpin();
    }

    void SlashATK()
    {
        if (Vector2.Distance(transform.position, player.position) <= slashMeleeDistance && health > 90)
        {
            GameManager.instance.TakeDamage(5);
        }
        else if (Vector2.Distance(transform.position, player.position) <= slashRangedDistance && Vector2.Distance(transform.position, player.position) > slashMeleeDistance && health <= 90 && health > 0)
        {
            Instantiate(bladeWave, waveSpawnPoint.transform.position, Quaternion.identity);
        }
    }

    void SpinATKI()
    {
        if(Vector2.Distance(transform.position, player.position) <= spinDistance)
        {
            StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration1, knockbackPower1, this.transform));
            GameManager.instance.TakeDamage(5);
        }
    }
    void SpinATKII()
    {
        if (Vector2.Distance(transform.position, player.position) <= spinDistance)
        {
            StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration2, knockbackPower2, this.transform));
            GameManager.instance.TakeDamage(10);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);    
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

    public void TakeDamage()
    {
        gameObject.GetComponent<ColorChanger>().ChangeColor();
        int damage = 10;
        health -= damage;
        audioSource.PlayOneShot(armor_hurt, audioSource.volume);
        if (!isAlreadyDying)
        {
            SwitchToDead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
