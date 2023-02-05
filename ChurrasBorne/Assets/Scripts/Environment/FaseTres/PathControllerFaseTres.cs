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
                EnemyControllerFaseTres.Instance.SpawnFistMob();
                EnemyControllerFaseTres.Instance.SpawnSecondMob();
                EnemyControllerFaseTres.Instance.SpawnThirdMob();

            }
            else if (gameObject.CompareTag("FASETRESLOCKDOIS"))
            {
                EnemyControllerFaseTres.Instance.SpawnFourthMob();
                EnemyControllerFaseTres.Instance.SpawnFifthMob();


            }
            else if (gameObject.CompareTag("FASETRESLOCKTRES"))
            {
                EnemyControllerFaseTres.Instance.SpawnSixthMob();
                EnemyControllerFaseTres.Instance.SpawnSeventhMob();
                gameObject.SetActive(false);


                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("FASETRESSALAQUATRO"))
            {
                EnemyControllerFaseTres.Instance.SpawnEigthMob();
                gameObject.SetActive(false);
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
