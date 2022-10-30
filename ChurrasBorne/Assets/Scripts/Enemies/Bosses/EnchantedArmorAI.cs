using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantedArmorAI : MonoBehaviour
{
    public Transform player;

    public float agroDistance, stopDistance, speed, attackDistance, startTimeBTWAttacks;
    private float timeBTWAttacks;

    public Collider2D bodyCollider;
    public Rigidbody2D rb;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    public Animator playerAnimator;
    public bool isDead = false;
    public bool isOnFaseUm;

    public GameObject zoneAttack;

    public GameObject[] towah;
    private int currentIndex = 0;


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

            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;

            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
        else if (Vector2.Distance(transform.position, player.position) > agroDistance)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
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
            GameManager.instance.TakeDamage(10);

            animator.SetTrigger("Attack");

            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }

        //ZONE ATTACK
        if(currentHealth <= 20)
        {
            zoneAttack.SetActive(true);
            rb.velocity = Vector2.zero;
        }
    }

    //TOWAH
    public void NewRandomObject()
    {
        if (currentHealth <= 50)
        {
            int newIndex = Random.Range(0, towah.Length);
            towah[currentIndex].SetActive(false);
            currentIndex = newIndex;
            towah[currentIndex].SetActive(true);
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

        animator.SetTrigger("Hit");

        //stunned = true;

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
        animator.SetBool("Pheesh", true);
        GetComponent<Collider2D>().enabled = false;

        if (isOnFaseUm)
        {
            EnemyControl.Instance.KilledEnemy(gameObject);
        }
        this.enabled = false;
    }
}
