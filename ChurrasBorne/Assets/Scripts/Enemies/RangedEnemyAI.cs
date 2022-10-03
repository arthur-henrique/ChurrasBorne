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

    public Animator animator;

    public Collider2D bodyCollider;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < agroDistance)
        {
            Instantiate(enemyProjectile);
        }
    }
}
