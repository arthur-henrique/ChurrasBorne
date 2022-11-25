using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterSpike : MonoBehaviour
{
    public GameObject trickster;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (trickster.gameObject.GetComponent<TricksterAI>().isAlive == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.TakeDamage(5);
        }
    }

    void DamagePlayerOn()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    void DamagePlayerOff()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
