using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public Transform player;

    public GameObject instantiator;

    public float agroDistance, startStunTime;
    private float stunTime;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    public Animator playerAnimator;
    public bool isOnFaseUm;

    private bool stunned = false;

    void Start()
    {
        //Para RANGED, STUN
        player = GameObject.FindGameObjectWithTag("Player").transform;

        stunTime = startStunTime;

        //Para HEALTH
        currentHealth = maxHealth;
    }

    void Update()
    {
        //RANGED
        if (Vector2.Distance(transform.position, player.position) < agroDistance && stunned == false)
        {
            instantiator.GetComponent<REInstantiator>().canShoot = true;
        }
        else
        {
            instantiator.GetComponent<REInstantiator>().canShoot = false;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //MELEE ON CONTACT
        if (collision.gameObject.CompareTag("Player") && stunned == false)
        {
            GameManager.instance.TakeDamage(5);
        }

        //HEALTH
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

        if (isOnFaseUm)
        {
            EnemyControl.Instance.KilledEnemy(gameObject);
        }
        this.enabled = false;
    }
}
