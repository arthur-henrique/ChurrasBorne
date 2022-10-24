using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EATowah : MonoBehaviour
{
    public Transform player;

    public GameObject greaterProjectile;

    public float startTimeBTWAttacks;
    private float timeBTWAttacks;

    public GameObject towah;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTWAttacks = startTimeBTWAttacks;

        towah.SetActive(false);
    }

    void Update()
    {
        if (timeBTWAttacks <= 0)
        {
            Instantiate(greaterProjectile);
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }
    }
}
