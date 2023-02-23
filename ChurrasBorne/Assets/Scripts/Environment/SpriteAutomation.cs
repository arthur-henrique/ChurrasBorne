using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAutomation : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite passado, eclipse;
    public ManagerOfScenes sceneManager;

    // Port�es
    public bool isFaseTr�sPort�o = false;
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
            if(!isFaseTr�sPort�o)
                sr.sprite = passado;
        }
        else if (sceneManager.GetComponent<ManagerOfScenes>().isEclipse == true)
        {
            if(!isFaseTr�sPort�o)
                sr.sprite = eclipse;
            else if(isFaseTr�sPort�o)
            {
                sr.color = endColor;
                print("ChangedColor");
            }
        }
    }

    
}
