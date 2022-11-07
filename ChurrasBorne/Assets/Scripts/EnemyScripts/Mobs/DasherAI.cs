using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherAI : MonoBehaviour
{
    public Transform player;
    public Vector3 target;

    public float agroDistance, stopDistance, speed, dashSpeed, dashPunchDistance, attackDistance, startTimeBTWAttacks, startStunTime, startRecoveryTime;
    private float timeBTWAttacks, stunTime, recoveryTime;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    public bool isOnTutorial, isOnFaseUm, isOnFaseUmHalf, dash = false, canDash;

    private bool stunned = false, recovering = false;   

    public GameObject dashDetector;

    void Start()
    {
        //Para MELEE, DASH, MOVEMENT, FLIP, STUN
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWAttacks = startTimeBTWAttacks;

        stunTime = startStunTime;

        recoveryTime = startRecoveryTime;
        
        
        //Para HEALTH
        currentHealth = maxHealth;

        
        //Para ON CONTACT
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //MOVEMENT
        if (Vector2.Distance(transform.position, player.position) <= agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance && stunned == false && canDash == false && recovering == false)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", false);

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance && canDash == false || Vector2.Distance(transform.position, player.position) > agroDistance && canDash == false && recovering == false)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", true);

            rb.velocity = Vector2.zero;
        }


        //DASH
        if(canDash == false)
        {
            target = player.transform.position;

            Vector3 fator = player.position - transform.position;

            target.x = player.position.x + fator.x * 2;

            target.y = player.position.y + fator.y * 2; ;
        }
        
        if(canDash == true)
        {
            Dash();
        }

        void Dash()
        {
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", false);

            transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * Time.deltaTime);

            if (target.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (target.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (Vector2.Distance(transform.position, player.position) <= dashPunchDistance && canDash == true)
        {
            animator.SetTrigger("Punch");

            GameManager.instance.TakeDamage(10);

            recovering = true;

            canDash = false;
        }
        else if (transform.position.x == target.x && transform.position.y == target.y && canDash == true)
        {
            recovering = true;
            
            canDash = false;
        }

        if(recovering == true)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Running", false);
            animator.SetBool("Walking", false);

            rb.velocity = Vector2.zero;

            if (recoveryTime <= 0)
            {
                recovering = false;

                recoveryTime = startRecoveryTime;
            }
            else
            {
                recoveryTime -= Time.deltaTime; 
            }
        }


        //FLIP
        if (player.position.x < transform.position.x && canDash == false && recovering == false)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x && canDash == false && recovering == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        
        //MELEE
        if (Vector2.Distance(transform.position, player.position) <= attackDistance && timeBTWAttacks <= 0 && GameManager.instance.GetAlive() && stunned == false && canDash == false && recovering == false)
        {
            animator.SetTrigger("Attack");

            GameManager.instance.TakeDamage(5);

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

            canDash = false;    

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
            if(isOnTutorial)
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
        animator.SetTrigger("Hit");

        currentHealth -= damage;

        stunned = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Dead", true);
        
        GetComponent<Collider2D>().enabled = false;
        
        if (isOnTutorial)
        {
            EnemyControlTutorial.Instance.KilledEnemy(gameObject);
        }
        if (isOnFaseUm)
        {
            EnemyControl.Instance.KilledEnemy(gameObject);
        }
        
        this.enabled = false;
    }
}
 