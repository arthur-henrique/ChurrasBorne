using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterSpikeDestroySelf : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }
}
