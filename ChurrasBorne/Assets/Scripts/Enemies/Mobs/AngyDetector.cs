using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngyDetector : MonoBehaviour
{
    public GameObject angyDetector;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            angyDetector.SetActive(false);  
        }
    }
}
