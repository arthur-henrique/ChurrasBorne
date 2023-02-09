using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseTresTriggerController : MonoBehaviour
{
    public static FaseTresTriggerController Instance;
    // Start is called before the first frame update
    public GameObject[] firstLock;
    public GameObject[] secondLock;
    public GameObject[] thirdLock;
    public GameObject[] fourthLock;
    public GameObject[] fifthLock;


    public GameObject portalToHub;
    public int inimigosMortos;
    public CinemachineVirtualCamera gate;
    public GameObject[] gateCamPos;
    private bool hasOpenfirst = false, hasOpensecond = false, hasOpenthird = false, hasPlayedSound = false;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        inimigosMortos = 0;
    }

    public void SalaUmTrigger()
    {
        for (int i = 0; i < firstLock.Length; i++)
        {
            if (firstLock[i].activeSelf == true)
            firstLock[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
    }
    public void SalaDoisTrigger()
    {
        for (int i = 0; i < secondLock.Length; i++)
        {
            secondLock[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
    }
    public void SalaBossInTrigger()
    {
        for (int i = 0; i < thirdLock.Length; i++)
        {
            thirdLock[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
    }
    public void SalaBossOutTrigger()
    {
        for (int i = 0; i < fourthLock.Length; i++)
        {
            fourthLock[i].SetActive(!fourthLock[i].activeSelf);
        }
    }
    

    public void ContadorDeInimigosMortos()
    {
        inimigosMortos++;
    }

    private void FixedUpdate()
    {
        if (inimigosMortos >= 28 && !hasOpenfirst)
        {
            hasOpenfirst = true;
            gate.transform.position = gateCamPos[0].transform.position;
            GameManager.instance.GateCamSetter(gate);
            StartCoroutine(FreezeTime());
            GameManager.instance.GateCAM();
            StartCoroutine(OpenTheGates(1));

        }
        else if(inimigosMortos >= 53 && !hasOpensecond)
        {
            hasOpensecond = true;
            gate.transform.position = gateCamPos[1].transform.position;
            GameManager.instance.GateCamSetter(gate);
            StartCoroutine(FreezeTime());
            GameManager.instance.GateCAM();
            StartCoroutine(OpenTheGates(2));
        }
        else if(inimigosMortos > 99 && !hasOpenthird)
        {
            hasOpenthird = true;
            gate.transform.position = gateCamPos[2].transform.position;
            GameManager.instance.GateCamSetter(gate);
            StartCoroutine(FreezeTime());
            GameManager.instance.GateCAM();
            StartCoroutine(OpenTheGates(3));
        }
    }

    public void GateOpener()
    {
        StartCoroutine(BossWasKilled());
    }

    public void CloseTheGates()
    {
        for (int i = 0; i < thirdLock.Length; i++)
        {
            thirdLock[i].GetComponent<Animator>().SetTrigger("CLOSEIT");
        }
    }
    IEnumerator OpenTheGates(int sequence)
    {
        yield return new WaitForSecondsRealtime(2f);
        
        if(sequence == 1)
        {
            SalaUmTrigger();
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
        else if (sequence == 2)
        {
            SalaDoisTrigger();
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
        else if (sequence == 3)
        {
            SalaBossInTrigger();
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
    }
    IEnumerator BossWasKilled()
    {
        gate.transform.position = gateCamPos[3].transform.position;
        yield return new WaitForSeconds(1);
        GameManager.instance.GateCamSetter(gate);
        GameManager.instance.GateCAM();
        SalaBossInTrigger();
        SalaBossOutTrigger();
        for (int i = 0; i < thirdLock.Length; i++)
        {
            fifthLock[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
    }

    IEnumerator FreezeTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(7);
        Time.timeScale = 1;
    }
}
