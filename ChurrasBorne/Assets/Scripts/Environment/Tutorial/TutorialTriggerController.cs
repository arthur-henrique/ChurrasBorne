using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerController : MonoBehaviour
{
    public static TutorialTriggerController Instance;
    public GameObject[] firstGateOut;
    public GameObject[] secondGateIn;

    public Animator portaoAnim;

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
        portaoAnim.SetTrigger("OPENIT");
        SecondGateTrigger();
    }
}
