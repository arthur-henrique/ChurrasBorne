using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerFaseTres : MonoBehaviour
{
    public static EnemyControllerFaseTres Instance;
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
    private bool clearedTres, clearedTresHalf;
    private int randomTL;

    public Collider2D[] mobTriggers;
    private ManagerOfScenes manager;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        manager = FindObjectOfType<ManagerOfScenes>();

        ltds.AddRange(FindObjectsOfType<UnityEngine.Experimental.Rendering.Universal.Light2D>());
        for (int i = 0; i < ltds.Count; i++)
        {
            if (ltds[i].lightType != UnityEngine.Experimental.Rendering.Universal.Light2D.LightType.Point)
            {
                ltds.Remove(ltds[i]);
            }
        }
        for (int i = 0; i < ltds.Count; i++)
        {
            if (ltds[i].lightType != UnityEngine.Experimental.Rendering.Universal.Light2D.LightType.Point)
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
        for (int i = 0; i < ltds.Count; i++)
        {
            if (ltds[i].CompareTag("Fish"))
            {
                ltds.Remove(ltds[i]);
            }
        }
        for (int i = 0; i < ltds.Count; i++)
        {
            if (ltds[i].CompareTag("Fish"))
            {
                ltds.Remove(ltds[i]);
            }
        }

        StartCoroutine(EnemySetter());
        

        if (GameManager.instance.faseumBossFire == true)
        {
            LoadFromBossCamp();
            print("Wipe");
        }
    }

    // Update is called once per frame
    public void KilledEnemy(GameObject enemy)
    {
        if (firstMob.Contains(enemy))
        {
            firstMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();
        }
        else if (secondMob.Contains(enemy))
        {
            secondMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();

        }
        else if (thirdMob.Contains(enemy))
        {
            thirdMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();

        }
        else if (fourthMob.Contains(enemy))
        {
            fourthMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();
        }
        else if (fifthMob.Contains(enemy))
        {
            fifthMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();
        }
        else if (sixthMob.Contains(enemy))
        {
            sixthMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();
        }
        if (seventhMob.Contains(enemy))
        {
            seventhMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();
        }
        if (eigthMob.Contains(enemy))
        {
            eigthMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();
        }
        if (bossMob.Contains(enemy))
        {
            bossMob.Remove(enemy);
            FaseTresTriggerController.Instance.ContadorDeInimigosMortos();
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


    public void IsBossMobCleared()
    {
        if (bossMob.Count <= 0)
        {
            GateChecker.Instance.MobsDied();
        }
    }

    IEnumerator LoadFromBossCamp()
    {
        yield return new WaitForSeconds(0.05f);
        firstMob.ForEach(x => Destroy(x));
        secondMob.ForEach(x => Destroy(x));
        thirdMob.ForEach(x => Destroy(x));
        fourthMob.ForEach(x => Destroy(x));
        fifthMob.ForEach(x => Destroy(x));
        sixthMob.ForEach(x => Destroy(x));
        seventhMob.ForEach(x => Destroy(x));
        eigthMob.ForEach(x => Destroy(x));
        firstMob.Clear();
        secondMob.Clear();
        thirdMob.Clear();
        fourthMob.Clear();
        fifthMob.Clear();
        sixthMob.Clear();
        seventhMob.Clear();
        eigthMob.Clear();

        FaseTresTriggerController.Instance.BossFireSet();


        for (int i = 0; i < mobTriggers.Length; i++)
        {
            mobTriggers[i].enabled = false;
        }
        
    }

    IEnumerator EnemySetter()
    {
        yield return new WaitForSeconds(0.03f);
        clearedTres = GameManager.instance.GetHasCleared(4);
        clearedTresHalf = GameManager.instance.GetHasCleared(5);
        randomTL = GameManager.instance.randomT;
        if (!clearedTres && !clearedTresHalf)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            ltds.ForEach(x => x.intensity = 5f);
        }
        else if (clearedTres && !clearedTresHalf)
        {
            p1.SetActive(false);
            p2.SetActive(true);
            ltds.ForEach(x => x.intensity = 2f);
        }
        else if (clearedTres && clearedTresHalf)
        {
            if (randomTL == 1)
            {
                p1.SetActive(true);
                p2.SetActive(false);
                ltds.ForEach(x => x.intensity = 5f);
            }
            else if (randomTL == 2)
            {
                p1.SetActive(false);
                p2.SetActive(true);
                ltds.ForEach(x => x.intensity = 2f);

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
}

