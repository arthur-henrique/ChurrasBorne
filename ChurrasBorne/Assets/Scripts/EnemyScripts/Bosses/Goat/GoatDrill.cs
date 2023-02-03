using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatDrill : MonoBehaviour
{
    public Transform player;

    public GameObject goat;

    public float damageDistance;

    public bool canDamage = false;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDamage == true)
        {
            if (Vector2.Distance(transform.position, player.position) <= damageDistance)
            {
                GameManager.instance.TakeDamage(5, 0.25f);
            }
        }

        if (goat.GetComponent<GoatAI>().isAlive == false)
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
