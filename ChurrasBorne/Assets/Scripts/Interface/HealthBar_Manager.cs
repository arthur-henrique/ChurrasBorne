using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        HPM6
    }
    private State state;

    GameObject HP_Base;
    GameObject HP_Meat;
    GameObject HP_OverlayColor;
    GameObject HP_OverlayLines;

    GameObject player;
    float realHealth;

    public Sprite meat_1;
    public Sprite meat_2;
    public Sprite meat_3;

    Color hp_color_1;
    Color hp_color_2;
    Color hp_color_3;
    Color hp_color_4;
    Color hp_color_5;
    Color hp_color_6;

    float hp_amount_lerp = 0;
    float convertHealth = 0;

    float color_time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        state = State.HPM6;

        player = GameObject.FindGameObjectWithTag("Player");

        HP_Base = DialogSystem.getChildGameObject(gameObject, "HP_Base");
        HP_Meat = DialogSystem.getChildGameObject(gameObject, "HP_Meat");
        HP_OverlayColor = DialogSystem.getChildGameObject(gameObject, "HP_OverlayColor");
        HP_OverlayLines = DialogSystem.getChildGameObject(gameObject, "HP_OverlayLines");

        hp_color_6 = new Color(0.1254902f, 0.7459202f, 0.9058824f);
        hp_color_5 = new Color(0.1254902f, 0.9058824f, 0.3886421f);
        hp_color_4 = new Color(0.6274008f, 0.9058824f, 0.1254902f);
        hp_color_3 = new Color(0.9058824f, 0.8306620f, 0.1254902f);
        hp_color_2 = new Color(0.9058824f, 0.4308138f, 0.1254902f);
        hp_color_1 = new Color(0.9058824f, 0.2118464f, 0.1254902f);
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        realHealth = GameManager.instance.GetHealth();
        convertHealth = realHealth / GameManager.instance.maxHealth;
        hp_amount_lerp = Mathf.Lerp(hp_amount_lerp, convertHealth, 4f * Time.deltaTime);
        HP_OverlayColor.GetComponent<Image>().fillAmount = hp_amount_lerp;

        if (convertHealth >= 0.835f)
        {
            state = State.HPM6;
        } else if (convertHealth >= 0.667f)
        {
            state = State.HPM5;
        } else if (convertHealth >= 0.503f)
        {
            state = State.HPM4;
        } else if (convertHealth >= 0.333f)
        {
            state = State.HPM3;
        } else if (convertHealth >= 0.16f)
        {
            state = State.HPM2;
        } else if (convertHealth >= 0f)
        {
            state = State.HPM1;
        }

        switch (state)
        {
            case State.HPM6:

                if (HP_OverlayColor.GetComponent<Image>().color != hp_color_6)
                {
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
                    HP_OverlayColor.GetComponent<Image>().color = Color.Lerp(HP_OverlayColor.GetComponent<Image>().color, hp_color_1, color_time);
                }
                if (HP_OverlayColor.GetComponent<Image>().color == hp_color_1)
                {
                    color_time = 0f;
                }
                break;
        }

        color_time = color_time + 0.05f;
    }
}
