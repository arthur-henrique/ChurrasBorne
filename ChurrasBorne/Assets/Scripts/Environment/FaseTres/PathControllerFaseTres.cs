using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathControllerFaseTres : MonoBehaviour
{
    public GameObject[] triggerUm, triggerDois;
    private void Start()
    {
        triggerUm = GameObject.FindGameObjectsWithTag("FASETRESSALAUM");
        triggerDois = GameObject.FindGameObjectsWithTag("FASETRESSALADOIS");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("FASETRESSALAUM"))
            {
                FaseTresTriggerController.Instance.SalaUmTrigger();
                EnemyControllerFaseTres.Instance.SpawnFistMob();

                for (int i = 0; i < triggerUm.Length; i++)
                {
                    triggerUm[i].SetActive(false);
                }
            }
            else if (gameObject.CompareTag("FASETRESSALADOIS"))
            {
                FaseTresTriggerController.Instance.SalaDoisTrigger();
                EnemyControllerFaseTres.Instance.SpawnSecondMob();

                for (int i = 0; i < triggerDois.Length; i++)
                {
                    triggerDois[i].SetActive(false);
                }
            }
            else if (gameObject.CompareTag("FASETRESSALATRES"))
            {
                FaseTresTriggerController.Instance.SalaTresTrigger();
                EnemyControllerFaseTres.Instance.SpawnThirdMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESSALAQUATRO"))
            {
                FaseTresTriggerController.Instance.SalaQuatroTrigger();
                EnemyControllerFaseTres.Instance.SpawnFourthMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESSALACINCO"))
            {
                FaseTresTriggerController.Instance.SalaCincoTrigger();
                EnemyControllerFaseTres.Instance.SpawnFifthMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESSALASEIS"))
            {
                FaseTresTriggerController.Instance.SalaSeisTrigger();
                EnemyControllerFaseTres.Instance.SpawnSixthMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESSALASETE"))
            {
                FaseTresTriggerController.Instance.SalaSeteTrigger();
                EnemyControllerFaseTres.Instance.SpawnSeventhMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESSALAOITO"))
            {
                FaseTresTriggerController.Instance.SalaOitoTrigger();
                EnemyControllerFaseTres.Instance.SpawnEigthMob();
                gameObject.SetActive(false);
            }
        }
    }
}
