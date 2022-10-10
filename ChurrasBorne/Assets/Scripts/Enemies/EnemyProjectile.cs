using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;

    public Transform player;

    public float speed;

    public float fireDistance;

    private Vector2 alvo1;
    private Vector2 alvo2;
    private Vector2 alvo3;
    private Vector2 alvo4;

    public GameObject minorEnemyProjectile;

    void Start()
    {
        target1 = GameObject.FindGameObjectWithTag("Target1").transform;
        target2 = GameObject.FindGameObjectWithTag("Target2").transform;
        target3 = GameObject.FindGameObjectWithTag("Target3").transform;
        target4 = GameObject.FindGameObjectWithTag("Target4").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        alvo1 = target1.position;
        alvo2 = target2.position;
        alvo3 = target3.position;
        alvo4 = target4.position;

        new Vector2(target1.position.x, target1.position.y);
        new Vector2(target2.position.x, target2.position.y);
        new Vector2(target3.position.x, target3.position.y);
        new Vector2(target4.position.x, target4.position.y);
    }

    void Update()
    {
        if (Vector2.Distance(target1.position, player.position) < fireDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, alvo1, speed * Time.deltaTime);

            if (transform.position.x == alvo1.x && transform.position.y == alvo1.y)
            {
                Instantiate(minorEnemyProjectile, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (Vector2.Distance(target2.position, player.position) < fireDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, alvo2, speed * Time.deltaTime);

            if (transform.position.x == alvo2.x && transform.position.y == alvo2.y)
            {
                Instantiate(minorEnemyProjectile, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (Vector2.Distance(target3.position, player.position) < fireDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, alvo3, speed * Time.deltaTime);

            if (transform.position.x == alvo3.x && transform.position.y == alvo3.y)
            {
                Instantiate(minorEnemyProjectile, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (Vector2.Distance(target4.position, player.position) < fireDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, alvo4, speed * Time.deltaTime);

            if (transform.position.x == alvo4.x && transform.position.y == alvo4.y)
            {
                Instantiate(minorEnemyProjectile, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //GameManager.instance.TakeDamage(1);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obst"))
        {
            Destroy(gameObject);
        }
    }
}
