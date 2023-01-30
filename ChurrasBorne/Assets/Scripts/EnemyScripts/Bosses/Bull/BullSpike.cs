using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BullSpike : MonoBehaviour
{
    public Transform player;

    public GameObject bull;
    
    public float damageDistance;

    public bool canDamage = false, isOnTut;

    public Animator anim;
    public ParticleSystem boomSpikeUp, boomSpikeDown;
    public bool isCenterSpike;
    
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
                GameManager.instance.TakeDamage(5, 0.25f);
            }
        }
        if (canDamage == true && !isOnTut)
        {
            if (Vector2.Distance(transform.position, player.position) <= damageDistance)
            {
                GameManager.instance.TakeDamage(10, 0.25f);
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
