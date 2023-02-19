using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDrill : MonoBehaviour
{
    public Transform player;

    public GameObject finalBoss;

    public float damageDistance;

    public bool canDamage = false;

    public Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (canDamage)
        {
            if (Vector2.Distance(transform.position, player.position) <= damageDistance)
            {
                GameManager.instance.TakeDamage(15);
            }
        }

        if (finalBoss.GetComponent<FinalBossAI>().isBreathing == false)
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
}
