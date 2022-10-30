using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerController : MonoBehaviour
{
    public static TutorialTriggerController Instance;
    public GameObject[] firstGateOut;
    public GameObject[] secondGateIn;
    public GameObject[] secondGateOut;

    private void Awake()
    {
        Instance = this;
    }

    public void FirstGateTrigger()
    {
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

    public void SecondGateTriggerOut()
    {
        for (int i = 0; i < secondGateOut.Length; i++)
        {
            secondGateOut[i].SetActive(!secondGateOut[i].activeSelf);
        }
    }
}
