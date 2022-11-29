using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public Transform player;
    private Vector2 target;

    public int health;

    public Animator anim;

    public bool isOnTutorial;

    private float yOffset = 1.6f;

    void Start()
    {
        //Para PROJECTILE MOVEMENT
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);

        //target = player.position;

        //new Vector2(player.position.x, player.position.y);

        Vector3 fator = player.position - transform.position;

        target.x = player.position.x + fator.x * 3;

        target.y = player.position.y + fator.y * 3;
    }

    //PROJECTILE MOVEMENT
    void Update()
    {
        if (health > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else if (health <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, -speed * Time.deltaTime);
        }

        anim.SetBool("Flying", true);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DAMAGE
        if (collision.CompareTag("Player") && health > 0)
        {
            GameManager.instance.TakeDamage(10);
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
            TakeDamage();
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }


    public void TakeDamage()
    {
        int damage;

        if (isOnTutorial)
        {
            damage = 5;
        }
        else
        {
            damage = 10;
        }

        health -= damage;
    }
}
