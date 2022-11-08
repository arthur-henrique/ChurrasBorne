using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterAI : MonoBehaviour
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

    public bool isOnTutorial, isOnFaseUm, canDamage = false;

    private bool stunned = false;

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
        else if (Vector2.Distance(transform.position, player.position) > chaseDistance || Vector2.Distance(transform.position, player.position) <= stopDistance || Vector2.Distance(transform.position, player.position) > agroDistance)
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
        if (Vector2.Distance(transform.position, player.position) < meleeDistance && meleeTime <= 0 && GameManager.instance.GetAlive() && stunned == false)
        {
            canDamage = true;

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
            animator.SetTrigger("Spitting");

            Instantiate(projectile, transform.position, Quaternion.identity);

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

    //MELEE
    public void damagePlayer()
    {
        if (canDamage == true && Vector2.Distance(transform.position, player.position) <= meleeDistance)
        {
            GameManager.instance.TakeDamage(5);

            canDamage = false;
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
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        if (isOnTutorial)
        {
            EnemyControlTutorial.Instance.KilledEnemy(gameObject);
            if(gameObject.CompareTag("BOSS"))
            {
                TutorialTriggerController.Instance.SecondGateTriggerOut();
                GameManager.instance.SwitchToDefaultCam();
            }
        }
        if (isOnFaseUm)
        {
            EnemyControl.Instance.KilledEnemy(gameObject);
        }
        this.enabled = false;
    }
}
