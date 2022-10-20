using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause_Triggers : EventTrigger, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem(BaseEventData data)
    {
        if (PauseManager.selection_confirm == false)
        {
            switch (gameObject.name)
            {
                case "PAUSE_Continuar":

                    PauseManager.selection_position = 0;
                    break;

                case "PAUSE_Hub":

                    PauseManager.selection_position = 1;
                    break;

                case "PAUSE_Titulo":

                    PauseManager.selection_position = 2;
                    break;

            }
        }

    }

    public void EnterItem(BaseEventData data)
    {
        if (Time.fixedTime > 1)
        {
            if (PauseManager.isPaused)
            {
                PauseManager.selection_confirm = true;
                Debug.Log("start");
            }
            
        }

    }
}
