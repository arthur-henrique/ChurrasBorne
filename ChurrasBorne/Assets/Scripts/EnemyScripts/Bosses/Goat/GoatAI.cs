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
        RecoveringFromDash,
        SummoningSpikes,
        Idling,
        Dead       
    }
    private State state;

    public Transform player;
    private Vector2 dashTarget;

    public float chasingSpeed, dashingSpeed, meleeDistance, dashDistance, dashATKDistance, canDashDistance;

    public float startTimeBTWMeleeATKs, startDashRecoveryTime;
    private float timeBTWMeleeATKs, dashRecoveryTime, timeToDie;

    private bool isDashing = false, isAlreadyDying = false, isAlive = true;

    public int health;

    public Rigidbody2D rb;
    public Animator anim;

    public bool isP1, isP2;
    public Collider2D coll;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWMeleeATKs = .5f;

        timeToDie = .1f;

        dashRecoveryTime = startDashRecoveryTime;
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
                break;

            case State.Stomping:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if(timeBTWMeleeATKs <= 0)
                {
                    anim.SetTrigger("Stomp");

                    timeBTWMeleeATKs = startTimeBTWMeleeATKs;
                }
                else
                {
                    timeBTWMeleeATKs -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToDashing();
                break;

            case State.Dashing:
                isDashing = true;   

                DashFlip();

                transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashingSpeed * Time.deltaTime);

                anim.SetBool("Dash", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                if (Vector2.Distance(transform.position, player.position) <= dashATKDistance && isDashing)
                {
                    isDashing = false;

                    anim.SetTrigger("DashATK");

                    state = State.RecoveringFromDash;
                }
                else if (transform.position.x == dashTarget.x && transform.position.y == dashTarget.y && isDashing)
                {
                    isDashing = false;

                    state = State.RecoveringFromDash;
                }
                break;
                
            case State.RecoveringFromDash:
                rb.velocity = Vector2.zero;

                Debug.Log("rawr x3");

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if (dashRecoveryTime <= 0)
                {
                    SwitchToStomping();
                    SwitchToChasing();
                    SwitchToDashing();

                    dashRecoveryTime = startDashRecoveryTime;
                }
                else
                {
                    dashRecoveryTime -= Time.deltaTime;
                }
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                isAlive = false;
                isAlreadyDying = true;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if (timeToDie <= 0)
                {
                    anim.SetTrigger("Die");

                    timeToDie = 1000;
                }
                else
                {
                    timeToDie -= Time.deltaTime;
                }

                if(isP1)
                {
                    GameManager.instance.SetHasCleared(0, true);
                }
                else if (isP2)
                {
                    GameManager.instance.SetHasCleared(1, true);
                }
                coll.enabled = true;
                break;

            case State.Idling:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);
                break;
        }

        if (!isDashing)
        {
            dashTarget = player.transform.position;

            Vector3 fator = player.position - transform.position;

            dashTarget.x = player.position.x + fator.x * 2;

            dashTarget.y = player.position.y + fator.y * 2;
        }
    }

    void SwitchToChasing()
    {
        if (Vector2.Distance(transform.position, player.position) > dashDistance && health > 0)
        {
            state = State.Chasing;
        }
        else if (Vector2.Distance(transform.position, player.position) <= canDashDistance && Vector2.Distance(transform.position, player.position) > meleeDistance && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToDashing()
    {
        if (Vector2.Distance(transform.position, player.position) <= dashDistance && Vector2.Distance(transform.position, player.position) > canDashDistance && health > 0)
        {
            state = State.Dashing;
        }
    }
    void SwitchToStomping()
    {
        if (Vector2.Distance(transform.position, player.position) <= meleeDistance && health > 0)
        {
            state = State.Stomping;
        }
    }
    void SwitchToDead()
    {
        if (health <= 0 && isAlive)
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
        if(Vector2.Distance(transform.position, player.position) <= meleeDistance)
        {
            GameManager.instance.TakeDamage(5);
        }
    }

    void DashDamage()
    {
        if (Vector2.Distance(transform.position, player.position) <= dashATKDistance)
        {
            GameManager.instance.TakeDamage(10);
        }
    }

    void Flip()
    {
        if (player.position.x < transform.position.x && !isDashing)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x && !isDashing)
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

    void DestroySelf()
    {
        Destroy(gameObject);    
    }

    public void TakeDamage()
    {
        int damage = 10;
        health -= damage;

        if (!isAlreadyDying)
        {
            SwitchToDead();
        }
    }
}
