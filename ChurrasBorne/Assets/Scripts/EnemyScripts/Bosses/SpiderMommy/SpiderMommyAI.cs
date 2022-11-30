using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMommyAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Teleporting,
        Shooting,
        ShootingWeb,
        Slaping,
        Dead
    }
    
    private State state;

    public Transform player;

    public Rigidbody2D rb;

    public GameObject web;   

    public Animator anim;

    public int health;

    public bool isSpiderGranny;

    private bool isAlreadyDying = false;

    public float chasingSpeed, rangedDistanceI, rangedDistanceII, meleeDistance, timeBTWShots, timeBTWWebShots, timeBTWSlaps;
    private float currentTimeBTWShots, currentTimeBTWSlaps, currentTimeBTWWebShots;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentTimeBTWShots = .5f;
        currentTimeBTWWebShots = .5f;
        currentTimeBTWSlaps = .5f;
    }

    void Update()
    {
        switch(state)
        {
            case State.Spawning:
                Flip();
                break;

            case State.Chasing:
                Flip();

                anim.SetBool("Walk", true);
                anim.SetBool("Idle", false);

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                SwitchToShooting();
                SwitchToSlaping();
                break;

            case State.Shooting:
                Flip();

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                rb.velocity = Vector2.zero;

                if(currentTimeBTWShots <= 0)
                {
                    anim.SetTrigger("Ranged");

                    currentTimeBTWShots = timeBTWShots;
                }
                else
                {
                    currentTimeBTWShots -= Time.deltaTime;
                }

                if (currentTimeBTWWebShots <= 0)
                {
                    state = State.ShootingWeb;

                    currentTimeBTWWebShots = timeBTWWebShots;
                }
                else
                {
                    currentTimeBTWWebShots -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToSlaping();
                break;

            case State.ShootingWeb:
                Flip();

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                rb.velocity = Vector2.zero;

                anim.SetTrigger("WebShot");

                state = State.Shooting;
                break;

            case State.Slaping:
                Flip();

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                rb.velocity = Vector2.zero;

                if (currentTimeBTWSlaps <= 0)
                {
                    anim.SetTrigger("Melee");

                    currentTimeBTWSlaps = timeBTWSlaps;
                }
                else
                {
                    currentTimeBTWSlaps -= Time.deltaTime;
                }

                SwitchToChasing();
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                isAlreadyDying = true;

                anim.SetTrigger("Die");
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", false);
                break;
        }
    }

    void BeginCombat()
    {
        SwitchToChasing();
        SwitchToShooting(); 
    }

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
    void SwitchToShooting()
    {
        if (Vector2.Distance(transform.position, player.position) <= rangedDistanceII && Vector2.Distance(transform.position, player.position) > rangedDistanceI && health > 0)
        {
            state = State.Shooting;
        }
    }
    void SwitchToSlaping()
    {
        if (Vector2.Distance(transform.position, player.position) <= meleeDistance && health > 0)
        {
            state = State.Slaping;
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            state = State.Dead;
        }
    }

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

    void ShootWeb()
    {
        Instantiate(web, transform.position, Quaternion.identity);
    }

    void TakeDamage()
    {
        int damage = 10;
        health -= damage;

        if (!isAlreadyDying)
        {
            SwitchToDead();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
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
