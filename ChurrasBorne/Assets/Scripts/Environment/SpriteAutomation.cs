using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAutomation : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite passado, eclipse;
    public GameObject sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sceneManager = GameObject.FindGameObjectWithTag("FASETRES");
        StartCoroutine(CheckEclipse());        
    }

    IEnumerator CheckEclipse()
    {
        yield return new WaitForSeconds(.5f);
        if (sceneManager.GetComponent<ManagerOfScenes>().isEclipse == false)
        {
            sr.sprite = passado;
            print("iSPassado");
        }
        else if (sceneManager.GetComponent<ManagerOfScenes>().isEclipse == true)
        {
            sr.sprite = eclipse;
            print("iSEclipse");

        }
    }

    
}
