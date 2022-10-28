using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngyDetector : MonoBehaviour
{
    public GameObject enemy;
    
    public GameObject angyDetector;

    public float angyDistance;

    public Transform player;

    private bool canBeAngy = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > angyDistance && canBeAngy == true)
        {
            enemy.GetComponent<EnemyAI>().angy = true;

            GameObject.Destroy(angyDetector);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeAngy = true;
        }
    }
}
