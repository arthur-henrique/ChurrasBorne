using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private Vector2 target;

    public float agroDistance, stopDistance, speed, dashSpeed, dashPunchDistance, attackDistance, startTimeBTWAttacks, startStunTime;
    private float timeBTWAttacks, stunTime;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    public Animator playerAnimator;
    public bool isOnTutorial, isOnFaseUm, isOnFaseUmHalf;

    private bool stunned = false;
    
    public bool dash = false;    

    public GameObject angyDetector;

    void Start()
    {
        //Para MELEE, DASH, MOVEMENT, STUN
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWAttacks = startTimeBTWAttacks;

        stunTime = startStunTime;

        target = player.position;

        new Vector2(player.position.x, player.position.y);

        Vector3 fator = player.position - transform.position;

        target.x = player.position.x + fator.x * 2;

        target.y = player.position.y + fator.y * 2;

        //Para HEALTH
        currentHealth = maxHealth;

        //Para ON CONTACT
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //MOVEMENT
        if (Vector2.Distance(transform.position, player.position) <= agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance && stunned == false && dash == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            Debug.Log("NotDashingAnimore");

            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", false);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance && dash == false)
        {
            rb.velocity = Vector2.zero;

            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
        else if (Vector2.Distance(transform.position, player.position) > agroDistance && dash == false)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }

        //DASH
        if (dash == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * Time.deltaTime);

            Debug.Log("Stuck");

            animator.SetBool("Running", true);

            if (Vector2.Distance(transform.position, player.position) < dashPunchDistance)
            {
                GameManager.instance.TakeDamage(7);

                animator.SetTrigger("Punch");

                dash = false;
            }
            else if(transform.position.x == target.x && transform.position.y == target.y)
            {
                dash = false;
            }
        }

        if(dash == false)
        {
            //Debug.Log("NotDashing");
        }
        else
        {
            //Debug.Log("Dashing");
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
        if (Vector2.Distance(transform.position, player.position) < attackDistance && timeBTWAttacks <= 0 && GameManager.instance.GetAlive() && stunned == false && dash == false)
        {
            GameManager.instance.TakeDamage(3);

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
        if(collision.CompareTag("AttackHit"))
        {
            if(isOnTutorial)
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
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Pheesh", true);
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
 