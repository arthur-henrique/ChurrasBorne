using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAutomation : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite passado, eclipse;
    public ManagerOfScenes sceneManager;

    // Portões
    public bool isFaseTrêsPortão = false;
    public Color startColor, endColor;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sceneManager = FindObjectOfType<ManagerOfScenes>();
        StartCoroutine(CheckEclipse());        
    }

    IEnumerator CheckEclipse()
    {
        yield return new WaitForSeconds(.5f);
        if (sceneManager.GetComponent<ManagerOfScenes>().isEclipse == false)
        {
            if(!isFaseTrêsPortão)
                sr.sprite = passado;
        }
        else if (sceneManager.GetComponent<ManagerOfScenes>().isEclipse == true)
        {
            if(!isFaseTrêsPortão)
                sr.sprite = eclipse;
            else if(isFaseTrêsPortão)
            {
                sr.color = endColor;
                print("ChangedColor");
            }
        }
    }

    
}
