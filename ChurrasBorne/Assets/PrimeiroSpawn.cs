using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeiroSpawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EnemyControl.Instance.SpawnFirstMob();
            gameObject.SetActive(false);
        }
    }
}
