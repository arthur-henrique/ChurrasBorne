using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBProjectile : MonoBehaviour
{
    public Transform player;
    private Vector2 target;

    public float speed;

    public GameObject finalBossFase1, finalBossFase2;

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("NYA").transform;

        target = player.position;
        new Vector2 (target.x, target.y);

        Vector3 fator = player.position - transform.position;

        target.x = player.position.x + fator.x * 2;

        target.y = player.position.y + fator.y * 2;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Fly", true);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(40);
            anim.SetTrigger("Destroy");
        }
        if(collision.CompareTag("PAREDE"))
        {
            anim.SetTrigger("Destroy");
        }

        if(finalBossFase1.GetComponent<FinalBossAI>().isBreathing == false || finalBossFase2.GetComponent<FinalBossAI>().isBreathing == false)
        {
            anim.SetTrigger("Destroy");
        }

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            anim.SetTrigger("Destroy");
        }
    }

    void DisableTrigger()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
