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

    // Eclipse
    public GameObject[] eclipseFirstLock;


    public GameObject portalToHub;
    public int inimigosMortos;
    public CinemachineVirtualCamera gate;
    public GameObject[] gateCamPos;
    private bool hasOpenfirst = false, hasOpensecond = false, hasOpenthird = false, hasPlayedSound = false;
    public GameObject normalLock8, eclipseLock6, normalBossLock, eclipseBossLock;
    public GameObject zonaSeteOpenGate, zonaSeteClosedGate;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        inimigosMortos = 0;
    }

    private void Start()
    {
        inimigosMortos = 0;
        if (!ManagerOfScenes.instance.isEclipse)
        {
            normalLock8.SetActive(true);
            normalBossLock.SetActive(true);
            eclipseLock6.SetActive(false);
            eclipseBossLock.SetActive(false);

            zonaSeteOpenGate.SetActive(true);
            zonaSeteClosedGate.SetActive(false);
        }
        else if(ManagerOfScenes.instance.isEclipse)
        {
            normalLock8.SetActive(false);
            normalBossLock.SetActive(false);
            eclipseLock6.SetActive(true);
            eclipseBossLock.SetActive(true);

            zonaSeteOpenGate.SetActive(false);
            zonaSeteClosedGate.SetActive(true);
        }
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

    // Eclipse
    public void SalaUmEclipse()
    {
        for (int i = 0; i < eclipseFirstLock.Length; i++)
        {
            eclipseFirstLock[i].GetComponent<Animator>().SetTrigger("OPENIT");
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
        if(!ManagerOfScenes.instance.isEclipse)
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
            else if (inimigosMortos >= 53 && !hasOpensecond)
            {
                hasOpensecond = true;
                gate.transform.position = gateCamPos[1].transform.position;
                GameManager.instance.GateCamSetter(gate);
                StartCoroutine(FreezeTime());
                GameManager.instance.GateCAM();
                StartCoroutine(OpenTheGates(2));
            }
            else if (inimigosMortos > 99 && !hasOpenthird)
            {
                hasOpenthird = true;
                gate.transform.position = gateCamPos[2].transform.position;
                GameManager.instance.GateCamSetter(gate);
                StartCoroutine(FreezeTime());
                GameManager.instance.GateCAM();
                StartCoroutine(OpenTheGates(3));
            }
        }
        else if(ManagerOfScenes.instance.isEclipse)
        {
            if (inimigosMortos >= 50 && !hasOpenfirst)
            {
                hasOpenfirst = true;
                gate.transform.position = gateCamPos[4].transform.position;
                GameManager.instance.GateCamSetter(gate);
                StartCoroutine(FreezeTime());
                GameManager.instance.GateCAM();
                StartCoroutine(OpenTheGates(4));

            }
            else if (inimigosMortos >= 80 && !hasOpensecond)
            {
                hasOpensecond = true;
                gate.transform.position = gateCamPos[1].transform.position;
                GameManager.instance.GateCamSetter(gate);
                StartCoroutine(FreezeTime());
                GameManager.instance.GateCAM();
                StartCoroutine(OpenTheGates(2));
            }
            else if (inimigosMortos > 120 && !hasOpenthird)
            {
                hasOpenthird = true;
                gate.transform.position = gateCamPos[5].transform.position;
                GameManager.instance.GateCamSetter(gate);
                StartCoroutine(FreezeTime());
                GameManager.instance.GateCAM();
                StartCoroutine(OpenTheGates(5));
            }
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
        SalaBossOutTrigger();
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
        else if(sequence == 4)
        {
            SalaUmEclipse();
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
        else if(sequence == 5)
        {
            SalaBossOutTrigger();
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
