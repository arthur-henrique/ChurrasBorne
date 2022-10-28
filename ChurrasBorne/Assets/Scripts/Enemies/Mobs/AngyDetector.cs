using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngyDetector : MonoBehaviour
{
    public GameObject enemy;
    
    public GameObject angyDetector;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (angyDetector.activeSelf == false)
        {
            enemy.GetComponent<EnemyAI>().angy = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyAI>().angy = true;

            angyDetector.SetActive(false);
        }
    }
}
