using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseUmTriggerController : MonoBehaviour
{
    public static FaseUmTriggerController Instance;
    public GameObject[] firstGateIn;
    public GameObject[] secondGateIn;
    public GameObject[] firstGateOut;

    private void Awake()
    {
        Instance = this;
    }
    public void FirstGateTrigger()
    {
        for (int i = 0; i < firstGateIn.Length; i++)
        {
            firstGateIn[i].SetActive(!firstGateIn[i].activeSelf);
        }
    }

    public void FirstGateOut()
    {
        for (int i = 0; i < firstGateIn.Length; i++)
        {
            firstGateIn[i].SetActive(!firstGateIn[i].activeSelf);
        }
        for (int i = 0; i < firstGateOut.Length; i++)
        {
            firstGateOut[i].SetActive(!firstGateOut[i].activeSelf);
        }
    }

    public void SecondGateTrigger()
    {
        for (int i = 0; i < secondGateIn.Length; i++)
        {
            secondGateIn[i].SetActive(!secondGateIn[i].activeSelf);
        }
    }
}
