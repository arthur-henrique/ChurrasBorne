using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public GameObject[] doorToOpen;
    public bool opensDoor, opensPlatforms;

    public void DoOnDeath()
    {
        if (opensDoor)
        {
            for (int i = 0; i < doorToOpen.Length; i++)
            {
                doorToOpen[i].SetActive(!doorToOpen[i].activeSelf);
            }
        }
    }
}
