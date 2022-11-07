using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GoatAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Stomping,
        Dashing,
        DashAttack,
        SummoningSpikes,
        Idling,
        WasHurt,
        Dead       
    }

    public Transform player;

    private bool mustStomp, mustDash, mustDashAttack, mustSummonSpikes;

    public float walkingSpeed, dashingSpeed, stompDistance, dashDistance, dashATKDistance;

    public float startSpawnTime, startDashTime, startStompTime, startSummonPrepTime, startSpikeSpawnTime, startStopSummoningTime;
    private float spawnTime, dashTime, stompTime, summonPrepTime, spikeSpawnTime, stopSummoningTime;

    public int maxHealth;
    int currentHealth;

    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject goat;

    public Animator anim;

    private static State state;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        spawnTime = startSpawnTime;

        dashTime = startDashTime;

        stompTime = startStompTime;

        spikeSpawnTime = startSpikeSpawnTime; 

        summonPrepTime = startSummonPrepTime;

        stopSummoningTime = startStopSummoningTime;


        //Para HEALTH
        currentHealth = maxHealth;
    }

    void Update()
    {
        //FLIP
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        switch (state)
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
                
            case State.Chasing:
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", true);
                transform.position = Vector2.MoveTowards(transform.position, player.position, walkingSpeed * Time.deltaTime);

                if(Vector2.Distance(transform.position, player.position) <= stompDistance)
                {
                    mustStomp = true;
                    state = State.Stomping;
                }
                /*
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
                */
                break;

            case State.Stomping:
                rb.velocity = Vector2.zero;
                anim.SetBool("Walking", false);
                anim.SetBool("Idle", true);
                if (stompTime <= 0 && mustStomp == true)
                {
                    anim.SetTrigger("Stomp");

                    GameManager.instance.TakeDamage(20);

                    stompTime = startStompTime;
                }
                else
                {
                    stompTime -= Time.deltaTime;    
                }
                if(Vector2.Distance(transform.position, player.position) > stompDistance && mustStomp == true)
                {
                    mustStomp = false;
                    state = State.Chasing;
                }
                break;

            case State.WasHurt:
                TakeDamage(30);
                if(currentHealth <= 0)
                {
                    state = State.Dead;
                }
                else
                {
                    state = State.Chasing;
                }
                break;

            case State.Dead:
                anim.SetBool("Dead", true);
                anim.SetBool("Walking", false);
                anim.SetBool("Idle", false);
                GetComponent<Collider2D>().enabled = false;
                this.enabled = false;
                break;

                /*
            case State.Idling:
                anim.SetBool("Idle", true);
                rb.velocity = Vector2.zero;
                break;
                */
        }
    }

    //ON CONTACT
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col.isTrigger = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col.isTrigger = false;
        }
    }

    //HEALTH
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackHit"))
        {
            state = State.WasHurt;
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hit");
    }
}
