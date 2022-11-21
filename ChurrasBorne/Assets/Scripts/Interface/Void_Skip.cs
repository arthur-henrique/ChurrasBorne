using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Void_Skip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Tutorial");
        if (GameManager.instance)
        {
            GameManager.instance.NextLevelSetter(Vector2.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
