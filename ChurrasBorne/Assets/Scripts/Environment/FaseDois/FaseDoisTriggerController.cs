using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseDoisTriggerController : MonoBehaviour
{
    public static FaseDoisTriggerController Instance;
    public GameObject[] salaUmLock;
    public GameObject[] salaDoisLock;
    public GameObject[] salaTresLock;
    public GameObject[] salaQuatroLock;
    public GameObject[] salaCincoLock;
    public GameObject[] salaSeisLock;
    public GameObject[] salaSeteLock;
    public GameObject[] salaOitoLock;

    public GameObject portalToHub;
    private int salasTerminadas;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        salasTerminadas = 0;
    }

    public void SalaUmTrigger()
    {
        for (int i = 0; i < salaUmLock.Length; i++)
        {
            salaUmLock[i].SetActive(!salaUmLock[i].activeSelf);
        }
    }
    public void SalaDoisTrigger()
    {
        for (int i = 0; i < salaDoisLock.Length; i++)
        {
            salaDoisLock[i].SetActive(!salaDoisLock[i].activeSelf);
        }
    }
    public void SalaTresTrigger()
    {
        for (int i = 0; i < salaTresLock.Length; i++)
        {
            salaTresLock[i].SetActive(!salaTresLock[i].activeSelf);
        }
    }
    public void SalaQuatroTrigger()
    {
        for (int i = 0; i < salaQuatroLock.Length; i++)
        {
            salaQuatroLock[i].SetActive(!salaQuatroLock[i].activeSelf);
        }
    }
    public void SalaCincoTrigger()
    {
        for (int i = 0; i < salaCincoLock.Length; i++)
        {
            salaCincoLock[i].SetActive(!salaCincoLock[i].activeSelf);
        }
    }
    public void SalaSeisTrigger()
    {
        for (int i = 0; i < salaSeisLock.Length; i++)
        {
            salaSeisLock[i].SetActive(!salaSeisLock[i].activeSelf);
        }
    }
    public void SalaSeteTrigger()
    {
        for (int i = 0; i < salaSeteLock.Length; i++)
        {
            salaSeteLock[i].SetActive(!salaSeteLock[i].activeSelf);
        }
    }
    public void SalaOitoTrigger()
    {
        for (int i = 0; i < salaOitoLock.Length; i++)
        {
            salaOitoLock[i].SetActive(!salaOitoLock[i].activeSelf);
        }
    }

    public void ContadorDeSalasTerminadas()
    {
        salasTerminadas++;
    }
}
