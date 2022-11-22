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
        Dead
    }
    
    private State state;

    public Transform player;

    public Rigidbody2D rb;

    public GameObject web;   

    public Animator anim;

    public int health;

    public float chasingSpeed, shootingDistance, timeBTWShots;
    private float currentTimeBTWShots;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentTimeBTWShots = 0.5f;
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
                SwitchToDead();
                break;

            case State.Shooting:
                Flip();

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);

                rb.velocity = Vector2.zero;

                if(currentTimeBTWShots <= 0)
                {
                    anim.SetTrigger("ShootWeb");

                    currentTimeBTWShots = timeBTWShots;
                }
                else
                {
                    currentTimeBTWShots -= Time.deltaTime;
                }

                SwitchToChasing();
                SwitchToDead();
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

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
        if(Vector2.Distance(transform.position, player.position) > shootingDistance && health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToShooting()
    {
        if(Vector2.Distance(transform.position, player.position) <= shootingDistance && health > 0)
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

    void ShootWeb()
    {
        Instantiate(web, transform.position, Quaternion.identity);
    }

    void TakeDamage()
    {
        int damage = 10;
        health -= damage;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
