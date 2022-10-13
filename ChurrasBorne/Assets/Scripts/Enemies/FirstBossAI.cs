using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossAI : MonoBehaviour
{
    public Transform player;

    public Transform position1;
    public Transform position2;
    public Transform position3;

    public float agroDistance, stopDistance, distanceToTarget, dashDistance, speed;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    private float timeBTWAttacks;
    public float startTimeBTWAttacks;

    public Animator animator;

    private Vector2 target;

    private Vector2 strikePos1;
    private Vector2 strikePos2;
    private Vector2 strikePos3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;

        timeBTWAttacks = startTimeBTWAttacks;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //MOVIMENTO

        if (Vector2.Distance(transform.position, player.position) < agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;
        }

        //ATAQUE

        if (Vector2.Distance(position1.position, player.position) < distanceToTarget && timeBTWAttacks <= 0)
        {
            GameManager.instance.TakeDamage(20);
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }
        
        if (Vector2.Distance(position2.position, player.position) < distanceToTarget && timeBTWAttacks <= 0)
        {
            GameManager.instance.TakeDamage(20);
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }

        if (Vector2.Distance(position3.position, player.position) < distanceToTarget && timeBTWAttacks <= 0)
        {
            GameManager.instance.TakeDamage(20);
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(20);
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
}
