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

    public Animator preBossAnim;
    public Animator bossAnim;
    public Animator preBossAnimEc;
    public Animator bossAnimEc;

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
    public void BossFireSet()
    {
        salasTerminadas = 4;
    }

    private void FixedUpdate()
    {
        if(salasTerminadas > 3)
        {
            salasTerminadas = 0;
            GameManager.instance.GateCAM();
            StartCoroutine(OpenTheGates());
            if(!GameManager.instance.faseumBossFire)
                GameManager.instance.faseumBossFire = true;

        }
    }

    public void GateOpener()
    {
        StartCoroutine(OpenTheGates());
    }

    public void CloseTheGates()
    {
        if (EnemyControlFaseDois.Instance.p1.activeSelf == true && EnemyControlFaseDois.Instance.p2.activeSelf == false)
        {
            preBossAnim.SetTrigger("CLOSEIT");
            bossAnim.SetTrigger("CLOSEIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
        if (EnemyControlFaseDois.Instance.p1.activeSelf == false && EnemyControlFaseDois.Instance.p2.activeSelf == true)
        {
            preBossAnimEc.SetTrigger("CLOSEIT");
            bossAnimEc.SetTrigger("CLOSEIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
    }
    IEnumerator OpenTheGates()
    {
        yield return new WaitForSeconds(2);
        if(EnemyControlFaseDois.Instance.p1.activeSelf == true && EnemyControlFaseDois.Instance.p2.activeSelf == false)
        {
            preBossAnim.SetTrigger("OPENIT");
            bossAnim.SetTrigger("OPENIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }
        if (EnemyControlFaseDois.Instance.p1.activeSelf == false && EnemyControlFaseDois.Instance.p2.activeSelf == true)
        {
            preBossAnimEc.SetTrigger("OPENIT");
            bossAnimEc.SetTrigger("OPENIT");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.gateOpen, GameManager.instance.audioSource.volume);
        }       
    }
}
