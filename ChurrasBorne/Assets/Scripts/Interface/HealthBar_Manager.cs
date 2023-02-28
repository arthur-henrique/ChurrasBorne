using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar_Manager : MonoBehaviour
{
    private enum State
    {
        HPM1,
        HPM2,
        HPM3,
        HPM4,
        HPM5,
        HPM6,
        HPM7,
        HPM8
    }
    private State state;

    public static HealthBar_Manager instance;

    GameObject HP_Base;
    //GameObject HP_Meat;
    GameObject HP_OverlayColor;
    GameObject HP_OverlayLines;
    GameObject HP_Avatar;

    GameObject MONSTER_Base;
    GameObject MONSTER_OverlayColor;
    GameObject MONSTER_OverlayLines;
    GameObject TebasCounter;
    GameObject TebasCounter_Text;
    private float TebasCounter_Delay = 0;
    private int TebasCounter_Last = 0;
    GameObject InventoryIcon;
    public static bool newItem = false;
    public Sprite inventoryStandard;
    public Sprite inventoryHighlight;

    GameObject player;
    float realHealth;

    public GameObject boss;
    public bool refreshBoss;
    float realHealthBoss;
    float maxHealthBoss = 100;
    float hp_amount_lerpBoss = 0;
    float convertHealthBoss = 0;

    public Sprite meat_1;
    public Sprite meat_2;
    public Sprite meat_3;

    Color hp_color_1;
    Color hp_color_2;
    Color hp_color_3;
    Color hp_color_4;
    Color hp_color_5;
    Color hp_color_6;
    Color hp_color_7;
    Color hp_color_8;

    float hp_amount_lerp = 0;
    float convertHealth = 0;

    float color_time = 0f;
    public static bool alpha_reduce = false;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        state = State.HPM8;

        player = GameObject.FindGameObjectWithTag("Player");

        HP_Base = DialogSystem.getChildGameObject(gameObject, "HP_Base");
        //HP_Meat = DialogSystem.getChildGameObject(gameObject, "HP_Meat");
        HP_OverlayColor = DialogSystem.getChildGameObject(gameObject, "HP_OverlayColor");
        HP_OverlayLines = DialogSystem.getChildGameObject(gameObject, "HP_OverlayLines");
        HP_Avatar = DialogSystem.getChildGameObject(gameObject, "HP_Avatar");

        MONSTER_Base = DialogSystem.getChildGameObject(gameObject, "MONSTER_Base");
        MONSTER_OverlayColor = DialogSystem.getChildGameObject(gameObject, "MONSTER_OverlayColor");
        MONSTER_OverlayLines = DialogSystem.getChildGameObject(gameObject, "MONSTER_OverlayLines");

        MONSTER_Base.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        MONSTER_OverlayColor.GetComponent<Image>().color = new Color(0.828f, 0.1284265f, 0.1284265f, 0.0f);
        MONSTER_OverlayLines.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        TebasCounter = DialogSystem.getChildGameObject(gameObject, "TebasCounter");
        TebasCounter_Text = DialogSystem.getChildGameObject(gameObject, "TebasCounter_Text");

        InventoryIcon = DialogSystem.getChildGameObject(gameObject, "Inventory_Icon");

        hp_color_8 = new Color(0.2588235f, 0.8584604f, 0.9607843f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_7 = new Color(0.2588235f, 0.9607843f, 0.5990860f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_6 = new Color(0.4011565f, 0.9150943f, 0.2719384f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_5 = new Color(0.9137255f, 0.8126158f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_4 = new Color(0.9137255f, 0.6267108f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_3 = new Color(0.9137255f, 0.4769231f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_2 = new Color(0.9137255f, 0.3714983f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_1 = new Color(0.9137255f, 0.2947789f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);

        StartCoroutine(Alpha_Control_Disable());
    }

    // Update is called once per frame
    void Update()
    {
        // =-------------------- PLAYER HEALTH --------------------=

        #region PLAYER HEALTH
        hp_color_8 = new Color(0.2588235f, 0.8584604f, 0.9607843f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_7 = new Color(0.2588235f, 0.9607843f, 0.5990860f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_6 = new Color(0.4011565f, 0.9150943f, 0.2719384f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_5 = new Color(0.9137255f, 0.8126158f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_4 = new Color(0.9137255f, 0.6267108f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_3 = new Color(0.9137255f, 0.4769231f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_2 = new Color(0.9137255f, 0.3714983f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);
        hp_color_1 = new Color(0.9137255f, 0.2947789f, 0.2705883f, HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color.a);

        /*
        float meat = player.GetComponent<Animator>().GetFloat("numberOfMeat");
        switch (meat)
        {
            case -1:

                HP_Meat.SetActive(false);
                break;

            case 0:

                HP_Meat.SetActive(false);
                break;

            case 1:

                HP_Meat.SetActive(true);
                HP_Meat.GetComponent<Image>().sprite = meat_1;
                break;

            case 2:

                HP_Meat.SetActive(true);
                HP_Meat.GetComponent<Image>().sprite = meat_2;
                break;

            case 3:

                HP_Meat.SetActive(true);
                HP_Meat.GetComponent<Image>().sprite = meat_3;
                break;
        }*/

        realHealth = GameManager.instance.GetHealth();
        convertHealth = realHealth / GameManager.instance.maxHealth;
        hp_amount_lerp = Mathf.Lerp(hp_amount_lerp, convertHealth, 6f * Time.deltaTime);
        //print("HP_MANAGER: " + realHealth + ", " + convertHealth);
        HP_OverlayColor.GetComponent<Image>().fillAmount = hp_amount_lerp;

        if (convertHealth >= 0.875f)
        {
            state = State.HPM8;
        } else if (convertHealth >= 0.75f)
        {
            state = State.HPM7;
        } else if (convertHealth >= 0.625f)
        {
            state = State.HPM6;
        } else if (convertHealth >= 0.5f)
        {
            state = State.HPM5;
        } else if (convertHealth >= 0.375f)
        {
            state = State.HPM4;
        } else if (convertHealth >= 0.25f)
        {
            state = State.HPM3;
        } else if (convertHealth >= 0.125f)
        {
            state = State.HPM2;
        } else if (convertHealth >= 0f)
        {
            state = State.HPM1;
        }

        switch (state)
        {
            case State.HPM8:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_8)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_8, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_8)
                {
                    color_time = 0f;
                }
                break;

            case State.HPM7:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_7)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_7, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_7)
                {
                    color_time = 0f;
                }
                break;

            case State.HPM6:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_6)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_6, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_6)
                {
                    color_time = 0f;
                }
                break;

            case State.HPM5:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_5)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_5, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_5)
                {
                    color_time = 0f;
                }
                break;

            case State.HPM4:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_4)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_4, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_4)
                {
                    color_time = 0f;
                }
                break;

            case State.HPM3:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_3)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_3, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_3)
                {
                    color_time = 0f;
                }
                break;

            case State.HPM2:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_2)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_2, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_2)
                {
                    color_time = 0f;
                }
                break;

            case State.HPM1:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_1)
                {
                    //StartCoroutine(Alpha_Control_Enable());
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_1, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_1)
                {
                    color_time = 0f;
                }
                break;
        }

        color_time = color_time + 0.005f;
        #endregion

        // =-------------------- BOSS HEALTH --------------------=

        if (boss)
        {
            switch (boss.name)
            {
                case "Bull":

                    realHealthBoss = boss.GetComponent<BullAI>().health;
                    if (refreshBoss) {
                        maxHealthBoss = boss.GetComponent<BullAI>().health;
                        refreshBoss = false;
                    }
                    break;

                case "Goat":
                    realHealthBoss = boss.GetComponent<GoatAI>().health;
                    if (refreshBoss)
                    {
                        maxHealthBoss = boss.GetComponent<GoatAI>().health;
                        refreshBoss = false;
                    }
                    break;

                case "Armor":
                    realHealthBoss = boss.GetComponent<ArmorAI>().health;
                    if (refreshBoss)
                    {
                        maxHealthBoss = boss.GetComponent<ArmorAI>().health;
                        refreshBoss = false;
                    }
                    break;

                case "SpiderGranny":
                    realHealthBoss = boss.GetComponent<CEOofSpidersAI>().health;
                    if (refreshBoss)
                    {
                        maxHealthBoss = boss.GetComponent<CEOofSpidersAI>().health;
                        refreshBoss = false;
                    }
                    break;

                case "SpiderMommy":
                    realHealthBoss = boss.GetComponent<CEOofSpidersAI>().health;
                    if (refreshBoss)
                    {
                        maxHealthBoss = boss.GetComponent<CEOofSpidersAI>().health;
                        refreshBoss = false;
                    }
                    break;

                case "Trickster":
                    realHealthBoss = boss.GetComponent<TricksterAI>().health;
                    if (refreshBoss)
                    {
                        maxHealthBoss = boss.GetComponent<TricksterAI>().health;
                        refreshBoss = false;
                    }
                    break;
            }

            convertHealthBoss = realHealthBoss / maxHealthBoss;

            hp_amount_lerpBoss = Mathf.Lerp(hp_amount_lerpBoss, convertHealthBoss, 6f * Time.deltaTime);
            MONSTER_OverlayColor.GetComponent<Image>().fillAmount = hp_amount_lerpBoss;

            var monsterovcol = MONSTER_OverlayColor.GetComponent<Image>().color;
            monsterovcol = new Color(monsterovcol.r, monsterovcol.g, monsterovcol.b, Mathf.Lerp(monsterovcol.a, 1f, Time.deltaTime * 4f));
            MONSTER_OverlayColor.GetComponent<Image>().color = monsterovcol;

            var monsterbase = MONSTER_Base.GetComponent<Image>().color;
            monsterbase = new Color(monsterbase.r, monsterbase.g, monsterbase.b, Mathf.Lerp(monsterbase.a, 1f, Time.deltaTime * 4f));
            MONSTER_Base.GetComponent<Image>().color = monsterbase;

            var monsterovline = MONSTER_OverlayLines.GetComponent<Image>().color;
            monsterovline = new Color(monsterovline.r, monsterovline.g, monsterovline.b, Mathf.Lerp(monsterovline.a, 1f, Time.deltaTime * 4f));
            MONSTER_OverlayLines.GetComponent<Image>().color = monsterovline;
        }
        else
        {
            var monsterovcol = MONSTER_OverlayColor.GetComponent<Image>().color;
            monsterovcol = new Color(monsterovcol.r, monsterovcol.g, monsterovcol.b, Mathf.Lerp(monsterovcol.a, 0f, Time.deltaTime * 4f));
            MONSTER_OverlayColor.GetComponent<Image>().color = monsterovcol;

            var monsterbase = MONSTER_Base.GetComponent<Image>().color;
            monsterbase = new Color(monsterbase.r, monsterbase.g, monsterbase.b, Mathf.Lerp(monsterbase.a, 0f, Time.deltaTime * 4f));
            MONSTER_Base.GetComponent<Image>().color = monsterbase;

            var monsterovline = MONSTER_OverlayLines.GetComponent<Image>().color;
            monsterovline = new Color(monsterovline.r, monsterovline.g, monsterovline.b, Mathf.Lerp(monsterovline.a, 0f, Time.deltaTime * 4f));
            MONSTER_OverlayLines.GetComponent<Image>().color = monsterovline;
        }

        // =-------------------- Tebas Counter --------------------=

        if (SceneManager.GetActiveScene().name == "FaseTres")
        {
            //Debug.Log(FaseTresTriggerController.Instance.inimigosMortos);
            if (FaseTresTriggerController.Instance.inimigosMortos > TebasCounter_Last)
            {
                TebasCounter_Delay = 60f;
            }
            if (TebasCounter_Delay > 0)
            {
                TebasCounter.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(TebasCounter.GetComponent<CanvasGroup>().alpha, 1f, Time.deltaTime * 4f);
            } else
            {
                TebasCounter.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(TebasCounter.GetComponent<CanvasGroup>().alpha, 0.5f, Time.deltaTime * 4f);
            }
            
            TebasCounter_Last = FaseTresTriggerController.Instance.inimigosMortos;
            TebasCounter_Delay = Mathf.Lerp(TebasCounter_Delay, 0f, Time.deltaTime * 0.5f);
            TebasCounter_Text.GetComponent<TextMeshProUGUI>().text = "x " + FaseTresTriggerController.Instance.inimigosMortos;
        } else
        {
            TebasCounter.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(TebasCounter.GetComponent<CanvasGroup>().alpha, 0, Time.deltaTime * 4f);
        }

        // =-------------------- Inventory Icon --------------------=

        if (newItem == true)
        {
            InventoryIcon.GetComponent<Image>().sprite = inventoryHighlight;

            var invicon = InventoryIcon.GetComponent<Image>().color;
            invicon = new Color(invicon.r, invicon.g, invicon.b, Mathf.Lerp(invicon.a, 1f, Time.deltaTime * 4f));
            InventoryIcon.GetComponent<Image>().color = invicon;
        } else
        {
            InventoryIcon.GetComponent<Image>().sprite = inventoryStandard;

            var invicon = InventoryIcon.GetComponent<Image>().color;
            invicon = new Color(invicon.r, invicon.g, invicon.b, Mathf.Lerp(invicon.a, 0.6f, Time.deltaTime * 4f));
            InventoryIcon.GetComponent<Image>().color = invicon;
        }

    }

    public static IEnumerator Alpha_Control_Enable()
    {
        HealthBar_Manager.alpha_reduce = true;
        for (int i = 0; i < 60 * 4; i++)
        {
            HealthBar_Manager.alpha_reduce = true;
            var hpovcol = HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color;
            hpovcol = new Color(hpovcol.r, hpovcol.g, hpovcol.b, Mathf.Lerp(hpovcol.a, 1f, Time.deltaTime * 4f));
            HealthBar_Manager.instance.HP_OverlayColor.GetComponent<Image>().color = hpovcol;

            var hpbasecol = HealthBar_Manager.instance.HP_Base.GetComponent<Image>().color;
            hpbasecol = new Color(hpbasecol.r, hpbasecol.g, hpbasecol.b, Mathf.Lerp(hpbasecol.a, 1f, Time.deltaTime * 4f));
            HealthBar_Manager.instance.HP_Base.GetComponent<Image>().color = hpbasecol;

            /*
            var hpmeatcol = HealthBar_Manager.instance.HP_Meat.GetComponent<Image>().color;
            hpmeatcol = new Color(hpmeatcol.r, hpmeatcol.g, hpmeatcol.b, Mathf.Lerp(hpmeatcol.a, 1f, Time.deltaTime * 4f));
            HealthBar_Manager.instance.HP_Meat.GetComponent<Image>().color = hpmeatcol;
            */

            var hplinescol = HealthBar_Manager.instance.HP_OverlayLines.GetComponent<Image>().color;
            hplinescol = new Color(hplinescol.r, hplinescol.g, hplinescol.b, Mathf.Lerp(hplinescol.a, 1f, Time.deltaTime * 4f));
            HealthBar_Manager.instance.HP_OverlayLines.GetComponent<Image>().color = hplinescol;

            var hpav = HealthBar_Manager.instance.HP_Avatar.GetComponent<Image>().color;
            hpav = new Color(hpav.r, hpav.g, hpav.b, Mathf.Lerp(hpav.a, 1f, Time.deltaTime * 4f));
            HealthBar_Manager.instance.HP_Avatar.GetComponent<Image>().color = hpav;
            yield return null;
        }
        HealthBar_Manager.alpha_reduce = false;
    }

    private IEnumerator Alpha_Control_Disable()
    {
        while (true)
        {
            if (HealthBar_Manager.alpha_reduce == false)
            {
                var hpovcol = HP_OverlayColor.GetComponent<Image>().color;
                hpovcol = new Color(hpovcol.r, hpovcol.g, hpovcol.b, Mathf.Lerp(hpovcol.a, 0.3f, Time.deltaTime * 4f));
                HP_OverlayColor.GetComponent<Image>().color = hpovcol;

                var hpbasecol = HP_Base.GetComponent<Image>().color;
                hpbasecol = new Color(hpbasecol.r, hpbasecol.g, hpbasecol.b, Mathf.Lerp(hpbasecol.a, 0.3f, Time.deltaTime * 4f));
                HP_Base.GetComponent<Image>().color = hpbasecol;

                /*
                var hpmeatcol = HP_Meat.GetComponent<Image>().color;
                hpmeatcol = new Color(hpmeatcol.r, hpmeatcol.g, hpmeatcol.b, Mathf.Lerp(hpmeatcol.a, 0.3f, Time.deltaTime * 4f));
                HP_Meat.GetComponent<Image>().color = hpmeatcol;
                */

                var hplinescol = HP_OverlayLines.GetComponent<Image>().color;
                hplinescol = new Color(hplinescol.r, hplinescol.g, hplinescol.b, Mathf.Lerp(hplinescol.a, 0.3f, Time.deltaTime * 4f));
                HP_OverlayLines.GetComponent<Image>().color = hplinescol;

                var hpav = HealthBar_Manager.instance.HP_Avatar.GetComponent<Image>().color;
                hpav = new Color(hpav.r, hpav.g, hpav.b, Mathf.Lerp(hpav.a, 0.3f, Time.deltaTime * 4f));
                HealthBar_Manager.instance.HP_Avatar.GetComponent<Image>().color = hpav;
            }
            
            yield return null;
        }
    }
}
