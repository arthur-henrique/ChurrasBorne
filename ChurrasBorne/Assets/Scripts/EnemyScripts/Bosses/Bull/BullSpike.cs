using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSpike : MonoBehaviour
{
    public Transform player;

    public GameObject bull;
    
    public float damageDistance;

    public bool canDamage = false, isOnTut;

    public Animator anim;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (canDamage == true && isOnTut)
        {
            if (Vector2.Distance(transform.position, player.position) <= damageDistance)
            {
                GameManager.instance.TakeDamage(15);
            }
        }
        if (canDamage == true && !isOnTut)
        {
            if (Vector2.Distance(transform.position, player.position) <= damageDistance)
            {
                GameManager.instance.TakeDamage(10);
            }
        }

        if (bull.GetComponent<BullAI>().isAlive == false)
        {
            Destroy(gameObject);
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void DamagePlayerOn()
    {
        canDamage = true;
    }

    public void DamagePlayerOff()
    {
        canDamage = false;
    }
}
