using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBladeWave : MonoBehaviour
{
    public Transform player;
    private Vector2 target;

    public float speed;
    
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

        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
