using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOver_Triggers : EventTrigger, IPointerClickHandler
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
                case "GOVER_Retry":

                    GameOver_Manager.gover_selection_position = 0;
                    break;

                case "GOVER_Hub":

                    GameOver_Manager.gover_selection_position = 1;
                    break;

                case "GOVER_Title":

                    GameOver_Manager.gover_selection_position = 2;
                    break;

            }
        }

    }

    public void EnterItem(BaseEventData data)
    {
        if (Time.fixedTime > 1)
        {
            GameOver_Manager.gover_selection_confirm = true;
        }
    }
}
