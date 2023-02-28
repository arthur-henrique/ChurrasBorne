using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathControllerFaseTres : MonoBehaviour
{
    public GameObject boss, gridMaster;
    private ManagerOfScenes manager;

    private void Start()
    {
        manager = FindObjectOfType<ManagerOfScenes>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!manager.isEclipse)
        {
            if (other.CompareTag("Player"))
            {
                if (gameObject.CompareTag("FASETRESLOCKUM"))
                {
                    EnemyControllerFaseTres.Instance.SpawnFistMob();
                    EnemyControllerFaseTres.Instance.SpawnSecondMob();
                    EnemyControllerFaseTres.Instance.SpawnThirdMob();
                    gameObject.SetActive(false);

                }
                else if (gameObject.CompareTag("FASETRESLOCKDOIS"))
                {
                    EnemyControllerFaseTres.Instance.SpawnFourthMob();
                    EnemyControllerFaseTres.Instance.SpawnFifthMob();
                    gameObject.SetActive(false);

                }
                else if (gameObject.CompareTag("FASETRESLOCKTRES"))
                {
                    EnemyControllerFaseTres.Instance.SpawnSixthMob();
                    EnemyControllerFaseTres.Instance.SpawnSeventhMob();
                    gameObject.SetActive(false);
                }
                else if (gameObject.CompareTag("FASETRESLOCKQUATRO"))
                {
                    EnemyControllerFaseTres.Instance.SpawnEigthMob();
                    gameObject.SetActive(false);
                }
                else if (gameObject.CompareTag("FASETRESBOSS"))
                {
                    FaseTresTriggerController.Instance.CloseTheGates();
                    boss.SetActive(true);
                    gridMaster.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
        else if(manager.isEclipse)
        {
            if(other.CompareTag("Player"))
            {
                if (gameObject.CompareTag("FASETRESLOCKUM"))
                {
                    EnemyControllerFaseTres.Instance.SpawnFistMob();
                    EnemyControllerFaseTres.Instance.SpawnSecondMob();
                    EnemyControllerFaseTres.Instance.SpawnSeventhMob();
                    EnemyControllerFaseTres.Instance.SpawnEigthMob();
                    gameObject.SetActive(false);

                }
                else if (gameObject.CompareTag("FASETRESLOCKDOIS"))
                {
                    EnemyControllerFaseTres.Instance.SpawnSixthMob();
                    gameObject.SetActive(false);
                }
                else if (gameObject.CompareTag("FASETRESLOCKTRES"))
                {
                    EnemyControllerFaseTres.Instance.SpawnFourthMob();
                    EnemyControllerFaseTres.Instance.SpawnFifthMob();
                    gameObject.SetActive(false);
                }
                else if (gameObject.CompareTag("FASETRESBOSS"))
                {
                    FaseTresTriggerController.Instance.CloseTheGatesEclipse();
                    boss.SetActive(true);
                    gridMaster.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
        
    }
}
