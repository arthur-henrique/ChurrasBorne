using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    public Transform player;

    public float speed, startATKTime, startStunTime, agroDistance, stopDistance, attackDistance;
    private float attackTime, stunTime;

    public Collider2D col;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    public Animator anim;

    public bool isOnFaseUm, isOnFaseUmHalf;

    private bool stunned = false, canDamage = false;

    void Start()
    {
        //Para MOVEMENT, MELEE, STUN
        player = GameObject.FindGameObjectWithTag("Player").transform;

        attackTime = startATKTime;
        stunTime = startStunTime;

        //Para HEALTH
        currentHealth = maxHealth;

        //Para ON CONTACT
    }

    void Update()
    {
        //MOVEMENT
        if (Vector2.Distance(transform.position, player.position) <= agroDistance && Vector2.Distance(transform.position, player.position) > stopDistance && stunned == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance && stunned == false || Vector2.Distance(transform.position, player.position) > agroDistance && stunned == false)
        {
            rb.velocity = Vector2.zero;

            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
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
        if (Vector2.Distance(transform.position, player.position) < attackDistance && attackTime <= 0 && GameManager.instance.GetAlive() && stunned == false)
        {
            canDamage = true;

            anim.SetTrigger("Attack");

            attackTime = startATKTime;
        }
        else
        {
            attackTime -= Time.deltaTime;
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

    //MELEE
    public void damagePlayer()
    {
        if (canDamage == true && Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            GameManager.instance.TakeDamage(5);

            canDamage = false;
        }
    }

    //ON CONTACT    
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

    //HEALTH
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackHit"))
        {
            TakeDamage(20);
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hit");

        stunned = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Die", true);

        GetComponent<Collider2D>().enabled = false;

        if (isOnFaseUm)
        {
            EnemyControl.Instance.KilledEnemy(gameObject);
        }
        
        this.enabled = false;
    }
}
