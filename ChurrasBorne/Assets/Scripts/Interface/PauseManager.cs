using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public GameObject canvas; // TransitionCanvas NEEDS to be in scene
    PlayerController pc;

    public static bool selection_confirm = false;
    public static int selection_position;

    private GameObject pause_bg;
    private GameObject pause_drop_shadow;
    private GameObject pause_layered_shadow;

    private GameObject pause_label;
    private GameObject pause_sel1;
    private GameObject pause_sel2;
    private GameObject pause_sel3;

    private Vector3 velocity_bg = Vector3.zero;
    private Vector3 velocity_drop_shadow = Vector3.zero;
    private Vector3 velocity_sel1 = Vector3.zero;
    private Vector3 velocity_sel2 = Vector3.zero;
    private Vector3 velocity_sel3 = Vector3.zero;

    Coroutine cr_pause_bg;
    Coroutine cr_pause_lay_sh;
    Coroutine cr_pause_drop_sh;
    Coroutine cr_pause_label;
    Coroutine cr_pause_sel;

    public AudioSource audioSource;
    public AudioClip ui_move;
    public AudioClip ui_confirm;

    private bool canChange = false;
    public static bool isPaused = false;
    float ypos = 34.3f;

    private void Awake()
    {
        pc = new PlayerController();
        audioSource = GetComponent<AudioSource>();
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
        instance = this;
        canvas = GameObject.Find("TransitionCanvas"); // TransitionCanvas NEEDS to be in scene

        pause_bg = DialogSystem.getChildGameObject(gameObject, "PAUSE_Background");
        pause_bg.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
        pause_bg.GetComponent<Image>().color = new Color(0.4245283f, 0.4245283f, 0.4245283f, 0.0f);
        //pause_bg.GetComponent<Image>().color = new Color(0.5607843f, 1.0f, 1.0f, 1.0f);

        pause_layered_shadow = DialogSystem.getChildGameObject(gameObject, "PAUSE_LayeredShadow");
        pause_layered_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // pause_drop_shadow.GetComponent<Image>().color = new Color(0.8705882f, 1.0f, 1.0f, 1.0f);

        pause_drop_shadow = DialogSystem.getChildGameObject(gameObject, "PAUSE_DropShadow");
        pause_drop_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // pause_drop_shadow.GetComponent<Image>().color = new Color(0.6745098f, 1.0f, 1.0f, 1.0f);
        pause_drop_shadow.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 108, 0);

        pause_label = DialogSystem.getChildGameObject(gameObject, "PAUSE_Pause");
        pause_label.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        pause_sel1 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Continuar");
        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_sel1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 34.3f + 24, 0);

        pause_sel2 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Hub");
        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_sel2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -15.7f + 24, 0);

        pause_sel3 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Titulo");
        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_sel3.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -82.5f + 24, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (pc.UI.Pause.WasPressedThisFrame())
        {
            audioSource.PlayOneShot(ui_confirm, audioSource.volume);
            if (isPaused && GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetBool("isDead") == false)
            {
                Hide_Pause();
            } else
            {
                canChange = false;
                Show_Pause();
            }
            
        }
        
        switch (selection_position) {
            case 0: ypos = 34.3f; break;
            case 1: ypos = -15.7f; break;
            case 2: ypos = -98.4f; break;
        }

        if (canChange == true)
        {
            switch (selection_position)
            {
                case 0:
                    pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                    pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    break;

                case 1:
                    if (GameManager.instance.isTut)
                    {
                        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    } else
                    {
                        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    }
                    
                    break;

                case 2:
                    pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                    break;
            }
        }

        if (isPaused)
        {
            if (pc.Movimento.NorteSul.WasPressedThisFrame())
            {
                selection_position -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                if (selection_position > 2) { selection_position = 0; }
                if (selection_position < 0) { selection_position = 2; }
                audioSource.PlayOneShot(ui_move, audioSource.volume);
            }

            if (pc.Movimento.Attack.WasPressedThisFrame())
            {
                selection_confirm = true;
                audioSource.PlayOneShot(ui_confirm, audioSource.volume);
            }

            //StopCoroutine(cr_pause_drop_sh);
            pause_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, ypos, 0), ref velocity_drop_shadow, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            if (selection_confirm == true)
            {
                switch (selection_position)
                {
                    case 0:
                        
                        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                        Hide_Pause();
                        selection_confirm = false;
                        break;

                    case 1:
                        if (GameManager.instance.isTut)
                        {
                            // pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                        } else
                        {
                            pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                            canvas.GetComponent<Transition_Manager>().RestartScene("Hub", 100, 3, true, null);
                        }
                        selection_confirm = false;
                        canChange = false;
                        isPaused = false;
                        Hide_Pause();
                        break;

                    case 2:
                        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                        canvas.GetComponent<Transition_Manager>().RestartScene("MainMenu", 100, 3, true, null);
                        selection_confirm = false;
                        canChange = false;
                        isPaused = false;
                        break;
                }
            }
        }
    }

    private void Show_Pause()
    {
        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, pause_sel1.GetComponent<TextMeshProUGUI>().color.a);
        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, pause_sel2.GetComponent<TextMeshProUGUI>().color.a);
        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, pause_sel3.GetComponent<TextMeshProUGUI>().color.a);

        selection_position = 0;
        isPaused = true;
        if (cr_pause_bg != null)
            StopCoroutine(cr_pause_bg);
        if (cr_pause_lay_sh != null)
            StopCoroutine(cr_pause_lay_sh);
        if (cr_pause_drop_sh != null)
            StopCoroutine(cr_pause_drop_sh);
        if (cr_pause_label != null)
            StopCoroutine(cr_pause_label);
        if (cr_pause_sel != null)
            StopCoroutine(cr_pause_sel);
        cr_pause_bg = StartCoroutine(Pause_Background_Zoom_In());
        cr_pause_lay_sh = StartCoroutine(Pause_Layered_Shadow_Fade_In());
        cr_pause_drop_sh = StartCoroutine(Pause_Drop_Shadow_Fade_In());
        cr_pause_label = StartCoroutine(Pause_Label_Fade_In());
        cr_pause_sel = StartCoroutine(Pause_Selections_Fade_In());
        Time.timeScale = 0;
    }

    private void Hide_Pause()
    {
        canChange = false;
        isPaused = false;
        if (cr_pause_bg != null)
            StopCoroutine(cr_pause_bg);
        if (cr_pause_lay_sh != null)
            StopCoroutine(cr_pause_lay_sh);
        if (cr_pause_drop_sh != null)
            StopCoroutine(cr_pause_drop_sh);
        if (cr_pause_label != null)
            StopCoroutine(cr_pause_label);
        if (cr_pause_sel != null)
            StopCoroutine(cr_pause_sel);
        cr_pause_bg = StartCoroutine(Pause_Background_Zoom_Out());
        cr_pause_lay_sh = StartCoroutine(Pause_Layered_Shadow_Fade_Out());
        cr_pause_drop_sh = StartCoroutine(Pause_Drop_Shadow_Fade_Out());
        cr_pause_label = StartCoroutine(Pause_Label_Fade_Out());
        cr_pause_sel = StartCoroutine(Pause_Selections_Fade_Out());
        Time.timeScale = 1;
    }

    #region Fade In
    private IEnumerator Pause_Background_Zoom_In()
    {
        for (int i = 0; i < 60 * 4; i++)
        {
            pause_bg.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(pause_bg.GetComponent<RectTransform>().localScale, new Vector3(1, 1, 1), ref velocity_bg, 4 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            //pause_bg.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            pause_bg.GetComponent<Image>().color = new Color(0.4245283f, 0.4245283f, 0.4245283f, Mathf.Lerp(pause_bg.GetComponent<Image>().color.a, 0.8156863f, Time.unscaledDeltaTime * 5f));
            yield return null;
        }
    }

    private IEnumerator Pause_Layered_Shadow_Fade_In()
    {
        for (int i = 0; i < 60 * 4; i++)
        {
            pause_layered_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_layered_shadow.GetComponent<Image>().color.a, 0.8705882f, Time.unscaledDeltaTime * 7f));
            yield return null;
        }
    }

    private IEnumerator Pause_Drop_Shadow_Fade_In()
    {
        for (int i = 0; i < 60 * 8; i++)
        {
            pause_drop_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_drop_shadow.GetComponent<Image>().color.a, 0.6745098f, Time.unscaledDeltaTime * 7f));
            yield return null;
        }
    }

    private IEnumerator Pause_Label_Fade_In()
    {
        for (int i = 0; i < 60 * 4; i++)
        {
            pause_label.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_label.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
            yield return null;
        }
    }

    private IEnumerator Pause_Selections_Fade_In()
    {
        for (int i = 0; i < 60 * 4; i++)
        {
            Color psel1c = pause_sel1.GetComponent<TextMeshProUGUI>().color;
            Color psel2c = pause_sel2.GetComponent<TextMeshProUGUI>().color;
            Color psel3c = pause_sel3.GetComponent<TextMeshProUGUI>().color;

            if (canChange == false)
            {
                pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(psel1c.r, psel1c.g, psel1c.b, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
                pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(psel2c.r, psel2c.g, psel2c.b, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
                pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(psel3c.r, psel3c.g, psel3c.b, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
            }

            if (pause_sel1.GetComponent<TextMeshProUGUI>().color.a >= 0.85f)
            {
                canChange = true;
            }

            pause_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, 34.3f, 0), ref velocity_sel1, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel2.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -15.7f, 0), ref velocity_sel2, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel3.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -82.5f, 0), ref velocity_sel3, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            yield return null;
        }
        
    }
    #endregion

    #region Fade Out
    private IEnumerator Pause_Background_Zoom_Out()
    {
        for (int i = 0; i < 60 * 4; i++)
        {
            pause_bg.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(pause_bg.GetComponent<RectTransform>().localScale, new Vector3(1.5f, 1.5f, 1), ref velocity_bg, 4 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            //pause_bg.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            pause_bg.GetComponent<Image>().color = new Color(0.4245283f, 0.4245283f, 0.4245283f, Mathf.Lerp(pause_bg.GetComponent<Image>().color.a, 0.0f, Time.unscaledDeltaTime * 5f));
            yield return null;
        }
    }

    private IEnumerator Pause_Layered_Shadow_Fade_Out()
    {
        for (int i = 0; i < 60 * 4; i++)
        {
            pause_layered_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_layered_shadow.GetComponent<Image>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            yield return null;
        }
    }

    private IEnumerator Pause_Drop_Shadow_Fade_Out()
    {
        for (int i = 0; i < 60 * 8; i++)
        {
            pause_drop_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_drop_shadow.GetComponent<Image>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            yield return null;
        }
        pause_drop_shadow.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 108, 0);
    }

    private IEnumerator Pause_Label_Fade_Out()
    {
        for (int i = 0; i < 60 * 4; i++)
        {
            pause_label.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_label.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            yield return null;
        }
    }

    private IEnumerator Pause_Selections_Fade_Out()
    {
        for (int i = 0; i < 60 * 3; i++)
        {
            Color psel1c = pause_sel1.GetComponent<TextMeshProUGUI>().color;
            Color psel2c = pause_sel2.GetComponent<TextMeshProUGUI>().color;
            Color psel3c = pause_sel3.GetComponent<TextMeshProUGUI>().color;

            pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(psel1c.r, psel1c.g, psel1c.b, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(psel2c.r, psel2c.g, psel2c.b, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(psel3c.r, psel3c.g, psel3c.b, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));

            pause_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, 34.3f - 24, 0), ref velocity_sel1, 14 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel2.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -15.7f - 24, 0), ref velocity_sel2, 14 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel3.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -82.5f - 24, 0), ref velocity_sel3, 14 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            yield return null;
        }
        
        pause_sel1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 34.3f + 24, 0);
        pause_sel2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -15.7f + 24, 0);
        pause_sel3.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -82.5f + 24, 0);
    }
    #endregion
}
