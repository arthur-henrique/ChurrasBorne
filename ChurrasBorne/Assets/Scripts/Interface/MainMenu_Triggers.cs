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
            MainMenu_Manager.instance.audioSource.PlayOneShot(MainMenu_Manager.instance.ui_move, MainMenu_Manager.instance.audioSource.volume);
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

                // ----------------------- Options

                case "MENU_Resolution":

                    MainMenu_Manager.menu_position = 0;
                    break;

                case "MENU_Fullscreen":

                    MainMenu_Manager.menu_position = 1;
                    break;

                case "MENU_Master":

                    MainMenu_Manager.menu_position = 2;
                    break;

                case "MENU_BGM":

                    MainMenu_Manager.menu_position = 3;
                    break;

                case "MENU_SFX":

                    MainMenu_Manager.menu_position = 4;
                    break;

                case "MENU_Apply":

                    MainMenu_Manager.menu_position = 5;
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
            MainMenu_Manager.instance.audioSource.PlayOneShot(MainMenu_Manager.instance.ui_confirm, MainMenu_Manager.instance.audioSource.volume);
        }
            
    }
}
