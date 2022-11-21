using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        AxeSwing,
        HeadBash,
        SpikeSummon,
        Idling,
        Dead
    }

    public Transform player;
    public Transform spawnPoint;

    public Rigidbody2D rb;

    public GameObject bullSpike;

    public Animator anim;

    public int health;

    public float chasingSpeed, bashDistance, startTimeBTWBashATKs, axeDistance, startTimeBTWAxeATKs, startTimeToSummonSpikes;
    private float timeBTWBashATKs, timeBTWAxeATKs, timeToSummonSpikes;

    public bool isOnTut, isOnFaseQuatro;

    private static State state;

    private void Awake()
    {
        state = State.Spawning;
    }


    void Start()
    {
        //Para SPAWN, MOVEMENT, BASH, AXE
        spawnPoint = GameObject.FindGameObjectWithTag("BullSpawnPoint").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWBashATKs = startTimeBTWBashATKs;
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

                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                SwitchToAxeATK();
                SwitchToBashATK();
                break;

            case State.HeadBash:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (timeBTWBashATKs <= 0)
                {
                    anim.SetTrigger("Bash");

                    timeBTWBashATKs = startTimeBTWBashATKs;
                }
                else
                {
                    timeBTWBashATKs -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToAxeATK();
                SwitchToDead();
                break;

            case State.AxeSwing:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (timeBTWAxeATKs <= 0)
                {
                    anim.SetTrigger("Axe");

                    timeBTWAxeATKs = startTimeBTWAxeATKs;
                }
                else
                {
                    timeBTWAxeATKs -= Time.deltaTime;
                }

                SwitchToBashATK();
                SwitchToChasing();
                SwitchToDead();
                break;

            case State.SpikeSummon:
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("Die");
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                if(isOnTut)
                {
                    TutorialTriggerController.Instance.SecondGateTriggerOut();
                }
                break;
        }
    }

    //STATES
    void SwitchToChasing()
    {
        if(Vector2.Distance(transform.position, player.position) > axeDistance && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToAxeATK()
    {
        if(Vector2.Distance(player.position, transform.position) <= axeDistance && Vector2.Distance(player.position, transform.position) > bashDistance && health > 0)
        {
            state = State.AxeSwing;
        }
    }
    void SwitchToBashATK()
    {
        if(Vector2.Distance(player.position, transform.position) <= bashDistance && health > 0)
        {
            state = State.HeadBash;
        }
    }
    void SwitchToSummonATK()
    {
        if(health == 40)
        {
            state = State.SpikeSummon;
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            state = State.Dead;
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);    
    }

    void BeginCombat()
    {
        SwitchToChasing();
        SwitchToAxeATK();
        SwitchToBashATK();
    }

    //FLIP
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

    //MELEE
    public void DamagePlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= bashDistance)
        {
            GameManager.instance.TakeDamage(5);
        }
    }

    //SPIKES
    public void SummonSpike()
    {
        Instantiate(bullSpike, transform.position, Quaternion.identity);
    }

    //HEALTH
    public void TakeDamage()
    {
        int damage = 10;
        health -= damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackHit"))
        {
            TakeDamage();
        }
    }
}
