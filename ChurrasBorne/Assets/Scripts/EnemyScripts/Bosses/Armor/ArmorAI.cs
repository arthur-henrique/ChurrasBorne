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
        Dead
    }

    private State state;

    public Transform player;

    public Rigidbody2D rb;
    public Animator anim;

    public GameObject bladeWave;
    public GameObject waveSpawnPoint;

    public int health;

    public float chasingSpeed, timeBTWSlashATKs, slashMeleeDistance, slashRangedDistance, timeBTWSpinATKs, spinDistance;
    private float currentTimeBTWSlashATKs, currentTimeBTWSpinATKs;

    public bool isOnTut;
    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentTimeBTWSlashATKs = 0.1f;
        currentTimeBTWSpinATKs = 0.1f;
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
                SwitchToDead();
                break;

            case State.BladeSlash:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if(currentTimeBTWSlashATKs <= 0)
                {
                    anim.SetTrigger("BladeSlash");

                    currentTimeBTWSlashATKs = timeBTWSlashATKs;
                }
                else
                {
                    currentTimeBTWSlashATKs -= Time.deltaTime;  
                }

                SwitchToChasing();
                SwitchToBladeSpin();
                SwitchToDead();
                break;

            case State.BladeSpin:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (currentTimeBTWSpinATKs <= 0)
                {
                    anim.SetTrigger("BladeSpin");

                    currentTimeBTWSpinATKs = timeBTWSpinATKs;
                }
                else
                {
                    currentTimeBTWSpinATKs -= Time.deltaTime;
                }

                SwitchToBladeSlash();
                SwitchToDead();
                break;

            case State.Dead:
                anim.SetTrigger("Die");

                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                if (isOnTut)
                {
                    TutorialTriggerController.Instance.SecondGateTriggerOut();
                }
                break;
        }
    }

    void SwitchToChasing()
    {
        if(Vector2.Distance(transform.position, player.position) > slashMeleeDistance && health > 50)
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
        if(Vector2.Distance(transform.position, player.position) <= slashMeleeDistance && Vector2.Distance(transform.position, player.position) > spinDistance && health > 50)
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
        if(Vector2.Distance(transform.position,player.position) <= spinDistance && health > 0)
        {
            state = State.BladeSpin;
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            state = State.Dead;
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
        if(Vector2.Distance(transform.position, player.position) <= slashMeleeDistance && health > 50)
        {
            GameManager.instance.TakeDamage(5);
        }
        else if(Vector2.Distance(transform.position, player.position) <= slashRangedDistance && Vector2.Distance(transform.position, player.position) > slashMeleeDistance && health <= 50 && health > 0)
        {
            Instantiate(bladeWave, waveSpawnPoint.transform.position, Quaternion.identity);
        }
    }

    void SpinATKI()
    {
        if(Vector2.Distance(transform.position, player.position) <= spinDistance)
        {
            GameManager.instance.TakeDamage(5);
        }
    }
    void SpinATKII()
    {
        if (Vector2.Distance(transform.position, player.position) <= spinDistance)
        {
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
        int damage;

        damage = 10;

        health -= damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.TakeDamage(5);
        }
    }
}