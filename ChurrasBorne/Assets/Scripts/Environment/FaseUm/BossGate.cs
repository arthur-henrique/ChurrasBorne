using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public GameObject boss;
    public bool isTutorial,
        isFaseUm,
        isFaseUmHalf;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTutorial)
            {
                TutorialTriggerController.Instance.SecondGateTrigger();
                gameObject.SetActive(false);
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
                boss.SetActive(true);
            }
        }
        
    }
}

