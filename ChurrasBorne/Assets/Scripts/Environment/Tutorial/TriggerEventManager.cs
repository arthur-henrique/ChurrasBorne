using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventManager : MonoBehaviour
{
    public static TriggerEventManager instance;
    [SerializeField] private UnityEvent spawnMobs;
    [SerializeField] private UnityEvent openMobGate;
    // Cuida do primeiro evento do Tutorial: Abrir a porta após os mobs:
    public GameObject rangedMobOne, rangedMobTwo;
    private bool rangedMobOneIsDead = false, rangedMobTwoIsDead = false, mobGateIsClosed = true;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        rangedMobOneIsDead = rangedMobOne.GetComponent<EnemyAI>().isDead;
        rangedMobTwoIsDead = rangedMobTwo.GetComponent<EnemyAI>().isDead;
        if (rangedMobOneIsDead && rangedMobTwoIsDead && mobGateIsClosed)
        {
            mobGateIsClosed = false;
            OpenMobGate();
        }
    }

    private void LateUpdate()
    {
    }

    public void SpawnMobs()
    {
        spawnMobs.Invoke();
    }

    public void OpenMobGate()
    {
        print("Abrete");
        openMobGate.Invoke();
    }

}
