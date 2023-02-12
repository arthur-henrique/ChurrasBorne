using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAutomation : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite passado, eclipse;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (!ManagerOfScenes.instance.isEclipse)
        {
            sr.sprite = passado;
        }
        else if(ManagerOfScenes.instance.isEclipse)
        {
            sr.sprite = eclipse;
        }
    }

    
}
