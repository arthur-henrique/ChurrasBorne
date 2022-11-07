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

    public Collider2D col;
    public Rigidbody2D rb;
    public GameObject bull;

    public Animator anim;

    public int maxHealth;
    int currentHealth;

    public float speed, bashDistance, startBashTime, axeDistance, startAxeTime, startSpawnTime;
    private float bashTime, axeTime, spawnTime;

    private bool swingingAxe = false, bashingHead = false, SummoningSpikes = false;

    public bool isOnFaseUm;

    private static State state;


    private void Awake()
    {
        state = State.Spawning;
    }


    void Start()
    {
        //Para SPAWN, MOVEMENT, BASH, AXE
        player = GameObject.FindGameObjectWithTag("Player").transform;

        spawnTime = startSpawnTime;

        bashTime = startBashTime;

        axeTime = startAxeTime;

        //Para HEALTH
        currentHealth = maxHealth;

        //Para ON CONTACT
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            state = State.Dead;
        }

        switch (state)
        {
            case State.Spawning:
                
                anim.SetTrigger("Spawn");

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (spawnTime <= 0)
                {
                    bull.GetComponent<CapsuleCollider2D>().enabled = true;

                    state = State.Chasing;
                }
                else
                {
                    spawnTime -= Time.deltaTime;
                }
                break;

            case State.Chasing:
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);
                
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                if(Vector2.Distance(transform.position, player.position) <= bashDistance)
                {
                    bashingHead = true;
                    state = State.HeadBash;
                }
                break;

            case State.HeadBash:
                if(bashingHead == true)
                {
                    rb.velocity = Vector2.zero;

                    anim.SetBool("Idle", true);
                    anim.SetBool("Walk", false);

                    if (bashTime <= 0)
                    {
                        anim.SetTrigger("Bash");



                        bashTime = startBashTime;
                    }
                    else
                    {
                        bashTime -= Time.deltaTime;
                    }
                }

                if (Vector2.Distance(transform.position, player.position) > bashDistance)
                {
                    bashingHead = false;

                    state = State.Chasing;
                }
                break;

            case State.Dead:
                anim.SetBool("Die", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                GetComponent<Collider2D>().enabled = false;

                if (isOnFaseUm)
                {
                    EnemyControl.Instance.KilledEnemy(gameObject);
                }

                this.enabled = false;
                break;
        }

        //FLIP
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
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
            TakeDamage(20);
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}