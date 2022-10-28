using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl Instance;
    private readonly List<GameObject> firstMob = new List<GameObject>();
    private readonly List<GameObject> secondMob = new List<GameObject>();
    private readonly List<GameObject> thirdMob = new List<GameObject>();
    private readonly List<GameObject> firstRound = new List<GameObject>();
    private readonly List<GameObject> secondRound = new List<GameObject>();
    private readonly List<GameObject> finalRound = new List<GameObject>();
    public GameObject tronco;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        firstMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_SALAUMMOB"));
        secondMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_SALAUMMOBDOIS"));
        thirdMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_SALADOISMOB"));
        firstRound.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_FIRSTROUND"));
        secondRound.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_SECONDROUND"));
        finalRound.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_FINALROUND"));
        for (int i = 0; i < secondMob.Count; i++)
        {
            secondMob[i].SetActive(false);
        }
        for (int i = 0; i < thirdMob.Count; i++)
        {
            thirdMob[i].SetActive(false);
        }
        for (int i = 0; i < firstRound.Count; i++)
        {
            firstRound[i].SetActive(false);
        }
        for (int i = 0; i < secondRound.Count; i++)
        {
            secondRound[i].SetActive(false);
        }
        for (int i = 0; i < finalRound.Count; i++)
        {
            finalRound[i].SetActive(false);
        }
    }

    public void KilledEnemy(GameObject enemy)
    {
        if(firstMob.Contains(enemy))
        {
            firstMob.Remove(enemy);
            IsFirstMobCleared();
        }
        else if (secondMob.Contains(enemy))
        {
            secondMob.Remove(enemy);
            IsSecondMobCleared();
        }
        else if (thirdMob.Contains(enemy))
        {
            thirdMob.Remove(enemy);
            IsThirdMobCleared();
        }
        else if (firstRound.Contains(enemy))
        {
            firstRound.Remove(enemy);
            IsFirstRoundCleared();
        }
        else if (secondRound.Contains(enemy))
        {
            secondRound.Remove(enemy);
            IsSecondRoundCleared();
        }
        else if (finalRound.Contains(enemy))
        {
            finalRound.Remove(enemy);
            IsFinalRoundCleared();
        }
    }

    public void SpawnSecondMob()
    {
        for (int i = 0; i < secondMob.Count; i++)
        {
            secondMob[i].SetActive(true);
        }
    }
    public void SpawnThirdMob()
    {
        for (int i = 0; i < thirdMob.Count; i++)
        {
            thirdMob[i].SetActive(true);
        }
    }
    public void BeginArena()
    {
        // Spawns the first round
        for (int i = 0; i < firstRound.Count; i++)
        {
            firstRound[i].SetActive(true);
        }
    }
    public void BeginSecondRound()
    {
        for (int i = 0; i < secondRound.Count; i++)
        {
            secondRound[i].SetActive(true);
        }
    }
    public void BeginFinalRound()
    {
        for (int i = 0; i < finalRound.Count; i++)
        {
            finalRound[i].SetActive(true);
        }
    }

    public void IsFirstMobCleared()
    {
        if(firstMob.Count <= 0)
        {
            // Call the event that spawns the second wave
            SpawnSecondMob();
        }
    }

    public void IsSecondMobCleared()
    {
        if(secondMob.Count <= 0)
        {
            tronco.SetActive(false);
            SpawnThirdMob();
        }
    }

    public void IsThirdMobCleared()
    {
        if (thirdMob.Count <= 0)
        {
            FaseUmTriggerController.Instance.FirstGateTrigger();
        }
    }
    public void IsFirstRoundCleared()
    {
        if (firstRound.Count <= 0)
        {
            BeginSecondRound();
        }
    }
    public void IsSecondRoundCleared()
    {
        if (secondRound.Count <= 0)
        {
            BeginFinalRound();
        }
    }
    public void IsFinalRoundCleared()
    {
        if (finalRound.Count <= 0)
        {
            FaseUmTriggerController.Instance.FirstGateOut();
        }
    }

}
