using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredTebasProjectile : MonoBehaviour
{
    public float speed;

    public MobAI armoredTebas;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitStart());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitUpdate());
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

    public void ArmorTebasSetter(MobAI armortebas)
    {
        armoredTebas = armortebas;
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(0.1f);
        if (armoredTebas.isFlipped == true)
        {
            print("Left");
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (armoredTebas.isFlipped == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
            print("Roght");
        }
    }

    IEnumerator WaitUpdate()
    {
        yield return new WaitForSeconds(0.11f);
        if (armoredTebas.isFlipped == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
