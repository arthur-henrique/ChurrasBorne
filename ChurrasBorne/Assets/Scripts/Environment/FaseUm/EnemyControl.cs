using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl Instance;
    public List<GameObject> firstMob = new List<GameObject>();
    public List<GameObject> secondMob = new List<GameObject>();
    public List<GameObject> thirdMob = new List<GameObject>();
    public GameObject tronco;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        firstMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_SALAUMMOB"));
        secondMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_SALAUMMOBDOIS"));
        thirdMob.AddRange(GameObject.FindGameObjectsWithTag("FASEUM_SALADOISMOB"));
        for (int i = 0; i < secondMob.Count; i++)
        {
            secondMob[i].SetActive(false);
        }
        for (int i = 0; i < thirdMob.Count; i++)
        {
            thirdMob[i].SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
