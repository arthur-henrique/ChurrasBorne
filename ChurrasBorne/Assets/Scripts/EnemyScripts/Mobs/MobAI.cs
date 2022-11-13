using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    private enum State
    {
        Idling,
        Chasing,
        Attacking,
        Shooting,
        Stunned,
        Dashing,
        RecoveringFromDash,
        Dead
    }

    private static State state;

    public Rigidbody2D rb;
    public Animator anim;

    public Transform player;
    public Vector3 dashTarget;

    public GameObject projectile;

    public float agroDistance, meleeDistance, canDashDistance, dashMeleeDistance, chaseDistance, chasingSpeed, dashingSpeed, startTimeBTWAttacks, startTimeBTWShots, startStunTime, startDashRecoveryTime;
    private float TimeBTWAttacks, timeBTWShots, stunTime, dashRecoveryTime;

    public int maxHealth;
    int currentHealth;

    public bool isASpitter, isADasher;
    private bool canDash = false, isDashing = false;

    private void Awake()
    {
        state = State.Idling;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        TimeBTWAttacks = startTimeBTWAttacks;

        timeBTWShots = startTimeBTWShots;

        stunTime = startStunTime;

        dashRecoveryTime = startDashRecoveryTime;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Idling:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                SwitchToChasing();
                SwitchToShooting();
                break;

            case State.Chasing:
                Flip();

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                anim.SetBool("Walk", true);
                anim.SetBool("Idle", false);

                SwitchToIdling();
                SwitchToAttacking();
                SwitchToShooting();
                SwitchToDashing();
                break;

            case State.Attacking:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if(TimeBTWAttacks <= 0)
                {
                    anim.SetTrigger("Melee");

                    TimeBTWAttacks = startTimeBTWAttacks;
                }
                else
                {
                    TimeBTWAttacks -= Time.deltaTime;
                }

                SwitchToChasing();
                break;

            case State.Shooting:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if(timeBTWShots <= 0)
                {
                    anim.SetTrigger("Ranged");

                    timeBTWShots = startTimeBTWShots;
                }
                else
                {
                    timeBTWShots -= Time.deltaTime; 
                }

                SwitchToIdling();
                SwitchToChasing();
                break;

            case State.Dashing:
                transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashingSpeed * Time.deltaTime);

                canDash = false;
                isDashing = true;

                anim.SetBool("Dash", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                if (dashTarget.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (dashTarget.x > transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                if (Vector2.Distance(transform.position, player.position) <= dashMeleeDistance && isDashing == true)
                {
                    anim.SetTrigger("DashMelee");

                    SwitchToRecoveringFromDash();
                }
                else if (transform.position.x == dashTarget.x && transform.position.y == dashTarget.y && isDashing == true)
                {
                    SwitchToRecoveringFromDash();
                    
                    isDashing = false;
                }
                break;

            case State.RecoveringFromDash:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);

                if (dashRecoveryTime <= 0)
                {
                    SwitchToIdling();
                    SwitchToChasing();
                    SwitchToAttacking();

                    dashRecoveryTime = startDashRecoveryTime;
                }
                else
                {
                    dashRecoveryTime -= Time.deltaTime;
                }
                break;

            case State.Stunned:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if(stunTime <= 0)
                {
                    SwitchToChasing();
                    SwitchToIdling();
                    SwitchToAttacking();
                    SwitchToDead();
                }
                else
                {
                    stunTime -= Time.deltaTime; 
                }
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("Die");
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);

                Destroy(gameObject, 1.5f);
                break;
        }

        //DASH
        if(isDashing == false)
        {
            dashTarget = player.transform.position;

            Vector3 fator = player.position - transform.position;

            dashTarget.x = player.position.x + fator.x * 2;

            dashTarget.y = player.position.y + fator.y * 2;
        }
    }
    
    //STATES
    void SwitchToChasing()
    {
        if(Vector2.Distance(transform.position, player.position) <= agroDistance && Vector2.Distance(transform.position, player.position) > meleeDistance && currentHealth > 0 && isASpitter == false)    
        {
            state = State.Chasing;
        }
        else if(Vector2.Distance(transform.position, player.position) <= chaseDistance && Vector2.Distance(transform.position, player.position) > meleeDistance && currentHealth > 0 && isASpitter == true)
        {
            state = State.Chasing;
        }
    }
    void SwitchToIdling()
    {
        if(Vector2.Distance(transform.position, player.position) > agroDistance && currentHealth > 0)
        {
            state = State.Idling;
        }
    }
    void SwitchToAttacking()
    {
        if(Vector2.Distance(transform.position, player.position) <= meleeDistance && currentHealth > 0)
        {
            state = State.Attacking;
        }
    }
    void SwitchToShooting()
    {
        if(Vector2.Distance(transform.position, player.position) <= agroDistance && Vector2.Distance(transform.position, player.position) > chaseDistance && currentHealth > 0 && isASpitter == true)
        {
            state = State.Shooting;
        }
    }
    void SwitchToDashing()
    {
        if(Vector2.Distance(transform.position,player.position) < canDashDistance && isADasher == true)
        {
            canDash = true;
        }
        else if(Vector2.Distance(transform.position, player.position) >= canDashDistance && canDash == true && isADasher == true)
        {
            state = State.Dashing;
        }
    }
    void SwitchToRecoveringFromDash()
    {
        state = State.RecoveringFromDash;
    }
    void SwitchToDead()
    {
        if(currentHealth <= 0)
        {
            state = State.Dead;
        }
    }

    //MELEE
    void DamagePlayer()
    {
        if(Vector2.Distance(transform.position,player.position) <= meleeDistance && isDashing == false)
        {
            GameManager.instance.TakeDamage(5);
        }
        else if(Vector2.Distance(transform.position, player.position) <= dashMeleeDistance && isDashing == true)
        {
            GameManager.instance.TakeDamage(10);

            isDashing = false;
        }
    }

    //RANGED
    void InstantiateProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    //FLIP
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

    //HEALTH
    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hit");

        currentHealth -= damage;

        state = State.Stunned;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackHit"))
        {
            TakeDamage(5);
        }
    }
}