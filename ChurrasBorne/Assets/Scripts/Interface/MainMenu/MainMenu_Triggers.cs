using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class MainMenu_Triggers : EventTrigger, IPointerClickHandler
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
        if (MainMenu_Manager.menu_selection_confirm == false)
        {
            switch (gameObject.name)
            {
                case "MENU_Start":

                    MainMenu_Manager.menu_position = 0;
                    break;

                case "MENU_Options":

                    MainMenu_Manager.menu_position = 1;
                    break;

                case "MENU_Quit":

                    MainMenu_Manager.menu_position = 2;
                    break;

            }
        }
        
    }

    public void EnterItem(BaseEventData data)
    {
        if (Time.fixedTime > 3)
        {
            MainMenu_Manager.menu_selection_confirm = true;
            Debug.Log("start");
        }
            
    }
}
