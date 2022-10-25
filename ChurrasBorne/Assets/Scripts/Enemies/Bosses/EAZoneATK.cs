using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAZoneATK : MonoBehaviour
{
    public float startZoneATKTime;
    private float zoneATKTime;

    public Collider2D bodyCollider;

    public GameObject zoneAttack;


    void Start()
    {
        zoneATKTime = startZoneATKTime;

        this.enabled = false;
    }

    void Update()
    {
        zoneAttack.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (zoneATKTime >= 2)
        {
            GameManager.instance.TakeDamage(10);

            zoneATKTime = startZoneATKTime;
        }
        else
        {
            zoneATKTime -= Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        zoneAttack.SetActive(false);
    }
}