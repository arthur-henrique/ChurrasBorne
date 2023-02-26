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

    public AudioSource audioSource;
    public AudioClip ui_move;
    public AudioClip ui_confirm;

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

    public int sel_pos = 0;
    public int sel_pos_real = 0;
    public int sel_pos_highlight = 0;
    public int sel_pos_highlight_dest = -225;

    private bool movementLock = true;

    public List<int> itemStorage = new List<int>();

    public Sprite[] itemImages; // sprites adicionados pelo inspetor, verificar prefab associado Inventory_Canvas

    #region Item Names
    private string item_void_name;
    private string item_void_desc;

    private string item_alho_name;
    private string item_alho_desc;

    private string item_astrolabio_name;
    private string item_astrolabio_desc;

    private string item_gelo_name;
    private string item_gelo_desc;

    private string item_lenha_name;
    private string item_lenha_desc;

    private string item_sal_name;
    private string item_sal_desc;

    private string item_corotefull_name;
    private string item_corotefull_desc;

    private string item_coroteempty_name;
    private string item_coroteempty_desc;

    private string item_chavedungeonlua_name;
    private string item_chavedungeonlua_desc;
    #endregion

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
        audioSource = GetComponent<AudioSource>();
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

        inv_label = DialogSystem.getChildGameObject(gameObject, "Inventory_Label");

        itemStorage.Add(1);
        itemStorage.Add(2);
        //itemStorage.Add(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("LANGAUGE") == 0) // english
        {
            inv_label.GetComponent<TextMeshProUGUI>().text = "Inventory";

            item_void_name = "Nothing";
            item_void_desc = "You have amazing nothings.";

            item_alho_name = "Garlic Bread";
            item_alho_desc = "The perfect snack to wait the meat - be it in a barbecue, or be it in a battle field.";

            item_astrolabio_name = "Ancient Astrolabe";
            item_astrolabio_desc = "This artifact seems to be powerless already. The tales tell that throughout the ages, such item were used to control the stars...";

            item_gelo_name = "Ice";
            item_gelo_desc = "With a spell touch, the drink shall cool forever. We got the ice, now we need the spell.";

            item_lenha_name = "Firewood";
            item_lenha_desc = "Primitive, but efficient. Perfect to light fireplaces, wood stoves, and Smith's forge furnace.";

            item_sal_name = "Coarse Salt";
            item_sal_desc = "Old world treasure. The gold of the apocalypse. The final touch. Increases the Meat's efficiency.";

            item_corotefull_name = "Pitú";
            item_corotefull_desc = "The secret recipe of the madam. Don't, just drink it. Gains movement and attack speed.";

            item_coroteempty_name = "Pitú (Empty)";
            item_coroteempty_desc = "The secret recipe of the madam. Don't, just drink it. Gains movement and attack speed.";

            item_chavedungeonlua_name = "Moon Key";
            item_chavedungeonlua_desc = "Only a mad mage would recreate the surface underneath, in his lair, and would lock it by 3 Keys... Opens one of 3 locks.";
        }

        if (PlayerPrefs.GetInt("LANGAUGE") == 1) // portuguese
        {
            inv_label.GetComponent<TextMeshProUGUI>().text = "Inventário";

            item_void_name = "Nada";
            item_void_desc = "Você possui incríveis nada.";

            item_alho_name = "Pão de Alho";
            item_alho_desc = "O petisco perfeito para esperar a carne - sendo num churrasco, ou no Campo de Batalha (provavelmente é consumível).";

            item_astrolabio_name = "Astrolábio";
            item_astrolabio_desc = "Um instrumento já sem poder algum. Reza a lenda que à tempos, tal item era usado para controlar os Astros...";

            item_gelo_name = "Gelo";
            item_gelo_desc = "Com um toque de mágica, a bebida será gelada para sempre. Já temos o gelo, falta a mágica.";

            item_lenha_name = "Lenha";
            item_lenha_desc = "Primitivo, mas eficiente. Perfeito para acender fogueiras, fogões e a fornalha do Ferreiro Ferreira.";

            item_sal_name = "Sal";
            item_sal_desc = "O tesouro do velho Mundo. O ouro do apocalipse. O toque final. Aumenta a eficiência das Carnes.";

            item_corotefull_name = "Pitú";
            item_corotefull_desc = "A receita secreta da madame. Não pergunte, apenas aproveite. Ganha bônus de movimento e velocidade de ataque.";

            item_coroteempty_name = "Pitú (Vazio)";
            item_coroteempty_desc = "A receita secreta da madame. Não pergunte, apenas aproveite. Ganha bônus de movimento e velocidade de ataque.";

            item_chavedungeonlua_name = "Chave Lua";
            item_chavedungeonlua_desc = "Apenas um Mago louco recriaria a superfície em sua Masmorra, e a trancaria com 3 chaves... Abre uma das 3 trancas.";
        }

        if (PlayerPrefs.GetInt("LANGAUGE") == 2) // spanish
        {
            inv_label.GetComponent<TextMeshProUGUI>().text = "Inventario";

            item_void_name = "Nada";
            item_void_desc = "Posees una nada increíble.";

            item_alho_name = "Pan de Ajo";
            item_alho_desc = "El aperitivo perfecto para esperar a la carne, ya sea en una barbacoa o en el campo de batalla (probablemente sea consumible).";

            item_astrolabio_name = "Astrolabio";
            item_astrolabio_desc = "Un instrumento ya sin ningún poder. Cuenta la leyenda que antaño se utilizaba un objeto así para controlar las estrellas...";

            item_gelo_name = "Hielo";
            item_gelo_desc = "Con un toque de magia, la bebida estará helada para siempre. Ya tenemos el hielo, falta la magia.";

            item_lenha_name = "Leña";
            item_lenha_desc = "Primitiva pero eficaz. Perfecta para encender hogueras, cocinas y el horno de la Herrería Ferreira.";

            item_sal_name = "Sal";
            item_sal_desc = "El tesoro del viejo mundo. El oro del Apocalipsis. El toque final. Aumenta la eficacia de la carne.";

            item_corotefull_name = "Pitú";
            item_corotefull_desc = "La receta secreta de la señora. No preguntes, disfruta. Obtiene bonificación de movimiento y velocidad de ataque.";

            item_coroteempty_name = "Pitú (Vacío)";
            item_coroteempty_desc = "La receta secreta de la señora. No preguntes, disfruta. Obtiene bonificación de movimiento y velocidad de ataque.";

            item_chavedungeonlua_name = "Llave Lunar";
            item_chavedungeonlua_desc = "Sólo un Mago loco recrearía la superficie en su Mazmorra, y la cerraría con 3 llaves... Abre una de las 3 cerraduras.";
        }

        if (PauseManager.isPaused == false)
        {
            if (movementLock == true)
            {
                if (pc.Movimento.Inventario.WasPressedThisFrame() && PlayerMovement.pc.Movimento.enabled)
                {
                    audioSource.PlayOneShot(ui_confirm, audioSource.volume);
                    movementLock = false;
                    PlayerMovement.DisableControl();
                }
                GetComponent<CanvasGroup>().alpha = Mathf.Lerp(GetComponent<CanvasGroup>().alpha, 0, 16f * Time.deltaTime);
                
            }
            else
            {
                if (pc.Movimento.Inventario.WasPressedThisFrame())
                {
                    audioSource.PlayOneShot(ui_confirm, audioSource.volume);
                    movementLock = true;
                    PlayerMovement.EnableControl();
                }
                HealthBar_Manager.newItem = false;
                GetComponent<CanvasGroup>().alpha = Mathf.Lerp(GetComponent<CanvasGroup>().alpha, 1, 16f * Time.deltaTime);
            }

            if (movementLock == false)
            {
                if (pc.Movimento.NorteSul.WasPressedThisFrame())
                {
                    if (itemStorage.Count > 0)
                    {
                        audioSource.PlayOneShot(ui_move, audioSource.volume);
                        sel_pos_highlight -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                        sel_pos_real -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                    }

                    if (sel_pos_highlight > 2)
                    {
                        sel_pos_highlight = 2;
                        if (sel_pos == sel_pos_highlight - 2)
                        {
                            sel_pos = sel_pos_highlight + 1;
                        }
                        else
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
                
                if (pc.Movimento.Rolar.WasPressedThisFrame())
                {
                    itemStorage.Add(1);
                }

                if (pc.Movimento.Curar.WasPressedThisFrame())
                {
                    itemStorage.Remove(1);
                }
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
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_void_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_void_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[0];
                        break;

                    case 1:
                        // Pão de Alho
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_alho_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_alho_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[1];
                        break;

                    case 2:
                        // Astrolábio
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_astrolabio_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_astrolabio_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[2];
                        break;

                    case 3:
                        // Gelo
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_gelo_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_gelo_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[3];
                        break;

                    case 4:
                        // Lenha
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_lenha_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_lenha_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[4];
                        break;

                    case 5:
                        // sal
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_sal_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_sal_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[5];
                        break;

                    case 6:
                        // corote full
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_corotefull_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_corotefull_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[6];
                        break;

                    case 7:
                        // corote empty
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_coroteempty_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_coroteempty_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[7];
                        break;

                    case 8:
                        // chave dungeon lua
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_chavedungeonlua_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_chavedungeonlua_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[8];
                        break;

                    default:
                        // empty / vazio / sem item
                        inv_item_name.GetComponent<TextMeshProUGUI>().text = item_void_name;
                        inv_item_desc.GetComponent<TextMeshProUGUI>().text = item_void_desc;
                        inv_board_item.GetComponent<Image>().sprite = itemImages[0];
                        break;
                }
            }


            if (sel_pos <= 2)
            {
                if (itemStorage.Count >= 1)
                {
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

            }
            else
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
        }
        

        //Debug.Log("itemStorage.Count: " + itemStorage.Count);
        //Debug.Log("sel_pos: " + sel_pos);
        //Debug.Log("sel_pos_highlight: " + sel_pos_highlight);
        //Debug.Log("sel_pos_real: " + sel_pos_real);
    }
}
