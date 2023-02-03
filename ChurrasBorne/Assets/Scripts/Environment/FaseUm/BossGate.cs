using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public GameObject boss;
    public bool isTutorial,
        isFaseUm,
        isFaseUmHalf,
        isFaseDois,
        isFaseDoisHalf;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTutorial)
            {
                TutorialTriggerController.Instance.SecondGateTrigger();
                gameObject.SetActive(false);
                EnemyControlTutorial.Instance.audioSource.PlayOneShot(EnemyControlTutorial.Instance.gate_open, EnemyControlTutorial.Instance.audioSource.volume);
                GameManager.instance.SwitchToBossCam();
                boss.SetActive(true);
            }
            else if (isFaseUm)
            {
                FaseUmTriggerController.Instance.SecondGateTrigger();
                gameObject.SetActive(false);
                GameManager.instance.SwitchToBossCam();
                boss.SetActive(true);
            }
            else if (isFaseUmHalf)
            {
                FaseUmTriggerController.Instance.SideSecondGateTrigger();
                gameObject.SetActive(false);
                GameManager.instance.SwitchToBossCam();
                boss.SetActive(true);
                EnemyControl.Instance.SpawnBossMob();
            }
            else if (isFaseDois)
            {
                FaseDoisTriggerController.Instance.CloseTheGates();
                gameObject.SetActive(false);
                GameManager.instance.SwitchToBossCam();
                boss.SetActive(true);
            }

            else if (isFaseDoisHalf)
            {
                FaseDoisTriggerController.Instance.CloseTheGates();
                gameObject.SetActive(false);
                GameManager.instance.SwitchToBossCam();
                boss.SetActive(true);
                EnemyControlFaseDois.Instance.SpawnBossMob();
            }
        }
        
    }
}

