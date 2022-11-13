using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSpike : MonoBehaviour
{
    public Transform player;
    
    public float damageDistance;

    public bool canDamage = false;

    public Animator anim;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (canDamage == true)
        {
            if (Vector2.Distance(transform.position, player.position) <= damageDistance)
            {
                GameManager.instance.TakeDamage(5);
            }
        }
    }
    public void destruirBagassa(float tempo)
    {
        Destroy(gameObject, tempo);
    }

    public void damagePlayerOn()
    {
        canDamage = true;
    }

    public void damagePlayerOff()
    {
        canDamage = false;
    }
}
