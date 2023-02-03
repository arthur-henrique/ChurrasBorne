using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlFaseDois : MonoBehaviour
{
    public static EnemyControlFaseDois Instance;
    public GameObject p1, p2;
    private readonly List<GameObject> firstMob = new List<GameObject>();
    private readonly List<GameObject> secondMob = new List<GameObject>();
    private readonly List<GameObject> thirdMob = new List<GameObject>();
    private readonly List<GameObject> fourthMob = new List<GameObject>();
    private readonly List<GameObject> fifthMob = new List<GameObject>();
    private readonly List<GameObject> sixthMob = new List<GameObject>();
    private readonly List<GameObject> seventhMob = new List<GameObject>();
    private readonly List<GameObject> eigthMob = new List<GameObject>();
    private readonly List<GameObject> bossMob = new List<GameObject>();
    private readonly List<UnityEngine.Experimental.Rendering.Universal.Light2D> ltds = new List<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    private bool clearedDois, clearedHalf;
    private int randomTL;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ltds.AddRange(FindObjectsOfType<UnityEngine.Experimental.Rendering.Universal.Light2D>());
        for (int i = 0; i < ltds.Count; i++)
        {
            if (ltds[i].lightType == UnityEngine.Experimental.Rendering.Universal.Light2D.LightType.Global)
            {
                ltds.Remove(ltds[i]);
            }
        }
        for (int i = 0; i < ltds.Count; i++)
        {
            if (ltds[i].CompareTag("Player"))
            {
                ltds.Remove(ltds[i]);
            }
        }
        clearedDois = GameManager.instance.GetHasCleared(2);
        clearedHalf = GameManager.instance.GetHasCleared(3);
        randomTL = ManagerOfScenes.randomTimeline;

        if (!clearedDois && !clearedHalf)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            ltds.ForEach(x => x.intensity = 7f);
        }
        else if (clearedDois && !clearedHalf)
        {
            p1.SetActive(false);
            p2.SetActive(true);
            ltds.ForEach(x => x.intensity = 3f);
        }
        else if (clearedDois && clearedHalf)
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
                ltds.ForEach(x => x.intensity = 3f);

            }
        }

        firstMob.AddRange(GameObject.FindGameObjectsWithTag("MOBUM"));
        secondMob.AddRange(GameObject.FindGameObjectsWithTag("MOBDOIS"));
        thirdMob.AddRange(GameObject.FindGameObjectsWithTag("MOBTRES"));
        fourthMob.AddRange(GameObject.FindGameObjectsWithTag("MOBQUATRO"));
        fifthMob.AddRange(GameObject.FindGameObjectsWithTag("MOBCINCO"));
        sixthMob.AddRange(GameObject.FindGameObjectsWithTag("MOBSEIS"));
        seventhMob.AddRange(GameObject.FindGameObjectsWithTag("MOBSETE"));
        eigthMob.AddRange(GameObject.FindGameObjectsWithTag("MOBOITO"));
        bossMob.AddRange(GameObject.FindGameObjectsWithTag("MOBBOSS"));

        firstMob.ForEach(x => x.SetActive(false));
        secondMob.ForEach(x => x.SetActive(false));
        thirdMob.ForEach(x => x.SetActive(false));
        fourthMob.ForEach(x => x.SetActive(false));
        fifthMob.ForEach(x => x.SetActive(false));
        sixthMob.ForEach(x => x.SetActive(false));
        seventhMob.ForEach(x => x.SetActive(false));
        eigthMob.ForEach(x => x.SetActive(false));
        bossMob.ForEach(x => x.SetActive(false));

    }

    // FunÁ„o de checagem de morte
    public void KilledEnemy(GameObject enemy)
    {
        if (firstMob.Contains(enemy))
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
        if (seventhMob.Contains(enemy))
        {
            seventhMob.Remove(enemy);
            IsSeventhMobCleared();
        }
        if (eigthMob.Contains(enemy))
        {
            eigthMob.Remove(enemy);
            IsEigthMobCleared();
        }
        if (bossMob.Contains(enemy))
        {
            bossMob.Remove(enemy);
            IsBossMobCleared();
        }
    }
    // Spawnar Mobs
    public void SpawnFistMob()
    {
        firstMob.ForEach(x => x.SetActive(true));
    }
    public void SpawnSecondMob()
    {
        secondMob.ForEach(x => x.SetActive(true));
    }
    public void SpawnThirdMob()
    {
        thirdMob.ForEach(x => x.SetActive(true));
    }
    public void SpawnFourthMob()
    {
        fourthMob.ForEach(x => x.SetActive(true));
    }
    public void SpawnFifthMob()
    {
        fifthMob.ForEach(x => x.SetActive(true));       
    }
    public void SpawnSixthMob()
    {
        sixthMob.ForEach(x => x.SetActive(true));       
    }
    public void SpawnSeventhMob()
    {
        seventhMob.ForEach(x => x.SetActive(true));       
    }
    public void SpawnEigthMob()
    {
        eigthMob.ForEach(x => x.SetActive(true));
    }

    public void SpawnBossMob()
    {
        bossMob.ForEach(x => x.SetActive(true));
    }
    // Fazer quando lista estÅEvazia:
    public void IsFirstMobCleared()
    {
        if (firstMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaUmTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }
    public void IsSecondMobCleared()
    {
        if (secondMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaDoisTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }
    public void IsThirdMobCleared()
    {
        if (thirdMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaTresTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }
    public void IsFourthMobCleared()
    {
        if (fourthMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaQuatroTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }
    public void IsFifthMobCleared()
    {
        if (fifthMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaCincoTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }
    public void IsSixthMobCleared()
    {
        if (sixthMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaSeisTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }
    public void IsSeventhMobCleared()
    {
        if (seventhMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaSeteTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }
    public void IsEigthMobCleared()
    {
        if (eigthMob.Count <= 0)
        {
            FaseDoisTriggerController.Instance.SalaOitoTrigger();
            FaseDoisTriggerController.Instance.ContadorDeSalasTerminadas();
        }
    }

    public void IsBossMobCleared()
    {
        if (bossMob.Count <= 0)
        {
            GateChecker.Instance.MobsDied();
        }
    }
}
