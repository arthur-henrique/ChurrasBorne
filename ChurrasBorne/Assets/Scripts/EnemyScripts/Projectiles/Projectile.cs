using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public Transform player;
    private Vector2 target;

    public int maxHealth;
    int currentHealth;

    public Animator animator;

    public bool isOnTutorial;


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

        animator.SetBool("Flying", true);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
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
        else if (collision.CompareTag("TRONCO"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("PAREDE"))
        {
            Destroy(gameObject);
        }

        //HEALTH
        if (collision.CompareTag("AttackHit"))
        {
            if (isOnTutorial)
            {
                TakeDamage(1);
            }
            else
            {
                TakeDamage(10);
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
            Destroy(gameObject);
        }
    }
}
