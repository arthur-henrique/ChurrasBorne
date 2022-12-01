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
    public GameObject[] sidePathThirdGateIn;

    public GameObject portalToHubP1, portalToHubP2;

    private void Awake()
    {
        Instance = this;
    }
    // UpPathGates

    private void Start()
    {
        SecondGateOpen();
    }
    public void FirstGateTrigger()
    {
        for (int i = 0; i < upPathFirstGateIn.Length; i++)
        {
            upPathFirstGateIn[i].GetComponent<Animator>().SetTrigger("OPENIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
            print("PortãoAberto");
        }
    }

    public void FirstGateOut()
    {
        for (int i = 0; i < upPathFirstGateIn.Length; i++)
        {
            upPathFirstGateIn[i].GetComponent<Animator>().SetTrigger("OPENIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
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
            upPathSecondGateIn[i].GetComponent<Animator>().SetTrigger("CLOSEIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
    }

    public void SecondGateOpen()
    {
        for (int i = 0; i < upPathSecondGateIn.Length; i++)
        {
            upPathSecondGateIn[i].GetComponent<Animator>().SetTrigger("OPENIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
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
            sidePathSecondGateIn[i].GetComponent<Animator>().SetTrigger("CLOSEIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
    }

    public void SideSecondGateOpen()
    {
        for (int i = 0; i < sidePathSecondGateIn.Length; i++)
        {
            sidePathSecondGateIn[i].GetComponent<Animator>().SetTrigger("OPENIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
    }

    public void P1Portal()
    {
        portalToHubP1.SetActive(true);
        SecondGateTrigger();
        GameManager.instance.SetHasCleared(0, true);
    }
    public void P2Portal()
    {
        portalToHubP2.SetActive(true);
        GameManager.instance.SetHasCleared(1, true);
    }
}
