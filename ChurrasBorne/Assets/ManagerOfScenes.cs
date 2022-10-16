using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfScenes : MonoBehaviour
{
    private void Awake()
    {
        if (gameObject.tag == "Tutorial")
        {
            GameManager.instance.SetHeals(-1f);
        }
        else if (gameObject.tag == "Untagged")
        {
            GameManager.instance.SetHeals(3f);
        }
    }
}
