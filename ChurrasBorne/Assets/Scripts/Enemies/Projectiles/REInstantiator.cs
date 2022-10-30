using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REInstantiator : MonoBehaviour
{
    public bool canShoot = false;

    public GameObject projectile;

    public float startTimeBTWAttacks;
    private float timeBTWAttacks;
    
    void Start()
    {
        timeBTWAttacks = startTimeBTWAttacks;
    }

    void Update()
    {
        if (canShoot == true && timeBTWAttacks <= 0)
        {
            Instantiate(projectile);
            timeBTWAttacks = startTimeBTWAttacks;
        }
        else
        {
            timeBTWAttacks -= Time.deltaTime;
        }
    }
}
