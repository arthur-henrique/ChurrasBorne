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
        if(isTutorial)
        {
            TutorialTriggerController.Instance.SecondGateTrigger();
            gameObject.SetActive(false);
            boss.SetActive(true);
        }
        if(isFaseUm)
        {
            if (other.CompareTag("Player"))
            {
                FaseUmTriggerController.Instance.SecondGateTrigger();
                gameObject.SetActive(false);
                boss.SetActive(true);
            }
        }
        if (isFaseUmHalf)
        {
            if (other.CompareTag("Player"))
            {
                FaseUmTriggerController.Instance.SideSecondGateTrigger();
                gameObject.SetActive(false);
                boss.SetActive(true);
            }
        }
    }
}
