using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl Instance;
    public GameObject p1, p2;
    private readonly List<GameObject> firstMob = new List<GameObject>();
    private readonly List<GameObject> secondMob = new List<GameObject>();
    private readonly List<GameObject> thirdMob = new List<GameObject>();
    private readonly List<GameObject> fourthMob = new List<GameObject>();
    private readonly List<GameObject> fifthMob = new List<GameObject>();
    private readonly List<GameObject> sixthMob = new List<GameObject>();
    private readonly List<UnityEngine.Experimental.Rendering.Universal.Light2D> ltds = new List<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    public GameObject troncosHalf;
    private bool clearedUm, clearedHalf;
    private int randomTL;
    //public UnityEngine.Experimental.Rendering.Universal.Light2D[] ltds;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        ltds.AddRange(FindObjectsOfType<UnityEngine.Experimental.Rendering.Universal.Light2D>());
        for (int i = 0; i < ltds.Count; i++)
        {
            if (ltds[i].lightType == UnityEngine.Experimental.Rendering.Universal.Light2D.LightType.Global)
            {
                ltds.Remove(ltds[i]);
            }
        }
        for (int i = 0; i <ltds.Count; i++)
        {
            if (ltds[i].CompareTag("Player"))
            {
                ltds.Remove(ltds[i]);
            }
        }
        clearedUm = GameManager.instance.GetHasCleared(0);
        clearedHalf = GameManager.instance.GetHasCleared(1);
        randomTL = ManagerOfScenes.randomTimeline;

        if (!clearedUm && !clearedHalf)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            ltds.ForEach(x => x.intensity = 7f);
        }
        else if (clearedUm && !clearedHalf)
        {
            p1.SetActive(false);
            p2.SetActive(true);
            ltds.ForEach(x => x.intensity = 3f);
        }
        else if (clearedUm && clearedHalf)
        {
            if (randomTL == 1)
            {
                p1.SetActive(true);
                p2.SetActive(false);
                ltds.ForEach(x => x.intensity = 7f);
            }
            else if (randomTL == 2)
            {
                p1.SetActive(false);
                p2.SetActive(true);
                ltds.ForEach(x => x.intensity = 7f);
            }
        }

        firstMob.AddRange(GameObject.FindGameObjectsWithTag("MOBUM"));
        secondMob.AddRange(GameObject.FindGameObjectsWithTag("MOBDOIS"));
        thirdMob.AddRange(GameObject.FindGameObjectsWithTag("MOBTRES"));
        fourthMob.AddRange(GameObject.FindGameObjectsWithTag("MOBQUATRO"));
        fifthMob.AddRange(GameObject.FindGameObjectsWithTag("MOBCINCO"));
        sixthMob.AddRange(GameObject.FindGameObjectsWithTag("MOBSEIS"));

        firstMob.ForEach(x => x.SetActive(false));
        secondMob.ForEach(x => x.SetActive(false));
        thirdMob.ForEach(x => x.SetActive(false));
        fourthMob.ForEach(x => x.SetActive(false));
        fifthMob.ForEach(x => x.SetActive(false));
        sixthMob.ForEach(x => x.SetActive(false));
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
            print(secondMob.Count);
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

    public void SpawnFirstMob()
    {
        for (int i = 0; i < firstMob.Count; i++)
        {
            firstMob[i].SetActive(true);
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
    public void SpawnFifthMob()
    {
        for (int i = 0; i < fifthMob.Count; i++)
        {
            fifthMob[i].SetActive(true);
        }
    }
    public void SpawnSixthMob()
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
            // abre o portão da wave
            FaseUmTriggerController.Instance.secondWaveCleared();
            if (!clearedUm)
                SpawnThirdMob();
            if(clearedUm)
            {
                // abre o portão da wave
                troncosHalf.SetActive(true);
                FaseUmTriggerController.Instance.SideFirstGateTrigger();
            }
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
                FaseUmTriggerController.Instance.SideSecondGateOpen();
        }
    }
    public void IsFourthMobCleared()
    {
        if (fourthMob.Count <= 0)
        {
            if (!clearedUm)
            {
                SpawnFifthMob();
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
            SpawnSixthMob();
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
