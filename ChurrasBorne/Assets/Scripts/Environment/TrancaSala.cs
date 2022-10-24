using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrancaSala : MonoBehaviour
{
    public GameObject[] trancaDestranca;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < trancaDestranca.Length; i++)
            {
                trancaDestranca[i].SetActive(!trancaDestranca[i].activeSelf);
            }
        }
        gameObject.SetActive(false);
    }
}
