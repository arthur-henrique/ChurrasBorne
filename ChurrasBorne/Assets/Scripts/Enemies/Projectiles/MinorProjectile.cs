using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorProjectile : MonoBehaviour
{
    public float speed;

    public Transform player;
    private Vector2 target;

    public Animator animator;

    public int maxHealth;
    int currentHealth;

    public Animator playerAnimator;

    private bool wasDeflected = false;


    void Start()
    {
        //Para PROJECTILE MOVEMENT
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = player.position;

        new Vector2(player.position.x, player.position.y);

        Vector3 fator = player.position - transform.position;

        target.x = player.position.x + fator.x * 3;

        target.y = player.position.y + fator.y * 3;
    }


    //PROJECTILE MOVEMENT
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y && wasDeflected == false)
        {
            Destroy(gameObject);
        }

        if (currentHealth <= 0)
        {
            wasDeflected = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DAMAGE
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(5);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obst"))
        {
            Destroy(gameObject);
        }

        //HEALTH
        if (collision.CompareTag("AttackHit"))
        {
            if (!playerAnimator.GetBool("isHoldingSword"))
            {
                TakeDamage(10);
            }
            else
            {
                TakeDamage(1);
            }
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, -speed * Time.deltaTime);
        }
    }
}
