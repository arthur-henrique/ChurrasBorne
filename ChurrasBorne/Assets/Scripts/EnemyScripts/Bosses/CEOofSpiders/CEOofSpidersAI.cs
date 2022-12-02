using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class CEOofSpidersAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Running,
        Shooting,
        WasHit,
        Dead
    }
    
    private State state;

    public Transform player;

    public Rigidbody2D rb;

    public GameObject web, spike, spiders, shootPoint;

    public Animator anim;

    public int health;

    public bool isSpiderGranny;

    private bool isAlreadyDying = false, isSpawningSpiders = false;

    public float speed, startStunTime, rangedDistanceI, rangedDistanceII, startTimeBTWWebShot;
    private float stunTime, timeBTWWebShots;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        stunTime = startStunTime;
        timeBTWWebShots = startTimeBTWWebShot;

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
                anim.SetBool("ATK1", false);

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                SwitchToShooting();
                break;

            case State.Running:
                Flip();

                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

                anim.SetBool("Walk", true);
                anim.SetBool("ATK1", false);

                SwitchToChasing();
                break;

            case State.Shooting:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("ATK1", true);
                anim.SetBool("Walk", false);

                if(timeBTWWebShots <= 0)
                {
                    anim.SetTrigger("ATK2");

                    timeBTWWebShots = startTimeBTWWebShot;
                }
                else
                {
                    timeBTWWebShots -= Time.deltaTime;
                }

                SwitchToRunning();
                SwitchToChasing();
                break;

            case State.WasHit:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                if (stunTime <= 0)
                { 
                    if (!isAlreadyDying)
                    {
                        SwitchToDead();
                    }

                    stunTime = startStunTime;
                }
                else
                {
                    stunTime -= Time.deltaTime;
                }
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                anim.SetTrigger("Die");

                isAlreadyDying = true;

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                break;
        }
    }

    void BeginCombat()
    {
        SwitchToRunning();
        SwitchToChasing();
        SwitchToShooting();
    }

    void SwitchToChasing()
    {
        if (Vector2.Distance(transform.position, player.position) > rangedDistanceII && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToRunning()
    {
        if (Vector2.Distance(transform.position, player.position) <= rangedDistanceI && health > 0)
        {
            state = State.Running;
        }
    }
    void SwitchToShooting()
    {
        if (Vector2.Distance(transform.position, player.position) > rangedDistanceI && Vector2.Distance(transform.position, player.position) <= rangedDistanceII && health > 0)
        {
            state = State.Shooting;
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

    void ShootSpike()
    {
        Instantiate(spike, shootPoint.transform.position, Quaternion.identity);
    }
    void ShootWeb()
    {
        Instantiate(web, shootPoint.transform.position, Quaternion.identity);
    }
    void SpawnSpiders()
    {
        Instantiate(spiders, transform.position, Quaternion.identity);
    }

    public void TakeDamage()
    {
        int damage = 10;
        health -= damage;

        anim.SetTrigger("Hit");

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
