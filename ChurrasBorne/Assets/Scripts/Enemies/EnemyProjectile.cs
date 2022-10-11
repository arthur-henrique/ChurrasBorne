using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Transform player;

    public float speed;

    public float targetDistance;

    private float destroyTime;
    public float startDestroyTime;

    private Vector2 target;

    public GameObject minorEnemyProjectile;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = player.position;

        new Vector2(player.position.x, player.position.y);

        destroyTime = startDestroyTime;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < targetDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (transform.position.x == target.x && transform.position.y == target.y && destroyTime <= 0)
            {              
                Instantiate(minorEnemyProjectile, transform.position, Quaternion.identity);
                Destroy(gameObject);
                destroyTime = startDestroyTime;
            }
            else
            {
                destroyTime -= Time.deltaTime;
            }
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
