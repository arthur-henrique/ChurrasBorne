using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TebasAI : MonoBehaviour
{
    public Transform player;

    public float agroDistance, stopDistance, speed, attackDistance, startTimeBTWAttacks, startStunTime;
    private float timeBTWAttacks, stunTime;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    public bool isOnTutorial, isOnFaseUm;

    private bool stunned = false;

    void Start()
    {
        //Para MELEE, STUN, FLIP
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWAttacks = startTimeBTWAttacks;

        stunTime = startStunTime;

        //Para HEALTH
        currentHealth = maxHealth;

        //Para ON CONTACT
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //MOVEMENT
        if (Vector2.Distance(transform.position, player.position) <= agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance && stunned == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            animator.SetBool("Walking", true);
            animator.SetBool("Idling", false);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance && stunned == false || Vector2.Distance(transform.position, player.position) > agroDistance && stunned == false)
        {
            rb.velocity = Vector2.zero;

            animator.SetBool("Walking", false);
            animator.SetBool("Idling", true);
        }

        //FLIP
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        
        //MELEE
        if (Vector2.Distance(transform.position, player.position) < attackDistance && timeBTWAttacks <= 0 && GameManager.instance.GetAlive() && stunned == false)
        {
            GameManager.instance.TakeDamage(5);
            
            animator.SetTrigger("Attack");
            
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }

        //STUN
        if (stunned == true)
        {
            rb.velocity = Vector2.zero;
            
            if (stunTime <= 0)
            {
                stunned = false;

                stunTime = startStunTime;
            }
            else
            {
                stunTime -= Time.deltaTime;
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
            if (isOnTutorial)
            {
                TakeDamage(10);
            }
            else
            {
                TakeDamage(20);
            }
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hit");

        stunned = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Idling", false);
        animator.SetBool("Death", true);
        
        GetComponent<Collider2D>().enabled = false;
        if (isOnTutorial)
        {
            EnemyControlTutorial.Instance.KilledEnemy(gameObject);
        }
        if (isOnFaseUm)
        {
            EnemyControl.Instance.KilledEnemy(gameObject);
            if (gameObject.CompareTag("BOSSP1"))
            {
                FaseUmTriggerController.Instance.P1Portal();
            }
            if (gameObject.CompareTag("BOSSP2"))
            {
                FaseUmTriggerController.Instance.P2Portal();
            }
        }
        this.enabled = false;
    }
}
