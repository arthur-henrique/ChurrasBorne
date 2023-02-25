using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory_Click : EventTrigger, IPointerClickHandler
{
    public void SelectItem(BaseEventData data)
    {
        if (PauseManager.isPaused == false)
        {
            if (gameObject.name == "Arrow_1")
            {
                if (Inventory_Manager.instance.itemStorage.Count > 0)
                {
                    Inventory_Manager.instance.audioSource.PlayOneShot(Inventory_Manager.instance.ui_move, Inventory_Manager.instance.audioSource.volume);
                    Inventory_Manager.instance.sel_pos_highlight -= 1;
                    Inventory_Manager.instance.sel_pos_real -= 1;
                }

                if (Inventory_Manager.instance.sel_pos_highlight > 2)
                {
                    Inventory_Manager.instance.sel_pos_highlight = 2;
                    if (Inventory_Manager.instance.sel_pos == Inventory_Manager.instance.sel_pos_highlight - 2)
                    {
                        Inventory_Manager.instance.sel_pos = Inventory_Manager.instance.sel_pos_highlight + 1;
                    }
                    else
                    {
                        Inventory_Manager.instance.sel_pos += 1;
                    }
                }
                if (Inventory_Manager.instance.sel_pos_highlight < 0)
                {
                    Inventory_Manager.instance.sel_pos_highlight = 0;
                    if (Inventory_Manager.instance.sel_pos == Inventory_Manager.instance.sel_pos_highlight + 2)
                    {
                        Inventory_Manager.instance.sel_pos = Inventory_Manager.instance.sel_pos_highlight - 1;
                    }
                    else
                    {
                        Inventory_Manager.instance.sel_pos -= 1;
                    }

                }

            }
            if (gameObject.name == "Arrow_2")
            {
                if (Inventory_Manager.instance.itemStorage.Count > 0)
                {
                    Inventory_Manager.instance.audioSource.PlayOneShot(Inventory_Manager.instance.ui_move, Inventory_Manager.instance.audioSource.volume);
                    Inventory_Manager.instance.sel_pos_highlight += 1;
                    Inventory_Manager.instance.sel_pos_real += 1;
                }

                if (Inventory_Manager.instance.sel_pos_highlight > 2)
                {
                    Inventory_Manager.instance.sel_pos_highlight = 2;
                    if (Inventory_Manager.instance.sel_pos == Inventory_Manager.instance.sel_pos_highlight - 2)
                    {
                        Inventory_Manager.instance.sel_pos = Inventory_Manager.instance.sel_pos_highlight + 1;
                    }
                    else
                    {
                        Inventory_Manager.instance.sel_pos += 1;
                    }
                }
                if (Inventory_Manager.instance.sel_pos_highlight < 0)
                {
                    Inventory_Manager.instance.sel_pos_highlight = 0;
                    if (Inventory_Manager.instance.sel_pos == Inventory_Manager.instance.sel_pos_highlight + 2)
                    {
                        Inventory_Manager.instance.sel_pos = Inventory_Manager.instance.sel_pos_highlight - 1;
                    }
                    else
                    {
                        Inventory_Manager.instance.sel_pos -= 1;
                    }

                }
            }
            if (gameObject.name == "Select_Item_1")
            {
                if (Inventory_Manager.instance.itemStorage.Count >= 0)
                {
                    Inventory_Manager.instance.audioSource.PlayOneShot(Inventory_Manager.instance.ui_move, Inventory_Manager.instance.audioSource.volume);
                    var sel_pos_old = Inventory_Manager.instance.sel_pos_highlight;
                    Inventory_Manager.instance.sel_pos_highlight = 0;
                    Inventory_Manager.instance.sel_pos_real += Inventory_Manager.instance.sel_pos_highlight - sel_pos_old;
                }
            }
            if (gameObject.name == "Select_Item_2")
            {
                if (Inventory_Manager.instance.itemStorage.Count >= 1)
                {
                    Inventory_Manager.instance.audioSource.PlayOneShot(Inventory_Manager.instance.ui_move, Inventory_Manager.instance.audioSource.volume);
                    var sel_pos_old = Inventory_Manager.instance.sel_pos_highlight;
                    Inventory_Manager.instance.sel_pos_highlight = 1;
                    Inventory_Manager.instance.sel_pos_real += Inventory_Manager.instance.sel_pos_highlight - sel_pos_old;
                }
            }
            if (gameObject.name == "Select_Item_3")
            {
                if (Inventory_Manager.instance.itemStorage.Count >= 2)
                {
                    Inventory_Manager.instance.audioSource.PlayOneShot(Inventory_Manager.instance.ui_move, Inventory_Manager.instance.audioSource.volume);
                    var sel_pos_old = Inventory_Manager.instance.sel_pos_highlight;
                    Inventory_Manager.instance.sel_pos_highlight = 2;
                    Inventory_Manager.instance.sel_pos_real += Inventory_Manager.instance.sel_pos_highlight - sel_pos_old;
                }
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
