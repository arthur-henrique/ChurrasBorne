using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    PlayerController pc;
    public static Inventory_Manager instance;

    private GameObject inv_board;
    private GameObject inv_board_highlight;
    private GameObject inv_board_item;

    private GameObject inv_space_1;
    private GameObject inv_space_2;
    private GameObject inv_space_3;

    private GameObject inv_item_1;
    private GameObject inv_item_2;
    private GameObject inv_item_3;

    private GameObject inv_arrow_1;
    private GameObject inv_arrow_2;

    private GameObject inv_label;
    private GameObject inv_item_name;
    private GameObject inv_item_desc;

    private Vector3 vel_board_highlight = Vector3.zero;

    private int sel_pos = 0;
    private int sel_pos_real = 0;
    private int sel_pos_highlight = 0;
    private int sel_pos_highlight_dest = -225;

    private bool movementLock = true;

    public List<int> itemStorage = new List<int>();

    public Sprite[] itemImages; // sprites adicionados pelo inspetor, verificar prefab associado Inventory_Canvas

    private void Awake()
    {
        pc = new PlayerController();
    }
    private void OnEnable()
    {
        pc.Enable();
    }
    private void OnDisable()
    {
        pc.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        
        inv_board_highlight = DialogSystem.getChildGameObject(gameObject, "Select_Highlight");
        inv_board_item = DialogSystem.getChildGameObject(gameObject, "Board_Item");

        inv_item_1 = DialogSystem.getChildGameObject(gameObject, "Select_Item_1");
        inv_item_2 = DialogSystem.getChildGameObject(gameObject, "Select_Item_2");
        inv_item_3 = DialogSystem.getChildGameObject(gameObject, "Select_Item_3");

        inv_item_name = DialogSystem.getChildGameObject(gameObject, "Item_Name");
        inv_item_desc = DialogSystem.getChildGameObject(gameObject, "Item_Description");

        inv_item_1.GetComponent<Image>().sprite = itemImages[0];
        inv_item_2.GetComponent<Image>().sprite = itemImages[0];
        inv_item_3.GetComponent<Image>().sprite = itemImages[0];

        //itemStorage.Add(1);
        //itemStorage.Add(2);
        //itemStorage.Add(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (movementLock == true)
        {
            if (pc.Movimento.Inventario.WasPressedThisFrame())
            {
                movementLock = false;
            }
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(GetComponent<CanvasGroup>().alpha, 0, 16f * Time.deltaTime);
            PlayerMovement.EnableControl();
            
        } else
        {
            if (pc.Movimento.Inventario.WasPressedThisFrame())
            {
                movementLock = true;
            }
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(GetComponent<CanvasGroup>().alpha, 1, 16f * Time.deltaTime);
            PlayerMovement.DisableControl();
        }

        if (movementLock == false)
        {
            if (pc.Movimento.NorteSul.WasPressedThisFrame())
            {
                if (itemStorage.Count > 0)
                {
                    sel_pos_highlight -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                    sel_pos_real -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                }

                if (sel_pos_highlight > 2)
                {
                    sel_pos_highlight = 2;
                    if (sel_pos == sel_pos_highlight - 2)
                    {
                        sel_pos = sel_pos_highlight + 1;
                    } else
                    {
                        sel_pos += 1;
                    }
                }
                if (sel_pos_highlight < 0)
                {
                    sel_pos_highlight = 0;
                    if (sel_pos == sel_pos_highlight + 2)
                    {
                        sel_pos = sel_pos_highlight - 1;
                    }
                    else
                    {
                        sel_pos -= 1;
                    }

                }

                
                
            }

            if (itemStorage.Count > 0)
            {
                if (sel_pos > itemStorage.Count - 1) { sel_pos = itemStorage.Count - 1; }
                if (sel_pos < 0) { sel_pos = 0; }

                if (sel_pos_real > itemStorage.Count - 1) { sel_pos_real = itemStorage.Count - 1; }
                if (sel_pos_real < 0) { sel_pos_real = 0; }
            }

            if (itemStorage.Count <= 3)
            {
                if (sel_pos_highlight > itemStorage.Count - 1) { sel_pos_highlight = itemStorage.Count - 1; }
                if (sel_pos_highlight < 0) { sel_pos_highlight = 0; }
            }
            /*
            if (pc.Movimento.Rolar.WasPressedThisFrame())
            {
                itemStorage.Add(1);
            }

            if (pc.Movimento.Curar.WasPressedThisFrame())
            {
                itemStorage.Remove(1);
            }*/
        }
        

        switch (sel_pos_highlight)
        {
            case 0:
                sel_pos_highlight_dest = 315;
                break;

            case 1:
                sel_pos_highlight_dest = 187;
                break;

            case 2:
                sel_pos_highlight_dest = 59;
                break;
        }

        inv_board_highlight.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(inv_board_highlight.GetComponent<RectTransform>().anchoredPosition, new Vector3(420, sel_pos_highlight_dest, 0), ref vel_board_highlight, 5 * Time.deltaTime);
    
        if (sel_pos_real >= 0 && sel_pos_real < itemStorage.Count)
        {
            switch (itemStorage[sel_pos_real])
            {
                case 0:
                    // empty / vazio / sem item
                    inv_item_name.GetComponent<TextMeshProUGUI>().text = "Nada";
                    inv_item_desc.GetComponent<TextMeshProUGUI>().text = "Você possui incríveis nada.";
                    inv_board_item.GetComponent<Image>().sprite = itemImages[0];
                    break;

                case 1:
                    // chave simples
                    inv_item_name.GetComponent<TextMeshProUGUI>().text = "Chave Simples";
                    inv_item_desc.GetComponent<TextMeshProUGUI>().text = "Uma chave simples de metal.";
                    inv_board_item.GetComponent<Image>().sprite = itemImages[1];
                    break;

                case 2:
                    // baiacu minecraft
                    inv_item_name.GetComponent<TextMeshProUGUI>().text = "Baiacu Minecraft";
                    inv_item_desc.GetComponent<TextMeshProUGUI>().text = "O baiacu pode ser usado para tirar os gatos de baús, camas, etc, reprodução de gatos, fazendo com que os filhotes de gatos cresçam mais rápido em 10% do tempo restante. Na Edição Java, eles também podem ser usados para ganhar a confiança de uma jaguatirica..";
                    inv_board_item.GetComponent<Image>().sprite = itemImages[2];
                    break;

                default:
                    // empty / vazio / sem item
                    inv_item_name.GetComponent<TextMeshProUGUI>().text = "Nada";
                    inv_item_desc.GetComponent<TextMeshProUGUI>().text = "Você possui incríveis nada.";
                    inv_board_item.GetComponent<Image>().sprite = itemImages[0];
                    break;
            }
        }
        

        if (sel_pos <= 2)
        {
            if (itemStorage.Count >= 1) {
                inv_item_1.GetComponent<Image>().sprite = itemImages[itemStorage[0]];
            } 
            else
            {
                inv_item_1.GetComponent<Image>().sprite = itemImages[0];
            }

            if (itemStorage.Count >= 2)
            {
                inv_item_2.GetComponent<Image>().sprite = itemImages[itemStorage[1]];
            }
            else
            {
                inv_item_2.GetComponent<Image>().sprite = itemImages[0];
            }

            if (itemStorage.Count >= 3)
            {
                inv_item_3.GetComponent<Image>().sprite = itemImages[itemStorage[2]];
            }
            else
            {
                inv_item_3.GetComponent<Image>().sprite = itemImages[0];
            }

        } else
        {
            if (sel_pos >= 0 && sel_pos < itemStorage.Count)
            {
                inv_item_1.GetComponent<Image>().sprite = itemImages[itemStorage[0 + sel_pos - 2]];
            }
            else
            {
                inv_item_1.GetComponent<Image>().sprite = itemImages[0];
            }

            if (sel_pos >= 0 && sel_pos < itemStorage.Count)
            {
                inv_item_2.GetComponent<Image>().sprite = itemImages[itemStorage[1 + sel_pos - 2]];
            }
            else
            {
                inv_item_2.GetComponent<Image>().sprite = itemImages[0];
            }

            if (sel_pos >= 0 && sel_pos < itemStorage.Count)
            {
                inv_item_3.GetComponent<Image>().sprite = itemImages[itemStorage[2 + sel_pos - 2]];
            }
            else
            {
                inv_item_3.GetComponent<Image>().sprite = itemImages[0];
            }
        }

        //Debug.Log("itemStorage.Count: " + itemStorage.Count);
        //Debug.Log("sel_pos: " + sel_pos);
        //Debug.Log("sel_pos_highlight: " + sel_pos_highlight);
        //Debug.Log("sel_pos_real: " + sel_pos_real);
    }
}
