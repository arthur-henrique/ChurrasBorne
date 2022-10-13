using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorEnemyProjectile : MonoBehaviour
{
    public Transform player;

    public float speed;

    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = player.position;

        new Vector2(player.position.x, player.position.y);

        Vector3 fator = player.position - transform.position;

        target.x = player.position.x + fator.x * 3;

        target.y = player.position.y + fator.y * 3;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(20);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obst"))
        {
            Destroy(gameObject);
        }
    }
}
