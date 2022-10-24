using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAI : MonoBehaviour
{
    public Transform player;

    public float agroDistance, stopDistance, speed, attackDistance, dashDistance, startDashTime, dashSpeed, startTimeBTWAttacks;
    private float timeBTWAttacks, dashTime;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    private int direction;

    void Start()
    {
        //Para MELEE
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWAttacks = startTimeBTWAttacks;

        //Para HEALTH
        currentHealth = maxHealth;

        //Para ON CONTACT
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //MOVEMENT
        if (Vector2.Distance(transform.position, player.position) < agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;
        }

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        //MELEE
        if (Vector2.Distance(transform.position, player.position) < attackDistance && timeBTWAttacks <= 0)
        {
            GameManager.instance.TakeDamage(5);
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, player.position) > dashDistance)
        {
            if (direction == 0)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    direction = 1;
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    direction = 2;
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    animator.SetTrigger("Melee");

                    if (direction == 1)
                    {
                        rb.velocity = Vector2.left * dashSpeed;
                    }
                    else if (direction == 2)
                    {
                        rb.velocity = Vector2.right * dashSpeed;
                    }
                }
            }
        }
    }


    //ON CONTACT
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bodyCollider.isTrigger = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bodyCollider.isTrigger = false;
        }
    }


    //HEALTH
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
