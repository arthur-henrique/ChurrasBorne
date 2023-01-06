using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUm : MonoBehaviour
{
    public Animator gateAnim;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            print("FechaPort");
            gateAnim.SetTrigger("CLOSEIT");
            EnemyControl.Instance.SpawnFourthMob();
            gameObject.SetActive(false);
        }
    }
}
