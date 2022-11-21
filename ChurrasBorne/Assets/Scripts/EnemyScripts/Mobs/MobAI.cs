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

    private State state;

    public Rigidbody2D rb;
    public Animator anim;

    public Transform player;
    private Vector3 target;
    public Vector3 dashTarget;

    public GameObject projectile;

    public float agroDistance, meleeDistance, canDashDistance, dashMeleeDistance, chaseDistance, chasingSpeed, dashingSpeed, startTimeBTWAttacks, startTimeBTWShots, startStunTime, startDashRecoveryTime;
    private float TimeBTWAttacks, timeBTWShots, stunTime, dashRecoveryTime;

    public int maxHealth;
    public int currentHealth;

    public bool isASpitter, isADasher;
    private bool canDash = false, isDashing = false;

    public bool isOnTutorial, isOnFaseUm, isOnFaseDois;

    private float yOffset = 1.6f;

    private void Awake()
    {
        state = State.Idling;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);

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

                transform.position = Vector2.MoveTowards(transform.position, target, chasingSpeed * Time.deltaTime);

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

                if (Vector2.Distance(transform.position, target) <= dashMeleeDistance && isDashing == true)
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

                if(isOnTutorial)
                {
                    EnemyControlTutorial.Instance.KilledEnemy(gameObject);
                }
                else if(isOnFaseUm)
                {
                    EnemyControl.Instance.KilledEnemy(gameObject);
                }

                Destroy(gameObject, 1.5f);
                break;
        }

        //DASH
        if(isDashing == false)
        {
            dashTarget = target;

            Vector3 fator = target - transform.position;

            dashTarget.x = target.x + fator.x * 2;

            dashTarget.y = target.y + fator.y * 2;
        }
    }

    private void FixedUpdate()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);
    }

    //STATES
    void SwitchToChasing()
    {
        if(Vector2.Distance(transform.position, target) <= agroDistance && Vector2.Distance(transform.position, target) > meleeDistance && currentHealth > 0 && isASpitter == false)    
        {
            state = State.Chasing;
        }
        else if(Vector2.Distance(transform.position, target) <= chaseDistance && Vector2.Distance(transform.position, target) > meleeDistance && currentHealth > 0 && isASpitter == true)
        {
            state = State.Chasing;
        }
    }
    void SwitchToIdling()
    {
        if(Vector2.Distance(transform.position, target) > agroDistance && currentHealth > 0)
        {
            state = State.Idling;
        }
    }
    void SwitchToAttacking()
    {
        if(Vector2.Distance(transform.position, target) <= meleeDistance && currentHealth > 0)
        {
            state = State.Attacking;
        }
    }
    void SwitchToShooting()
    {
        if(Vector2.Distance(transform.position, target) <= agroDistance && Vector2.Distance(transform.position, target) > chaseDistance && currentHealth > 0 && isASpitter == true)
        {
            state = State.Shooting;
        }
    }
    void SwitchToDashing()
    {
        if(Vector2.Distance(transform.position, target) < canDashDistance && isADasher == true)
        {
            canDash = true;
        }
        else if(Vector2.Distance(transform.position, target) >= canDashDistance && canDash == true && isADasher == true)
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
        if(Vector2.Distance(transform.position, target) <= meleeDistance && isDashing == false)
        {
            GameManager.instance.TakeDamage(5, 0.25f);
        }
        else if(Vector2.Distance(transform.position, target) <= dashMeleeDistance && isDashing == true)
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
        if (target.x < transform.position.x && isDashing == false)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (target.x > transform.position.x && isDashing == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //HEALTH
    public void TakeDamage()
    {
        if (currentHealth >= 0)
        {
            anim.SetTrigger("Hit");
        }

        int damage;

        if(isOnTutorial)
        {
            damage = 5;
        }
        else
        {
            damage = 10;    
        }

        currentHealth -= damage;

        state = State.Stunned;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.TakeDamage(5);
        }
    }
}
