using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause_Triggers : EventTrigger, IPointerClickHandler
{
    public void SelectItem(BaseEventData data)
    {
        if (PauseManager.selection_confirm == false)
        {
            if (PauseManager.isPaused == true)
            {
                PauseManager.instance.audioSource.PlayOneShot(PauseManager.instance.ui_move, PauseManager.instance.audioSource.volume);
            }
            
            if (PauseManager.submenu == false)
            {
                switch (gameObject.name)
                {
                    case "PAUSE_Continuar":

                        PauseManager.selection_position = 0;
                        break;

                    case "PAUSE_Hub":

                        PauseManager.selection_position = 1;
                        break;

                    case "PAUSE_Opcoes":

                        PauseManager.selection_position = 2;
                        break;

                    case "PAUSE_Titulo":

                        PauseManager.selection_position = 3;
                        break;

                }
            } else
            {
                switch (gameObject.name)
                {
                    case "PAUSE_Resolucao":

                        PauseManager.selection_position = 0;
                        break;

                    case "PAUSE_TelaCheia":

                        PauseManager.selection_position = 1;
                        break;

                    case "PAUSE_Master":

                        PauseManager.selection_position = 2;
                        break;

                    case "PAUSE_MasterVolume":

                        PauseManager.selection_position = 2;
                        break;

                    case "PAUSE_MasterSlider":

                        PauseManager.selection_position = 2;
                        break;

                    case "PAUSE_BGM":

                        PauseManager.selection_position = 3;
                        break;

                    case "PAUSE_BGMVolume":

                        PauseManager.selection_position = 3;
                        break;

                    case "PAUSE_BGMSlider":

                        PauseManager.selection_position = 3;
                        break;

                    case "PAUSE_SFX":

                        PauseManager.selection_position = 4;
                        break;

                    case "PAUSE_SFXVolume":

                        PauseManager.selection_position = 4;
                        break;

                    case "PAUSE_SFXSlider":

                        PauseManager.selection_position = 4;
                        break;

                    case "PAUSE_Linguagem":

                        PauseManager.selection_position = 5;
                        break;

                    case "PAUSE_Aplicar":

                        PauseManager.selection_position = 6;
                        break;


                }
            }
            
        }

    }

    public void EnterItem(BaseEventData data)
    {
        if (Time.fixedTime > 1)
        {
            if (PauseManager.isPaused)
            {
                //PauseManager.selection_confirm = true;
                //PauseManager.instance.audioSource.PlayOneShot(PauseManager.instance.ui_confirm, PauseManager.instance.audioSource.volume);
                //Debug.Log("start");
            }
            
        }

    }
}
