using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Chasing,
        Dead
    }

    private State state;

    public Transform player;

    public Rigidbody2D rb;

    public Animator anim;

    public int health;

    public float chasingSpeed;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        SwitchToChasing();
    }

    void SwitchToChasing()
    {
        if(health > 0)
        {
            state = State.Chasing;
        }
    }
    void SwitchToDead()
    {
        if(health <= 0)
        {
            state = State.Dead;
        }
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
