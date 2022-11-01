using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GoatAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Idling,
        Chasing,
        Dashing,
        DashAttack,
        Stomping,
        SummoningSpikes,
        GettingHurt,
        Dead       
    }

    public Transform player;

    private Vector2 target;

    private bool mustStomp, mustDash, mustDashAttack, mustSummonSpikes;

    public float walkingSpeed, dashingSpeed;

    public float stompDistance, dashDistance, dashATKDistance;

    public float startSpawnTime, startDashTime, startStompTime, startSummonPrepTime, startSpikeSpawnTime, startStopSummoningTime;
    private float spawnTime, dashTime, stompTime, summonPrepTime, spikeSpawnTime, stopSummoningTime;

    public List<GameObject> Spikes = new List<GameObject>();
    int counter = 0;
    public int spikes = 6;
    private float delay = 2f;

    public int maxHealth;
    int currentHealth;

    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject goat;

    public Animator anim;

    public Animator playerAnim;

    private static State state;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        spawnTime = startSpawnTime;

        dashTime = startDashTime;

        stompTime = startStompTime;

        spikeSpawnTime = startSpikeSpawnTime; 

        currentHealth = maxHealth;

        summonPrepTime = startSummonPrepTime;

        stopSummoningTime = startStopSummoningTime;

        target = player.position;

        new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        switch(state)
        {
            case State.Spawning:
                anim.SetTrigger("Spawning");
                if (spawnTime <= 0)
                {
                    goat.GetComponent<CapsuleCollider2D>().enabled = true;
                    state = State.Chasing;
                }
                else
                {
                    spawnTime -= Time.deltaTime;
                }
                break;
            case State.Idling:
                anim.SetBool("Idle", true);
                rb.velocity = Vector2.zero;
                break;
            case State.Chasing:
                anim.SetBool("Walking", true);
                Vector2.MoveTowards(transform.position, player.position, walkingSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, player.position) <= stompDistance)
                {
                    state = State.Stomping;
                }
                if(Vector2.Distance(transform.position, player.position) > dashDistance)
                {
                    dashTime -= Time.deltaTime;
                }
                if (dashTime <= 0)
                {
                    state = State.Dashing;

                    dashTime = startDashTime;
                }
                if(currentHealth == 50)
                {
                    state = State.SummoningSpikes;
                }
                break;
            case State.Stomping:
                rb.velocity = Vector2.zero;
                if(stompTime <0)
                {
                    GameManager.instance.TakeDamage(20);

                    anim.SetTrigger("Stomp");

                    stompTime = startStompTime;
                }
                else
                {
                    stompTime -= Time.deltaTime;    
                }
                break;
            case State.Dashing:
                transform.position = Vector2.MoveTowards(transform.position, target, dashingSpeed * Time.deltaTime);

                anim.SetBool("Dashing", true);

                if (transform.position.x == target.x && transform.position.y == target.y || Vector2.Distance(transform.position, player.position) <= dashATKDistance) 
                {
                    state = State.DashAttack;
                }
                break;
            case State.SummoningSpikes:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("SummonPrep");

                summonPrepTime -= Time.deltaTime;

                if(summonPrepTime <= 0)
                {
                    anim.SetBool("Summoning", true);

                    stopSummoningTime -= Time.deltaTime;

                    if(spikeSpawnTime <= 0)
                    {
                        Spikes[counter].SetActive(true);
                        counter++;

                        spikeSpawnTime = startSpikeSpawnTime;
                    }
                    else
                    {
                        spikeSpawnTime -= Time.deltaTime;   
                    }

                    if(stopSummoningTime <= 0)
                    {
                        state = State.Chasing;
                    }
                }
                break;
        }
    }
}
