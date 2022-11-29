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
    private State state;

    public Transform player;
    public Rigidbody2D rb;
    public Animator anim;

    public GameObject bullSpikes;

    public int health;

    public float chasingSpeed, meleeDistance, startTimeBTWMeleeATKs, rangedDistanceI, rangedDistanceII, startTimeBTWRangedATKs;
    private float timeBTWMeleeATKs, timeBTWRangedATKs, timeToDie;

    public bool isOnTut, isAlive = true;

    private bool isAlreadyDying = false;

    private void Awake()
    {
        state = State.Spawning;
    }


    void Start()
    {
        //Para SPAWN, MOVEMENT, BASH, AXE
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWMeleeATKs = .5f;

        timeBTWRangedATKs = .5f;

        timeToDie = .1f;

        if (isOnTut)
        {
            health = 300;
        }
        else
        {
            health = 200;
        }

        HealthBar_Manager.instance.boss = this.gameObject;
        HealthBar_Manager.instance.refreshBoss = true;
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
                anim.SetBool("Idle", false);

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                SwitchToAxeATK();
                SwitchToBashATK();
                break;

            case State.HeadBash:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (timeBTWMeleeATKs <= 0)
                {
                    anim.SetTrigger("Bash");

                    timeBTWMeleeATKs = startTimeBTWMeleeATKs;
                }
                else
                {
                    timeBTWMeleeATKs -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToAxeATK();
                break;

            case State.AxeSwing:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (timeBTWRangedATKs <= 0)
                {
                    anim.SetTrigger("Axe");

                    timeBTWRangedATKs = startTimeBTWRangedATKs;
                }
                else
                {
                    timeBTWRangedATKs -= Time.deltaTime;
                }

                SwitchToBashATK();
                SwitchToChasing();
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                isAlive = false;
                isAlreadyDying = true;
                
                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                if (timeToDie <= 0)
                {
                    anim.SetTrigger("Die");

                    timeToDie = 10000;
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
    }

    //STATES
    void SwitchToChasing()
    {
        if (Vector2.Distance(transform.position, player.position) > rangedDistanceII && health > 0)
        {
            state = State.Chasing;
        }
        else if (Vector2.Distance(transform.position, player.position) <= rangedDistanceI && Vector2.Distance(transform.position, player.position) > meleeDistance)
        {
            state = State.Chasing;
        }
    }
    void SwitchToAxeATK()
    {
        if (Vector2.Distance(player.position, transform.position) <= rangedDistanceII && Vector2.Distance(player.position, transform.position) > rangedDistanceI && health > 0)
        {
            state = State.AxeSwing;
        }
    }
    void SwitchToBashATK()
    {
        if (Vector2.Distance(player.position, transform.position) <= meleeDistance && health > 0)
        {
            state = State.HeadBash;
        }
    }
    void SwitchToSummonATK()
    {
        if (health == 40)
        {
            state = State.SpikeSummon;
        }
    }
    void SwitchToDead()
    {
        if (health <= 0 && isAlive)
        {
            state = State.Dead;
            if (isOnTut)
            {
                TutorialTriggerController.Instance.SecondGateTriggerOut();
            }
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
        if (Vector2.Distance(transform.position, player.position) <= meleeDistance && isOnTut)
        {
            GameManager.instance.TakeDamage(15);
        }
        else if (Vector2.Distance(transform.position, player.position) <= meleeDistance && !isOnTut)
        {
            GameManager.instance.TakeDamage(10);
        }
    }

    //SPIKES
    public void SummonSpike()
    {
        Instantiate(bullSpikes, player.position, Quaternion.identity);
    }

    //HEALTH
    public void TakeDamage()
    {
        int damage;

        if (isOnTut)
        {
            damage = 5;
        }
        else
        {
            damage = 10;
        }

        health -= damage;

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
