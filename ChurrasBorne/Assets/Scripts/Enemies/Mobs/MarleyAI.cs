using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarleyAI : MonoBehaviour
{
    public Transform player;

    public float agroDistance, stopDistance, speed, attackDistance, startTimeBTWAttacks;
    private float timeBTWAttacks, delay = 2f;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth, bites = 2;
    int currentHealth;

    public Animator animator;

    public Animator playerAnimator;

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

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        //MELEE
        if (Vector2.Distance(transform.position, player.position) < attackDistance && timeBTWAttacks <= 0 && GameManager.instance.GetAlive())
        {
            StartCoroutine(Bite());
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }

        IEnumerator Bite()
        {
            for (int i = 0; i < bites; i++)
            {
                GameManager.instance.TakeDamage(5, .5f);
                yield return new WaitForSeconds(delay);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackHit"))
        {
            if (!playerAnimator.GetBool("isHoldingSword"))
            {
                TakeDamage(15);
            }
            else
            {
                TakeDamage(34);
            }
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }


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
