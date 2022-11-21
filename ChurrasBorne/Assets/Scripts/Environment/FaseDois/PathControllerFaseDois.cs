using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathControllerFaseDois : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(gameObject.CompareTag("FASEDOISSALAUM"))
            {
                FaseDoisTriggerController.Instance.SalaUmTrigger();
                EnemyControlFaseDois.Instance.SpawnFistMob();
                gameObject.SetActive(false);
            }
            else if(gameObject.CompareTag("FASEDOISSALADOIS"))
            {
                FaseDoisTriggerController.Instance.SalaDoisTrigger();
                EnemyControlFaseDois.Instance.SpawnSecondMob();
                gameObject.SetActive(false);
            }
            else if(gameObject.CompareTag("FASEDOISSALATRES"))
            {
                FaseDoisTriggerController.Instance.SalaTresTrigger();
                EnemyControlFaseDois.Instance.SpawnThirdMob();
                gameObject.SetActive(false);
            }
            else if(gameObject.CompareTag("FASEDOISSALAQUATRO"))
            {
                FaseDoisTriggerController.Instance.SalaQuatroTrigger();
                EnemyControlFaseDois.Instance.SpawnFourthMob();
                gameObject.SetActive(false);
            }
            else if(gameObject.CompareTag("FASEDOISSALACINCO"))
            {
                FaseDoisTriggerController.Instance.SalaCincoTrigger();
                EnemyControlFaseDois.Instance.SpawnFifthMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASEDOISSALASEIS"))
            {
                FaseDoisTriggerController.Instance.SalaSeisTrigger();
                EnemyControlFaseDois.Instance.SpawnSixthMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASEDOISSALASETE"))
            {
                FaseDoisTriggerController.Instance.SalaSeteTrigger();
                EnemyControlFaseDois.Instance.SpawnSeventhMob();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASEDOISSALAOITO"))
            {
                FaseDoisTriggerController.Instance.SalaOitoTrigger();
                EnemyControlFaseDois.Instance.SpawnEigthMob();
                gameObject.SetActive(false);
            }
        }
    }
}
