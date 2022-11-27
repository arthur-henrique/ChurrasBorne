using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDestroySelf : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 5f);
    }
}
