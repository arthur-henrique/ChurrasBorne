using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathControllerFaseTres : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("FASETRESLOCKUM"))
            {
                if(!ManagerOfScenes.instance.isEclipse)
                {
                    EnemyControllerFaseTres.Instance.SpawnFistMob();
                    EnemyControllerFaseTres.Instance.SpawnSecondMob();
                    EnemyControllerFaseTres.Instance.SpawnThirdMob();
                }
                else if(ManagerOfScenes.instance.isEclipse)
                {
                    EnemyControllerFaseTres.Instance.SpawnFistMob();
                    EnemyControllerFaseTres.Instance.SpawnSecondMob();
                    EnemyControllerFaseTres.Instance.SpawnSeventhMob();
                    EnemyControllerFaseTres.Instance.SpawnEigthMob();
                }
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESLOCKDOIS"))
            {
                EnemyControllerFaseTres.Instance.SpawnFourthMob();
                EnemyControllerFaseTres.Instance.SpawnFifthMob();
                if (ManagerOfScenes.instance.isEclipse)
                {
                   gameObject.SetActive(false);
                }
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESLOCKTRES"))
            {
                if(!ManagerOfScenes.instance.isEclipse)
                {
                    EnemyControllerFaseTres.Instance.SpawnSixthMob();
                    EnemyControllerFaseTres.Instance.SpawnSeventhMob();
                }
                else if(ManagerOfScenes.instance.isEclipse)
                {
                    EnemyControllerFaseTres.Instance.SpawnFourthMob();
                    EnemyControllerFaseTres.Instance.SpawnFifthMob();
                }
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESSALAQUATRO"))
            {
                if(!ManagerOfScenes.instance.isEclipse)
                {
                    EnemyControllerFaseTres.Instance.SpawnEigthMob();
                    gameObject.SetActive(false);
                }
                else if(ManagerOfScenes.instance.isEclipse)
                {
                    EnemyControllerFaseTres.Instance.SpawnSixthMob();
                    gameObject.SetActive(false);
                }
                
            }
            else if (gameObject.CompareTag("FASETRESBOSS"))
            {
                FaseTresTriggerController.Instance.CloseTheGates();
                boss.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
