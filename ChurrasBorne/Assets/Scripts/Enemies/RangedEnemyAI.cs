using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public Transform player;

    public GameObject enemyProjectile;

    public float agroDistance;

    public int maxHealth;
    int currentHealth;

    private float timeBTWShots;
    public float startTimeBTWShots;

    public Animator animator;

    public Collider2D bodyCollider;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWShots = startTimeBTWShots;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < agroDistance && timeBTWShots <= 0)
        {
            Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            timeBTWShots = startTimeBTWShots;
        }
        else
        {
            timeBTWShots -= Time.deltaTime;
        }
    }
}
