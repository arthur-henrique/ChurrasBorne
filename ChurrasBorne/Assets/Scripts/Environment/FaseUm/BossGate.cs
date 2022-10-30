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
            boss.SetActive(true);
            gameObject.SetActive(false);
        }
        if(isFaseUm)
        {
            if (other.CompareTag("Player"))
            {
                FaseUmTriggerController.Instance.SecondGateTrigger();
                boss.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
