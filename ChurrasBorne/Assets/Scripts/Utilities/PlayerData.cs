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
        clearedPhaseTwoHalf;
    public bool hasSeenGateTwo;
    public int maxHealth;

    public PlayerData(GameManager gameManager)
    {
        hasSeenGateTwo = gameManager.GetHasSeenGateTwoAnim();
        maxHealth = gameManager.maxHealth;
        clearedPhaseOne = gameManager.GetHasCleared(0);
        clearedPhaseOneHalf = gameManager.GetHasCleared(1);
        clearedPhaseTwo = gameManager.GetHasCleared(2);
        clearedPhaseTwoHalf = gameManager.GetHasCleared(3);
    }
}
