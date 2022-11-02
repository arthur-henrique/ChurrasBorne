using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDois : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FaseUmTriggerController.Instance.SideFirstGateTrigger();
            EnemyControl.Instance.SpawnThirdMob();
            gameObject.SetActive(false);
        }
    }
}
