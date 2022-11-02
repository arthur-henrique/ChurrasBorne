using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUm : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FaseUmTriggerController.Instance.FirstGateTrigger();
            EnemyControl.Instance.SpawnFourthMob();
            gameObject.SetActive(false);
        }
    }
}
