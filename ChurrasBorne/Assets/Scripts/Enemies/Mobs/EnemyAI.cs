using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float agroDistance, stopDistance, speed, angySpeed, attackDistance, startTimeBTWAttacks, startStunTime;
    private float timeBTWAttacks, stunTime;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    public Animator playerAnimator;
    public bool isDead = false;
    public bool hasDeathEvents = false;

    private bool stunned = false;
    
    public bool angy = false;    

    public GameObject angyDetector;

    void Start()
    {
        //Para MELEE, ANGY PUNCH, MOVEMENT, ANGY MOVEMENT, STUN
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
        if (Vector2.Distance(transform.position, player.position) < agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance && stunned == false && angy == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance && angy == false)
        {
            transform.position = this.transform.position;

            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
        else if (Vector2.Distance(transform.position, player.position) > agroDistance && angy == false)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }

        //ANGY MOVEMENT
        if (Vector2.Distance(transform.position, player.position) < agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance && stunned == false && angy == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, angySpeed * Time.deltaTime);

            animator.SetBool("Walking", false);
            animator.SetBool("AngyWalking", true);
            animator.SetBool("Idle", false);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance && angy == true)
        {
            transform.position = this.transform.position;

            animator.SetBool("Walking", false);
            animator.SetBool("Idle", false);
            animator.SetBool("AngyIdle", true);
        }
        else if (Vector2.Distance(transform.position, player.position) > agroDistance && angy == true)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", false);
            animator.SetBool("AngyIdle", true);
        }

        //FLIP
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //MELEE
        if (Vector2.Distance(transform.position, player.position) < attackDistance && timeBTWAttacks <= 0 && GameManager.instance.GetAlive() && stunned == false && angy == false)
        {
            GameManager.instance.TakeDamage(5);

            animator.SetTrigger("Attack");

            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }

        //ANGY PUNCH
        if (Vector2.Distance(transform.position, player.position) < attackDistance && timeBTWAttacks <= 0 && GameManager.instance.GetAlive() && stunned == false && angy == true)
        {
            GameManager.instance.TakeDamage(10);

            animator.SetTrigger("Attack");

            timeBTWAttacks = startTimeBTWAttacks;

            angy = false;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }

        //STUN
        if (stunned == true)
        {
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
        if(collision.CompareTag("AttackHit"))
        {
            if(!playerAnimator.GetBool("isHoldingSword"))
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

        animator.SetTrigger("Hit");

        stunned = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        if (hasDeathEvents)
        {
            //gameObject.GetComponent<OnDeath>().DoOnDeath();
            TriggerEventManager.instance.SpawnMobs();
        }
        this.enabled = false;
    }
}
 