using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Projectile : MonoBehaviour
{
    public float speed;

    public Transform APTP, bossTarget;
    private Vector2 target, newTarget;

    public GameObject mommyWeb, grannyWeb;

    public GameObject spitter;

    private GameObject faseDois;

    public int health;

    public bool isOnTutorial, isFromBoss, isAWeb, hasBeenParried, isOnFaseDois, isAFireBall, isFromMommy, isFromGranny;
    private bool canBeParried = true;
    private SpriteRenderer sr;
    public UnityEngine.Experimental.Rendering.Universal.Light2D ltd;
    public GameObject normalTrail, unparryTrail;

    public LayerMask mask;

    void Start()
    {
        //Para PROJECTILE MOVEMENT
        APTP = GameObject.FindGameObjectWithTag("NYA").transform;
        if (isFromBoss)
        {
            bossTarget = GameObject.FindGameObjectWithTag("BossTarget").transform;
            newTarget = bossTarget.position;
        }
        sr = gameObject.GetComponent<SpriteRenderer>();

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

        /*
        if (isFromBoss)
        {
            normalTrail = null; unparryTrail = null;
        }
        */
        // Manages if the project may or not be parried
        int diceroll = Random.Range(0, 4);
        //print(diceroll);
        if (diceroll > 2 && normalTrail != null && unparryTrail != null)
        {
            canBeParried = false;
            normalTrail.SetActive(false);
            unparryTrail.SetActive(true);
            sr.color = new Color(0.7423134f, 0f, 1f, 1f);
            if (ltd != null)
                ltd.color = new Color(0.7423134f, 0f, 1f, 1f);
        }
        if (diceroll > 1 && normalTrail != null && unparryTrail != null)
        {
            canBeParried = false;
            normalTrail.SetActive(false);
            unparryTrail.SetActive(true);
            sr.color = new Color(0.7423134f, 0f, 1f, 1f);
            if (ltd != null)
                ltd.color = new Color(0.7423134f, 0f, 1f, 1f);
        }
    }

    //PROJECTILE MOVEMENT
    void Update()
    {
        if (health > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (health <= 0 && !isFromBoss)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, -speed * Time.deltaTime);

            hasBeenParried = true;
        }     
        if (health <= 0 && isFromBoss)
        {
            transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime);

            hasBeenParried = true;
        }

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            if (!isAWeb)
            {
                Destroy(gameObject);
            }
            if(isAWeb && isFromBoss && isFromMommy)
            {
                Instantiate(mommyWeb, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
            if (isAWeb && isFromBoss && isFromGranny)
            {
                Instantiate(grannyWeb, transform.position, Quaternion.identity);

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
                GameManager.instance.TakeDamage(9);
                Destroy(gameObject);
            }
            if (!isAWeb && isFromBoss)
            {
                GameManager.instance.TakeDamage(15);
                canBeParried = false;
                Destroy(gameObject);
            }
            else
            {
                canBeParried = false;
                Instantiate(mommyWeb, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if(isAFireBall)
            {
                GameManager.instance.Poison(1f);
            }
        }

        if (!isAWeb && !isFromBoss)
        {
            if (collision.gameObject.GetComponent<MobAI>() != null && hasBeenParried)
            {
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("TRONCO"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("PAREDE") && !isOnFaseDois)
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("BOSS") && isFromBoss)
        {
            Destroy(gameObject, .5f);
        }

        //HEALTH
        if (collision.CompareTag("AttackHit"))
        {
            TakeDamage();
        }

        //Vector2 difference = transform.position - collision.transform.position;
        //transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
    }


    public void TakeDamage(bool fish = true)
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
        if(canBeParried)
        {
            health -= damage;
        }
    }
}
