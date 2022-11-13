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
    public GameObject spikeSummoner;

    public Animator anim;

    public int maxHealth;
    int currentHealth;

    public float chasingSpeed, bashDistance, startTimeBTWBashATKs, axeDistance, startTimeBTWAxeATKs, startTimeToSpawn, startTimeToSummonSpikes;
    private float timeBTWBashATKs, timeBTWAxeATKs, timeToSpawn, timeToSummonSpikes;

    public bool isOnFaseUm;

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

        timeToSpawn = startTimeToSpawn;

        timeBTWBashATKs = startTimeBTWBashATKs;

        timeBTWAxeATKs = startTimeBTWAxeATKs;

        //PARA SPIKE SUMMON
        //spikes0 = GameObject.FindGameObjectsWithTag("BullSpikes0")

        //Para HEALTH
        currentHealth = maxHealth;

        //Para ON CONTACT
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Spawning:
                
                anim.SetTrigger("Spawn");

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (timeToSpawn <= 0)
                {
                    SwitchToChasing();
                    SwitchToAxeATK();
                    SwitchToBashATK();
                }
                else
                {
                    timeToSpawn -= Time.deltaTime;
                }
                break;

            case State.Chasing:
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
                transform.position = spawnPoint.position;

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                timeToSpawn -= Time.deltaTime;

                if(timeToSummonSpikes == 15)
                {
                    spikeSummoner.GetComponent<BullSpikeSummoner>().firstSpikePattern = true;
                }
                else if(timeToSummonSpikes == 10)
                {
                    spikeSummoner.GetComponent<BullSpikeSummoner>().firstSpikePattern = false;
                    spikeSummoner.GetComponent<BullSpikeSummoner>().secondSpikePattern = true;
                }
                else if(timeToSummonSpikes == 5)
                {
                    spikeSummoner.GetComponent<BullSpikeSummoner>().secondSpikePattern = false;
                    spikeSummoner.GetComponent<BullSpikeSummoner>().thirdSpikePattern = true;
                }
                else if(timeToSummonSpikes == 0)
                {
                    spikeSummoner.GetComponent<BullSpikeSummoner>().thirdSpikePattern = false;

                    SwitchToChasing();
                    SwitchToAxeATK();
                    SwitchToBashATK();
                }

                SwitchToDead();
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("Die");
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                Destroy(gameObject, 1.5f);
                break;
        }
    }

    //STATES
    void SwitchToChasing()
    {
        if(Vector2.Distance(transform.position, player.position) > axeDistance && currentHealth > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToAxeATK()
    {
        if(Vector2.Distance(player.position, transform.position) <= axeDistance && Vector2.Distance(player.position, transform.position) > bashDistance && currentHealth > 0)
        {
            state = State.AxeSwing;
        }
    }
    void SwitchToBashATK()
    {
        if(Vector2.Distance(player.position, transform.position) <= bashDistance && currentHealth > 0)
        {
            state = State.HeadBash;
        }
    }
    void SwitchToSummonATK()
    {
        if(currentHealth == 40)
        {
            state = State.SpikeSummon;
        }
    }
    void SwitchToDead()
    {
        if(currentHealth <= 0)
        {
            state = State.Dead;
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
}
