using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool clearedTutorial,
        clearedPhaseOne,
        clearedPhaseOneHalf,
        clearedPhaseTwo,
        clearedPhaseTwoHalf,
        clearedPhaseThree,
        clearedPhaseThreeHalf,
        clearedPhaseFour,
        hasFlask,
        hasCompletedQuestOne,
        hasCompletedQuestTwo,
        hasCompletedQuestThree;
    public bool hasSeenGateTwo;
    public float maxHealth, playerArmor;

    public PlayerData(GameManager gameManager)
    {
        hasSeenGateTwo = gameManager.GetHasSeenGateTwoAnim();
        maxHealth = gameManager.maxHealth;
        playerArmor = gameManager.GetArmor();
        clearedPhaseOne = gameManager.GetHasCleared(0);
        clearedPhaseOneHalf = gameManager.GetHasCleared(1);
        clearedPhaseTwo = gameManager.GetHasCleared(2);
        clearedPhaseTwoHalf = gameManager.GetHasCleared(3);
        clearedPhaseThree = gameManager.GetHasCleared(4);
        clearedPhaseThreeHalf = gameManager.GetHasCleared(5);
        clearedPhaseFour = gameManager.GetHasCleared(6);
        hasFlask = gameManager.HasFlask();
        hasCompletedQuestOne = gameManager.hasCompletedQuestOne;
        hasCompletedQuestTwo = gameManager.hasCompletedQuestTwo;
        hasCompletedQuestThree = gameManager.hasCompletedQuestThree;
    }
}
