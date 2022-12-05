using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public Transform APTP;
    private Vector2 target;

    public GameObject mommyWeb;

    public GameObject spitter;

    private GameObject faseDois;

    public int health;

    public bool isOnTutorial, isFromBoss, isAWeb, hasBeenParried, isOnFaseDois;

    void Start()
    {
        //Para PROJECTILE MOVEMENT
        APTP = GameObject.FindGameObjectWithTag("NYA").transform;

        target = APTP.position;

        new Vector2(APTP.position.x, APTP.position.y);

        if (!isAWeb)
        {
            Vector3 fator = APTP.position - transform.position;

            target.x = APTP.position.x + fator.x * 3;

            target.y = APTP.position.y + fator.y * 3;
        }

        isOnFaseDois = false;

        hasBeenParried = false;

        faseDois = GameObject.FindGameObjectWithTag("FASEDOIS");

        if (faseDois != null)
        {
            isOnFaseDois = true;
        }
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

            hasBeenParried = true;
        }

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            if (!isAWeb)
            {
                Destroy(gameObject);
            }
            else
            {
                Instantiate(mommyWeb, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }

        Destroy(gameObject, 7f);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DAMAGE
        if (collision.CompareTag("Player") && health > 0)
        {
            if (!isAWeb && !isFromBoss)
            {
                GameManager.instance.TakeDamage(7);
                Destroy(gameObject);
            }
            else if (!isAWeb && isFromBoss)
            {
                GameManager.instance.TakeDamage(15);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(mommyWeb, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("TRONCO"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("PAREDE") && !isOnFaseDois)
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
