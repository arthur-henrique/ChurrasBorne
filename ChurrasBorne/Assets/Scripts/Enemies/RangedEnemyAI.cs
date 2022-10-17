using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public Transform player;

    public GameObject projectile;

    public float agroDistance, startTimeBTWAttacks;
    private float timeBTWAttacks;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    void Start()
    {
        //Para RANGED
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWAttacks = startTimeBTWAttacks;

        //Para HEALTH
        currentHealth = maxHealth;
    }

    void Update()
    {
        //RANGED
        if (Vector2.Distance(transform.position, player.position) < agroDistance && timeBTWAttacks <= 0)
        {
            Instantiate(projectile);
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }
    }

    
    //MELEE ON CONTACT
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(5);
        }
    }

    //HEALTH
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
