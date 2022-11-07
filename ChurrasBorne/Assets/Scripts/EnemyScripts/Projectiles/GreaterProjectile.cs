using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterProjectile : MonoBehaviour
{
    public float speed;

    public Transform player;
    private Vector2 target;

    public Animator animator;


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

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }


    //DAMAGE
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(5);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obst"))
        {
            Destroy(gameObject);
        }
    }
}
