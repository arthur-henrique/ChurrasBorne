using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    PlayerController pc;

    [SerializeField]
    private TMP_Text _title;

    [SerializeField]
    private TMP_Text _narrator;

    private GameObject narrator_box;

    private GameObject title_box;
    private Vector2 title_box_pos;
    private Vector2 title_box_vel = Vector2.zero;

    public Sprite[] spriteArray; // sprites adicionados pelo inspetor, verificar prefab associado DialogBox
    private GameObject portrait_1;
    private GameObject portrait_2;

    private Vector2 portrait_1_pos;
    private Vector2 portrait_2_pos;
    private Vector2 portrait_1_vel = Vector2.zero;
    private Vector2 portrait_2_vel = Vector2.zero;
    private Color portrait_1_col;
    private Color portrait_2_col;
    private float portrait_1_col_a_vel, portrait_1_col_r_vel, portrait_1_col_g_vel, portrait_1_col_b_vel = 0.0f;
    private float portrait_2_col_a_vel, portrait_2_col_r_vel, portrait_2_col_g_vel, portrait_2_col_b_vel = 0.0f;

    private int letterCounter = 0;
    private bool refreshText = false;

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

    IEnumerator Start()
    {
        portrait_1 = getChildGameObject(gameObject, "Portrait_1");
        portrait_2 = getChildGameObject(gameObject, "Portrait_2");

        narrator_box = getChildGameObject(gameObject, "Narrator_Box");
        title_box = getChildGameObject(gameObject, "Title_Box");

        portrait_1.transform.position = new Vector2(-1450, -70);
        portrait_2.transform.position = new Vector2(1450, -70);
        portrait_1_pos = new Vector2(-1450, -70);
        portrait_2_pos = new Vector2(1450, -70);

        title_box.transform.position = new Vector2(0f, -750f);
        title_box_pos = new Vector2(0f, -750f);

        StartCoroutine(RenderManager());

        while (true)
        {
            int totalVisibleCharacters = _title.textInfo.characterCount;
            int visibleCount = letterCounter % (totalVisibleCharacters + 1);

            _title.maxVisibleCharacters = visibleCount;

            if (letterCounter < totalVisibleCharacters)
            {
                letterCounter += 1;
            }

            yield return new WaitForSeconds(0.004f);
        }

    }

    public IEnumerator RenderManager()
    {
        while (true)
        {
            title_box.GetComponent<RectTransform>().anchoredPosition =
                                Vector2.SmoothDamp(title_box.GetComponent<RectTransform>().anchoredPosition, title_box_pos, ref title_box_vel, 0.05f);

            portrait_1.GetComponent<RectTransform>().anchoredPosition =
                                Vector2.SmoothDamp(portrait_1.GetComponent<RectTransform>().anchoredPosition, portrait_1_pos, ref portrait_1_vel, 0.15f);
            portrait_2.GetComponent<RectTransform>().anchoredPosition =
                                    Vector2.SmoothDamp(portrait_2.GetComponent<RectTransform>().anchoredPosition, portrait_2_pos, ref portrait_2_vel, 0.15f);

            portrait_1.GetComponent<Image>().color = new Color(
                                                    Mathf.SmoothDamp(portrait_1.GetComponent<Image>().color.r, portrait_1_col.r, ref portrait_1_col_r_vel, 0.15f),
                                                    Mathf.SmoothDamp(portrait_1.GetComponent<Image>().color.g, portrait_1_col.g, ref portrait_1_col_g_vel, 0.15f),
                                                    Mathf.SmoothDamp(portrait_1.GetComponent<Image>().color.b, portrait_1_col.b, ref portrait_1_col_b_vel, 0.15f),
                                                    Mathf.SmoothDamp(portrait_1.GetComponent<Image>().color.a, portrait_1_col.a, ref portrait_1_col_a_vel, 0.15f)
                                                    );

            portrait_2.GetComponent<Image>().color = new Color(
                                                    Mathf.SmoothDamp(portrait_2.GetComponent<Image>().color.r, portrait_2_col.r, ref portrait_2_col_r_vel, 0.15f),
                                                    Mathf.SmoothDamp(portrait_2.GetComponent<Image>().color.g, portrait_2_col.g, ref portrait_2_col_g_vel, 0.15f),
                                                    Mathf.SmoothDamp(portrait_2.GetComponent<Image>().color.b, portrait_2_col.b, ref portrait_2_col_b_vel, 0.15f),
                                                    Mathf.SmoothDamp(portrait_2.GetComponent<Image>().color.a, portrait_2_col.a, ref portrait_2_col_a_vel, 0.15f)
                                                    );
            yield return null;
        } 
    }

    public IEnumerator DialogSimple()
    {
        title_box_pos = new Vector2(0f, -360f);
        yield return new WaitForSeconds(.1f);

        for (int i = 0; !pc.Movimento.Attack.WasPressedThisFrame(); i++)
        {
            yield return null;
        }

        GameManager.isInDialog = false;
        PlayerMovement.EnableControl();

        title_box_pos = new Vector2(0f, -750f);
        
    }

    public IEnumerator DialogComplex(int num, GameObject gm)
    {
        title_box_pos = new Vector2(0f, -360f);
        portrait_1.GetComponent<Image>().sprite = spriteArray[3];
        portrait_2.GetComponent<Image>().sprite = spriteArray[3];
        if (PlayerPrefs.GetInt("LANGUAGE") == 0) // english
        {
            for (int i = 0; DialogBank.portuguese_dialog_bank_new[num][i, 0] != ""; i++)
            {
                refreshText = true;
                letterCounter = 0;
                _title.text = DialogBank.portuguese_dialog_bank_new[num][i, 3];
                _narrator.text = DialogBank.portuguese_dialog_bank_new[num][i, 1];
                if (DialogBank.portuguese_dialog_bank_new[num][i, 0] == "left")
                {
                    portrait_1_pos = new Vector2(-480f, 0f);
                    portrait_2_pos = new Vector2(480f, -50f);

                    portrait_1_col = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    portrait_2_col = new Color(0.6f, 0.6f, 0.6f, 0.6f);

                }
                else
                {
                    portrait_1_pos = new Vector2(-480f, -50f);
                    portrait_2_pos = new Vector2(480f, 0f);

                    portrait_1_col = new Color(0.6f, 0.6f, 0.6f, 0.6f);
                    portrait_2_col = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                }

                for (int j = 0; j < spriteArray.Length; j++)
                {
                    if (spriteArray[j].name == DialogBank.portuguese_dialog_bank_new[num][i, 2])
                    {
                        if (DialogBank.portuguese_dialog_bank_new[num][i, 0] == "left")
                        {
                            portrait_1.GetComponent<Image>().sprite = spriteArray[j];
                        }
                        else
                        {
                            portrait_2.GetComponent<Image>().sprite = spriteArray[j];
                        }

                        break;
                    }
                }
            }

            yield return new WaitForSeconds(.5f);

            for (int j = 0; !pc.Movimento.Attack.WasPressedThisFrame(); j++)
            {
                yield return null;
            }
        }

        // ----------------------------------------------------------------------------------------------------

        if (PlayerPrefs.GetInt("LANGUAGE") == 1) // portuguese
        {
            for (int i = 0; DialogBank.portuguese_dialog_bank_new[num][i,0] != ""; i++)
            {
                for (int j = 0; pc.Movimento.Attack.WasPressedThisFrame(); j++)
                {
                    yield return null;
                }
                Debug.Log("dialogo index = " + DialogBank.portuguese_dialog_bank_new[num][i, 3]);
                refreshText = true;
                letterCounter = 0;
                _title.text = DialogBank.portuguese_dialog_bank_new[num][i, 3];
                _narrator.text = DialogBank.portuguese_dialog_bank_new[num][i, 1];
                if (DialogBank.portuguese_dialog_bank_new[num][i, 0] == "left")
                {
                    portrait_1_pos = new Vector2(-480f,   0f);
                    portrait_2_pos = new Vector2( 480f, -50f);

                    portrait_1_col = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    portrait_2_col = new Color(0.6f, 0.6f, 0.6f, 0.6f);

                } else
                {
                    portrait_1_pos = new Vector2(-480f, -50f);
                    portrait_2_pos = new Vector2( 480f,   0f);

                    portrait_1_col = new Color(0.6f, 0.6f, 0.6f, 0.6f);
                    portrait_2_col = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    
                }

                for (int j = 0; j < spriteArray.Length; j++)
                {
                    if (spriteArray[j].name == DialogBank.portuguese_dialog_bank_new[num][i, 2])
                    {
                        if (DialogBank.portuguese_dialog_bank_new[num][i, 0] == "left")
                        {
                            portrait_1.GetComponent<Image>().sprite = spriteArray[j];
                        } else
                        {
                            portrait_2.GetComponent<Image>().sprite = spriteArray[j];
                        }
                        
                        break;
                    }
                }

                for (int j = 0; !pc.Movimento.Attack.WasPressedThisFrame(); j++)
                {
                    yield return null;
                }
            }

            yield return new WaitForSeconds(.5f);

            for (int j = 0; !pc.Movimento.Attack.WasPressedThisFrame(); j++)
            {
                yield return null;
            }
        }

        // ----------------------------------------------------------------------------------------------------

        if (PlayerPrefs.GetInt("LANGUAGE") == 2) // spanish
        {
            for (int i = 0; DialogBank.portuguese_dialog_bank_new[num][i, 0] != ""; i++)
            {
                refreshText = true;
                letterCounter = 0;
                _title.text = DialogBank.portuguese_dialog_bank_new[num][i, 3];
                _narrator.text = DialogBank.portuguese_dialog_bank_new[num][i, 1];
                if (DialogBank.portuguese_dialog_bank_new[num][i, 0] == "left")
                {
                    portrait_1_pos = new Vector2(-480f, 0f);
                    portrait_2_pos = new Vector2(480f, -50f);

                    portrait_1_col = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    portrait_2_col = new Color(0.6f, 0.6f, 0.6f, 0.6f);

                }
                else
                {
                    portrait_1_pos = new Vector2(-480f, -50f);
                    portrait_2_pos = new Vector2(480f, 0f);

                    portrait_1_col = new Color(0.6f, 0.6f, 0.6f, 0.6f);
                    portrait_2_col = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                }

                for (int j = 0; j < spriteArray.Length; j++)
                {
                    if (spriteArray[j].name == DialogBank.portuguese_dialog_bank_new[num][i, 2])
                    {
                        if (DialogBank.portuguese_dialog_bank_new[num][i, 0] == "left")
                        {
                            portrait_1.GetComponent<Image>().sprite = spriteArray[j];
                        }
                        else
                        {
                            portrait_2.GetComponent<Image>().sprite = spriteArray[j];
                        }

                        break;
                    }
                }
            }

            yield return new WaitForSeconds(.5f);

            for (int j = 0; !pc.Movimento.Attack.WasPressedThisFrame(); j++)
            {
                yield return null;
            }
        }

        portrait_1_pos = new Vector2(-1440f, 0f);
        portrait_2_pos = new Vector2(1440f, 0f);

        gm.GetComponent<Animator>().SetTrigger("IDLE");

        GameManager.isInDialog = false;
        PlayerMovement.EnableControl();
        title_box_pos = new Vector2(0f, -750f);
    }

    public void db_SetSceneSimple(int scene_number)
    {
        GameManager.isInDialog = true;
        PlayerMovement.DisableControl();
        letterCounter = 0;
        _narrator.text = "";
        narrator_box.SetActive(false);
        if (PlayerPrefs.GetInt("LANGUAGE") == 0)
        {
            _title.text = DialogBank.english_bank[scene_number];
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 1)
        {
            _title.text = DialogBank.portuguese_bank[scene_number];
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 2)
        {
            _title.text = DialogBank.spanish_bank[scene_number];
        }

        StartCoroutine(DialogSimple());
    }

    public void db_SetSceneComplex(int dialog_piece, GameObject gm)
    {
        GameManager.isInDialog = true;
        PlayerMovement.DisableControl();
        letterCounter = 0;
        narrator_box.SetActive(true);
        StartCoroutine(DialogComplex(dialog_piece, gm));
    }

    public static GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
