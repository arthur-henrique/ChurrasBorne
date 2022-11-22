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
        DashATK,
        RecoveringFromDash,
        SummoningSpikes,
        Idling,
        Dead       
    }

    public Transform player;
    private Vector2 dashTarget;

    public float chasingSpeed, dashingSpeed, stompDistance, dashDistance, dashATKDistance, canDashDistance;

    public float timeBTWStompATKs, startSpikeSpawnTime, startStopSummoningTime, dashRecoveryTime;
    private float currentTimeBTWStompATKs, spikeSpawnTime, stopSummoningTime, currentDashRecoveryTime;

    private bool isDashing = false;

    public int health;

    public Rigidbody2D rb;

    public Animator anim;

    private static State state;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWStompATKs = 0.5f;

        spikeSpawnTime = startSpikeSpawnTime; 

        stopSummoningTime = startStopSummoningTime;

        currentDashRecoveryTime = dashRecoveryTime;
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

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                anim.SetBool("Walk", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Idle", false);

                SwitchToStomping();
                SwitchToDashing();
                SwitchToDead();
                break;

            case State.Stomping:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if(timeBTWStompATKs <= 0)
                {
                    anim.SetTrigger("Stomp");

                    timeBTWStompATKs = currentTimeBTWStompATKs;
                }
                else
                {
                    timeBTWStompATKs -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToDashing();
                SwitchToDead();
                break;

            case State.Dashing:
                isDashing = true;

                DashFlip();

                transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashingSpeed * Time.deltaTime);

                anim.SetBool("Dash", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                if (Vector2.Distance(transform.position, player.position) <= dashATKDistance && isDashing == true)
                {
                    state = State.DashATK;
                }
                else if (transform.position.x == dashTarget.x && transform.position.y == dashTarget.y && isDashing == true)
                {
                    state = State.RecoveringFromDash;
                }
                break;
                
            case State.DashATK:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("DashATK");
                break;
                
            case State.RecoveringFromDash:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                isDashing = false;

                if (currentDashRecoveryTime <= 0)
                {
                    SwitchToChasing();
                    SwitchToDashing();
                    SwitchToStomping();
                    SwitchToDead();

                    currentDashRecoveryTime = dashRecoveryTime;
                }
                else
                {
                    currentDashRecoveryTime -= Time.deltaTime;
                }
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("Die");

                anim.SetBool("Idle", false);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);
                break;
        }

        if (isDashing == false)
        {
            dashTarget = player.transform.position;

            Vector3 fator = player.position - transform.position;

            dashTarget.x = player.position.x + fator.x * 2;

            dashTarget.y = player.position.y + fator.y * 2;
        }
    }

    void SwitchToChasing()
    {
        if(Vector2.Distance(transform.position, player.position) > dashDistance && health > 0)
        {
            state = State.Chasing;
        }
        else if(Vector2.Distance(transform.position, player.position) <= canDashDistance && Vector2.Distance(transform.position, player.position) > stompDistance && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToDashing()
    {
        if(Vector2.Distance(transform.position, player.position) <= dashDistance && Vector2.Distance(transform.position, player.position) > canDashDistance && health > 0)
        {
            state = State.Dashing;
        }
    }
    void SwitchToStomping()
    {
        if(Vector2.Distance(transform.position, player.position) <= stompDistance && health > 0)
        {
            state = State.Stomping;
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
        SwitchToDashing();
        SwitchToStomping();
    }

    void StompDamage()
    {
        if(Vector2.Distance(transform.position, player.position) <= stompDistance)
        {
            GameManager.instance.TakeDamage(5);
        }
    }

    void DashDamage()
    {
        if(Vector2.Distance(transform.position, player.position) <= dashATKDistance && isDashing == true)
        {
            GameManager.instance.TakeDamage(10);
        }

        state = State.RecoveringFromDash;
    }

    void Flip()
    {
        if (player.position.x < transform.position.x && isDashing == false)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x && isDashing == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void DashFlip()
    {
        if (dashTarget.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (dashTarget.x > transform.position.x)
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
