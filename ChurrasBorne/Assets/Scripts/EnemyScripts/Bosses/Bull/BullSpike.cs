using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class BullSpike : MonoBehaviour
{
    public Transform player;

    public GameObject bull;

    public bool canDamage = false, isOnTut;

    private float damageDistance = 1.2f;

    public Animator anim;
    public ParticleSystem boomSpikeUp, boomSpikeDown;
    public bool isCenterSpike;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isOnTut = FindObjectOfType<BullAI>().isOnTut;
    }

    void Update()
    {
        if (bull.GetComponent<BullAI>().isAlive == false)
        {
            Destroy(gameObject);
        }

        if(Vector2.Distance(transform.position, player.position) <= damageDistance)
        {
            if(canDamage && isOnTut)
            {
                GameManager.instance.TakeDamage(3, 0.25f);
                print("IsOnTut");
            }
            if(canDamage && !isOnTut)
            {
                GameManager.instance.TakeDamage(7, 0.25f);
            }
        }
    }

    public void DamagePlayerOn()
    {
        canDamage = true;
    }
    public void DestroySelf()
    {
        Destroy(gameObject, 1.5f);
    }

    private void PlayParticles()
    {
        if (!isCenterSpike)
        {
            return;
        }
        if (isCenterSpike)
        {
            boomSpikeUp.gameObject.SetActive(true);
            boomSpikeDown.gameObject.SetActive(true);
            boomSpikeUp.Stop();
            boomSpikeDown.Stop();
            boomSpikeUp.Play();
            boomSpikeDown.Play();
        }
    }
}
