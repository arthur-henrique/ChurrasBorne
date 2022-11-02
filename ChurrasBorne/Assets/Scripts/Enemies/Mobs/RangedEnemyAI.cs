using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public Transform player;
    private Vector2 target;

    public GameObject projectile;

    public float agroDistance, chaseDistance, stopDistance, meleeDistance, startStunTime, startMeleeTime, startSpitTime, speed;
    private float stunTime, meleeTime, spitTime;

    public int maxHealth;
    int currentHealth;

    public Collider2D col;
    public Rigidbody2D rb;

    public Animator animator;

    public bool isOnTutorial, isOnFaseUm, isOnFaseUmHalf;

    private bool stunned = false;

    private bool isSpitting = false;

    void Start()
    {
        //Para MELEE, RANGED, STUN
        player = GameObject.FindGameObjectWithTag("Player").transform;

        stunTime = startStunTime;

        meleeTime = startMeleeTime;

        target = player.position;

        new Vector2(player.position.x, player.position.y);

        Vector3 fator = player.position - transform.position;

        target.x = player.position.x + fator.x * 3;

        target.y = player.position.y + fator.y * 3;

        //Para HEALTH
        currentHealth = maxHealth;
    }

    void Update()
    {
        //MOVEMENT
        if(Vector2.Distance(transform.position, player.position) <= chaseDistance && Vector2.Distance(transform.position, player.position) > stopDistance && stunned ==false)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            animator.SetBool("Walking", true);
            animator.SetBool("Idling", false);
        }
        else if (Vector2.Distance(transform.position, player.position) > chaseDistance)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idling", true);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            rb.velocity = Vector2.zero;

            animator.SetBool("Walking", false);
            animator.SetBool("Idling", true);
        }
        else if (Vector2.Distance(transform.position, player.position) > agroDistance)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idling", true);
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
        if (Vector2.Distance(transform.position, player.position) < meleeDistance && meleeTime <= 0 && GameManager.instance.GetAlive() && stunned == false)
        {
            GameManager.instance.TakeDamage(5);

            animator.SetTrigger("Melee");

            meleeTime = startMeleeTime;
        }
        else
        {
            meleeTime -= Time.deltaTime;
        }

        //RANGED
        if (Vector2.Distance(transform.position, player.position) < agroDistance && Vector2.Distance(transform.position, player.position) > chaseDistance && spitTime <= 0 && stunned == false && GameManager.instance.GetAlive())
        {
            Instantiate(projectile, transform.position, Quaternion.identity);

            animator.SetTrigger("Spitting");

            spitTime = startSpitTime;
        }
        else
        {
            spitTime -= Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col.isTrigger = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col.isTrigger = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //HEALTH
        if (collision.CompareTag("AttackHit"))
        {
            if (isOnTutorial)
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
        animator.SetBool("Idling", false);
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
