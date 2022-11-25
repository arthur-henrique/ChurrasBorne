using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Ascending,
        Chasing,
        Descendng,
        LongRange,
        CloseRange,
        Dead
    }

    private State state;

    public Transform player;

    public Rigidbody2D rb;

    public Animator anim;

    public int health;

    public float chasingSpeed, chaseDistance, timeBTWLRATKs, closeRangeDistance, timeBTWCRATKs;
    private float currentTimeBTWLRATKs, currentTimeBTWCRATKs;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentTimeBTWLRATKs = 0.5f;

        currentTimeBTWCRATKs = 0.5f;
    }

    void Update()
    {
        switch(state)
        {
            case State.Spawning:
                Flip();
                break;

            case State.Ascending:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetTrigger("Ascend");
                anim.SetBool("Float", true);
                anim.SetBool("Idle", false);
                break;

            case State.Chasing:
                Flip();

                anim.SetBool("Float", true);
                anim.SetBool("Idle", false);

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                SwitchToDead();
                break;

            case State.Descendng:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetTrigger("Descend");
                anim.SetBool("Float", true);
                anim.SetBool("Idle", false);
                break;

            case State.LongRange:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Float", false);

                if(currentTimeBTWLRATKs <= 0)
                {
                    anim.SetTrigger("LR");

                    currentTimeBTWLRATKs = timeBTWLRATKs;
                }
                else
                {
                    currentTimeBTWLRATKs -= Time.deltaTime;
                }

                SwitchToAscending();
                SwitchToCloseRange();
                SwitchToDead();
                break;

            case State.CloseRange:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Float", false);

                if (currentTimeBTWCRATKs <= 0)
                {
                    anim.SetTrigger("CR");

                    currentTimeBTWCRATKs = timeBTWCRATKs;
                }
                else
                {
                    currentTimeBTWCRATKs -= Time.deltaTime;
                }

                SwitchToLongRange();
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

    void BeginCombat()
    {
        SwitchToAscending();
        SwitchToLongRange();
    }

    void SwitchToAscending()
    {
        if (health > 0 && Vector2.Distance(transform.position, player.position) > chaseDistance)
        {
            state = State.Ascending;
        }
    }
    void SwitchToChasing()
    {
        state = State.Chasing;
    }
    void SwitchToDescending()
    {
        if(health > 0 && Vector2.Distance(transform.position, player.position) <= chaseDistance)
        {
            state = State.Descendng;
        }
    }
    void SwitchToLongRange()
    {
        if(health > 0 && Vector2.Distance(transform.position, player.position) <= chaseDistance && Vector2.Distance(transform.position, player.position) > closeRangeDistance)
        {
            state = State.LongRange;
        }
    }
    void SwitchToCloseRange()
    {
        if(health > 0 && Vector2.Distance(transform.position, player.position) <= closeRangeDistance)
        {
            state = State.CloseRange;
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            state = State.Dead;
        }
    }

    void ToAscendOrToBeatPlayerUp()
    {
        SwitchToAscending();
        SwitchToLongRange();
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
