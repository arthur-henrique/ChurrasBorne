using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl Instance;
    private readonly List<GameObject> firstMob = new List<GameObject>();
    private readonly List<GameObject> secondMob = new List<GameObject>();
    private readonly List<GameObject> thirdMob = new List<GameObject>();
    private readonly List<GameObject> fourthMob = new List<GameObject>();
    private readonly List<GameObject> fifthMob = new List<GameObject>();
    private readonly List<GameObject> sixthMob = new List<GameObject>();
    public GameObject tronco, troncosHalf;
    private bool clearedUm, clearedHalf;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        clearedUm = GameManager.instance.GetHasCleared(0);
        clearedHalf = GameManager.instance.GetHasCleared(1);

        firstMob.AddRange(GameObject.FindGameObjectsWithTag("P1-MOBUM"));
        secondMob.AddRange(GameObject.FindGameObjectsWithTag("P1-MOBDOIS"));
        thirdMob.AddRange(GameObject.FindGameObjectsWithTag("P1-MOBTRES"));
        fourthMob.AddRange(GameObject.FindGameObjectsWithTag("P1-MOBQUATRO"));
        fifthMob.AddRange(GameObject.FindGameObjectsWithTag("P1-MOBCINCO"));
        sixthMob.AddRange(GameObject.FindGameObjectsWithTag("P1-MOBSEIS"));
        if (clearedUm && !clearedHalf)
        {
            print("MobsHalf");
            for (int i = 0; i < firstMob.Count; i++)
            {
                firstMob[i].SetActive(false);
            }
            //firstMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUMHALF_SALAUMMOB"));
            //secondMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUMHALF_SALAUMMOBDOIS"));
            //thirdMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUMHALF_SALADOISMOB"));
            //fourthMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUMHALF_FIRSTROUND"));
            //fifthMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUMHALF_SECONDROUND"));
            //sixthMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUMHALF_FINALROUND"));
            for (int i = 0; i < firstMob.Count; i++)
            {
                firstMob[i].SetActive(true);
            }
        }
        for (int i = 0; i < secondMob.Count; i++)
        {
            secondMob[i].SetActive(false);
        }
        for (int i = 0; i < thirdMob.Count; i++)
        {
            thirdMob[i].SetActive(false);
        }
        for (int i = 0; i < fourthMob.Count; i++)
        {
            fourthMob[i].SetActive(false);
        }
        for (int i = 0; i < fifthMob.Count; i++)
        {
            fifthMob[i].SetActive(false);
        }
        for (int i = 0; i < sixthMob.Count; i++)
        {
            sixthMob[i].SetActive(false);
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
        else if (fourthMob.Contains(enemy))
        {
            fourthMob.Remove(enemy);
            IsFourthMobCleared();
        }
        else if (fifthMob.Contains(enemy))
        {
            fifthMob.Remove(enemy);
            IsFifthMobCleared();
        }
        else if (sixthMob.Contains(enemy))
        {
            sixthMob.Remove(enemy);
            IsSixthMobCleared();
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
    public void SpawnFourthMob()
    {
        // Spawns the first round
        for (int i = 0; i < fourthMob.Count; i++)
        {
            fourthMob[i].SetActive(true);
        }
    }
    public void BeginSecondRound()
    {
        for (int i = 0; i < fifthMob.Count; i++)
        {
            fifthMob[i].SetActive(true);
        }
    }
    public void BeginFinalRound()
    {
        for (int i = 0; i < sixthMob.Count; i++)
        {
            sixthMob[i].SetActive(true);
        }
    }

    public void IsFirstMobCleared()
    {
        if(firstMob.Count <= 0)
        {
            SpawnSecondMob();
        }
    }

    public void IsSecondMobCleared()
    {
        if(secondMob.Count <= 0)
        {
            tronco.SetActive(false);
            if(clearedUm)
            {
                troncosHalf.SetActive(true);
                // Abre o portão do jardim
            }
            SpawnThirdMob();
        }
    }

    public void IsThirdMobCleared()
    {
        if (thirdMob.Count <= 0)
        {
            if(!clearedUm)
            {
                FaseUmTriggerController.Instance.FirstGateTrigger();
            }
            if (clearedUm)
                SpawnFourthMob();
        }
    }
    public void IsFourthMobCleared()
    {
        if (fourthMob.Count <= 0)
        {
            if (!clearedUm)
            {
                BeginSecondRound();
            }
            if(clearedUm)
            {
                // Opens checkpoint Gate
            }
        }
    }
    public void IsFifthMobCleared()
    {
        if (fifthMob.Count <= 0)
        {
            BeginFinalRound();
        }
    }
    public void IsSixthMobCleared()
    {
        if (sixthMob.Count <= 0)
        {
            FaseUmTriggerController.Instance.FirstGateOut();
        }
    }

}
