using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlTutorial : MonoBehaviour
{
    public static EnemyControlTutorial Instance;
    private readonly List<GameObject> firstMob = new List<GameObject>();
    private readonly List<GameObject> secondMob = new List<GameObject>();
    public AudioSource audioSource;
    public AudioClip gate_open;
    public GameObject tutboxchurras;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        firstMob.AddRange(GameObject.FindGameObjectsWithTag("TUTORIAL_SALAUMMOB"));
        secondMob.AddRange(GameObject.FindGameObjectsWithTag("TUTORIAL_SALAUMMOBDOIS"));
        for (int i = 0; i < secondMob.Count; i++)
        {
            secondMob[i].SetActive(false);
        }
    }

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
    }
    public void SpawnSecondMob()
    {
        for (int i = 0; i < secondMob.Count; i++)
        {
            secondMob[i].SetActive(true);
        }
    }

    public void IsFirstMobCleared()
    {
        if (firstMob.Count <= 0)
        {
            // Call the event that spawns the second wave
            SpawnSecondMob();
        }
    }

    public void IsSecondMobCleared()
    {
        if (secondMob.Count <= 0)
        {
            audioSource.PlayOneShot(gate_open, audioSource.volume);
            TutorialTriggerController.Instance.FirstGateTrigger();
            Instantiate(tutboxchurras);
        }
    }
}
