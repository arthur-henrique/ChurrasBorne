using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Language_Triggers : EventTrigger, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip ui_move;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem(BaseEventData data)
    {
        if ( Language_Manager.lockSelec == false)
        {
            audioSource.Play();
        }
        switch (gameObject.name)
        {
            case "English_Image":

                Language_Manager.selec = 0;
                break;

            case "Portuguese_Image":

                Language_Manager.selec = 1;
                break;

            case "Spanish_Image":

                Language_Manager.selec = 2;
                break;

        }

    }

    public void EnterItem(BaseEventData data)
    {
    }
}
