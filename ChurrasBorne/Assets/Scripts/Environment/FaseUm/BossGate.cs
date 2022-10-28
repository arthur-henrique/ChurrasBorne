using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FaseUmTriggerController.Instance.SecondGateTrigger();
            boss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
