using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public GameObject[] firstStairsCollisions;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            for (int i = 0; i < firstStairsCollisions.Length; i++)
            {
                firstStairsCollisions[i].SetActive(!firstStairsCollisions[i].activeSelf);
            }
        }
    }
}
