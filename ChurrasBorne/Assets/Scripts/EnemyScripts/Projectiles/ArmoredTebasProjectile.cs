using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredTebasProjectile : MonoBehaviour
{
    public float speed;

    public GameObject armoredTebas;
    
    // Start is called before the first frame update
    void Start()
    {
        if(armoredTebas.GetComponent<MobAI>().isFlipped == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("LEFT");
        }
        if(armoredTebas.GetComponent<MobAI>().isFlipped == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("RIGHT");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(armoredTebas.GetComponent<MobAI>().isFlipped == true)
        {
            
        }
        else
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(25);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
