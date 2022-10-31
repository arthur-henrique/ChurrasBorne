using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseUmTriggerController : MonoBehaviour
{
    public static FaseUmTriggerController Instance;
    public GameObject[] upPathFirstGateIn;
    public GameObject[] upPathSecondGateIn;
    public GameObject[] upPathFirstGateOut;
    public GameObject[] sidePathFirstGateIn;
    public GameObject[] sidePathSecondGateIn;

    private void Awake()
    {
        Instance = this;
    }

    // UpPathGates
    public void FirstGateTrigger()
    {
        for (int i = 0; i < upPathFirstGateIn.Length; i++)
        {
            upPathFirstGateIn[i].SetActive(!upPathFirstGateIn[i].activeSelf);
        }
    }

    public void FirstGateOut()
    {
        for (int i = 0; i < upPathFirstGateIn.Length; i++)
        {
            upPathFirstGateIn[i].SetActive(!upPathFirstGateIn[i].activeSelf);
        }
        for (int i = 0; i < upPathFirstGateOut.Length; i++)
        {
            upPathFirstGateOut[i].SetActive(!upPathFirstGateOut[i].activeSelf);
        }
    }

    public void SecondGateTrigger()
    {
        for (int i = 0; i < upPathSecondGateIn.Length; i++)
        {
            upPathSecondGateIn[i].SetActive(!upPathSecondGateIn[i].activeSelf);
        }
    }

    // SidePathGates
    public void SideFirstGateTrigger()
    {
        for (int i = 0; i < sidePathFirstGateIn.Length; i++)
        {
            sidePathFirstGateIn[i].SetActive(!sidePathFirstGateIn[i].activeSelf);
        }
    }

    public void SideSecondGateTrigger()
    {
        for (int i = 0; i < sidePathSecondGateIn.Length; i++)
        {
            sidePathSecondGateIn[i].SetActive(!sidePathSecondGateIn[i].activeSelf);
        }
    }

}
