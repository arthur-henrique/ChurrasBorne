using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public GameObject canvas; // TransitionCanvas NEEDS to be in scene
    PlayerController pc;
    public AudioMixer mixer;

    public static bool selection_confirm = false;
    public static int selection_position;

    private GameObject pause_bg;
    private GameObject pause_drop_shadow;
    private GameObject pause_layered_shadow;

    private GameObject pause_label;
    private GameObject pause_sel1;
    private GameObject pause_sel2;
    private GameObject pause_sel3;
    private GameObject pause_sel4;

    private GameObject pause_res;
    private GameObject pause_fscreen;
    private GameObject pause_vol_master;
    private GameObject pause_vol_master_text;
    private GameObject pause_vol_master_slider;
    private GameObject pause_vol_bgm;
    private GameObject pause_vol_bgm_text;
    private GameObject pause_vol_bgm_slider;
    private GameObject pause_vol_sfx;
    private GameObject pause_vol_sfx_text;
    private GameObject pause_vol_sfx_slider;
    private GameObject pause_lang;
    private GameObject pause_apply;

    private int restable_opt = 0;
    private int[,] restable = { { 640, 360 },
                                { 854, 480 },
                                { 1024, 576 },
                                { 1280, 720 },
                                { 1920, 1080 },
                                { 3840, 2160 }};

    private int fs_mode_opt = 0;
    private string[] fs_mode = { "Desligada", "Exclusiva", "Borderless" };
    private int lang_mode_opt = 0;
    private string[] lang_mode = { "English", "Português", "Español" };
    private FullScreenMode[] fs_mode_out = { FullScreenMode.Windowed, FullScreenMode.ExclusiveFullScreen, FullScreenMode.FullScreenWindow };

    private Vector3 velocity_bg = Vector3.zero;
    private Vector3 velocity_drop_shadow = Vector3.zero;
    private Vector3 velocity_sel1 = Vector3.zero;
    private Vector3 velocity_sel2 = Vector3.zero;
    private Vector3 velocity_sel3 = Vector3.zero;
    private Vector3 velocity_sel4 = Vector3.zero;

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
    public static bool submenu = false;

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
        restable_opt = PlayerPrefs.GetInt("RESOLUTION_SIZE");
        fs_mode_opt = PlayerPrefs.GetInt("FULLSCREEN_MODE");
        lang_mode_opt = PlayerPrefs.GetInt("LANGUAGE");

        instance = this;
        canvas = GameObject.Find("TransitionCanvas"); // TransitionCanvas NEEDS to be in scene

        pause_bg = DialogSystem.getChildGameObject(gameObject, "PAUSE_Background");
        pause_bg.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
        pause_bg.GetComponent<Image>().color = new Color(0.4245283f, 0.4245283f, 0.4245283f, 0.0f);
        //pause_bg.GetComponent<Image>().color = new Color(0.5607843f, 1.0f, 1.0f, 1.0f);
        pause_bg.SetActive(false);

        pause_layered_shadow = DialogSystem.getChildGameObject(gameObject, "PAUSE_LayeredShadow");
        pause_layered_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // pause_drop_shadow.GetComponent<Image>().color = new Color(0.8705882f, 1.0f, 1.0f, 1.0f);
        pause_layered_shadow.SetActive(false);

        pause_drop_shadow = DialogSystem.getChildGameObject(gameObject, "PAUSE_DropShadow");
        pause_drop_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // pause_drop_shadow.GetComponent<Image>().color = new Color(0.6745098f, 1.0f, 1.0f, 1.0f);
        pause_drop_shadow.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 108, 0);
        pause_drop_shadow.SetActive(false);

        pause_label = DialogSystem.getChildGameObject(gameObject, "PAUSE_Pause");
        pause_label.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        pause_sel1 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Continuar");
        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_sel1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 34.3f + 24, 0);

        pause_sel2 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Hub");
        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_sel2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -15.7f + 24, 0);

        pause_sel3 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Opcoes");
        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_sel3.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -65.7f + 24, 0);

        pause_sel4 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Titulo");
        pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_sel4.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -132.5f + 24, 0);

        pause_res = DialogSystem.getChildGameObject(gameObject, "PAUSE_Resolucao");
        pause_res.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_res.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 135, 0);

        pause_fscreen = DialogSystem.getChildGameObject(gameObject, "PAUSE_TelaCheia");
        pause_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_fscreen.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 85, 0);

        pause_vol_master = DialogSystem.getChildGameObject(gameObject, "PAUSE_Master");
        pause_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_vol_master.GetComponent<RectTransform>().anchoredPosition = new Vector3(-113, 35, 0);
        pause_vol_master_text = DialogSystem.getChildGameObject(gameObject, "PAUSE_MasterVolume");
        pause_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_vol_master_slider = DialogSystem.getChildGameObject(gameObject, "PAUSE_MasterSlider");
        pause_vol_master_slider.GetComponent<CanvasGroup>().alpha = 0f;

        pause_vol_bgm = DialogSystem.getChildGameObject(gameObject, "PAUSE_BGM");
        pause_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_vol_bgm.GetComponent<RectTransform>().anchoredPosition = new Vector3(-113, -15, 0);
        pause_vol_bgm_text = DialogSystem.getChildGameObject(gameObject, "PAUSE_BGMVolume");
        pause_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_vol_bgm_slider = DialogSystem.getChildGameObject(gameObject, "PAUSE_BGMSlider");
        pause_vol_bgm_slider.GetComponent<CanvasGroup>().alpha = 0f;

        pause_vol_sfx = DialogSystem.getChildGameObject(gameObject, "PAUSE_SFX");
        pause_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_vol_sfx.GetComponent<RectTransform>().anchoredPosition = new Vector3(-113, -65, 0);
        pause_vol_sfx_text = DialogSystem.getChildGameObject(gameObject, "PAUSE_SFXVolume");
        pause_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_vol_sfx_slider = DialogSystem.getChildGameObject(gameObject, "PAUSE_SFXSlider");
        pause_vol_sfx_slider.GetComponent<CanvasGroup>().alpha = 0f;

        pause_lang = DialogSystem.getChildGameObject(gameObject, "PAUSE_Linguagem");
        pause_lang.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_lang.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -115, 0);

        pause_apply = DialogSystem.getChildGameObject(gameObject, "PAUSE_Aplicar");
        pause_apply.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pause_apply.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -165, 0);

        mixer.SetFloat("MasterVolumeParam", Mathf.Log10(PlayerPrefs.GetFloat("MASTER_VOLUME")) * 20);
        mixer.SetFloat("BGMVolumeParam", Mathf.Log10(PlayerPrefs.GetFloat("BGM_VOLUME")) * 20);
        mixer.SetFloat("SFXVolumeParam", Mathf.Log10(PlayerPrefs.GetFloat("SFX_VOLUME")) * 20);

        pause_vol_master_slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MASTER_VOLUME");
        pause_vol_bgm_slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("BGM_VOLUME");
        pause_vol_sfx_slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX_VOLUME");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("LANGUAGE") == 0) // ENGLISH
        {
            pause_label.GetComponent<TextMeshProUGUI>().text = "Pause Menu";
            pause_sel1.GetComponent<TextMeshProUGUI>().text = "Resume Game";
            pause_sel2.GetComponent<TextMeshProUGUI>().text = "Return to Hub";
            pause_sel3.GetComponent<TextMeshProUGUI>().text = "Options";
            pause_sel4.GetComponent<TextMeshProUGUI>().text = "Return to Main Menu";

            pause_res.GetComponent<TextMeshProUGUI>().text = "Resolution: " + restable[restable_opt, 0] + "x" + restable[restable_opt, 1];
            pause_fscreen.GetComponent<TextMeshProUGUI>().text = "Fullscreen: " + fs_mode[fs_mode_opt];
            pause_lang.GetComponent<TextMeshProUGUI>().text = "Language: " + lang_mode[lang_mode_opt];
            pause_apply.GetComponent<TextMeshProUGUI>().text = "Apply and Return";

            fs_mode[0] = "Off";
            fs_mode[1] = "Exclusive";
            fs_mode[2] = "Borderless";
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 1) // PORTUGUESE
        {
            pause_label.GetComponent<TextMeshProUGUI>().text = "Menu de Pause";
            pause_sel1.GetComponent<TextMeshProUGUI>().text = "Continuar o Jogo";
            pause_sel2.GetComponent<TextMeshProUGUI>().text = "Retornar ao Hub";
            pause_sel3.GetComponent<TextMeshProUGUI>().text = "Opções";
            pause_sel4.GetComponent<TextMeshProUGUI>().text = "Retornar à Tela de Título";

            pause_res.GetComponent<TextMeshProUGUI>().text = "Resolução: " + restable[restable_opt, 0] + "x" + restable[restable_opt, 1];
            pause_fscreen.GetComponent<TextMeshProUGUI>().text = "Tela Cheia: " + fs_mode[fs_mode_opt];
            pause_lang.GetComponent<TextMeshProUGUI>().text = "Linguagem: " + lang_mode[lang_mode_opt];
            pause_apply.GetComponent<TextMeshProUGUI>().text = "Aplicar e Retornar";

            fs_mode[0] = "Desligada";
            fs_mode[1] = "Exclusiva";
            fs_mode[2] = "Sem Bordas";
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 2) // SPANISH
        {
            pause_label.GetComponent<TextMeshProUGUI>().text = "Pause Menu";
            pause_sel1.GetComponent<TextMeshProUGUI>().text = "Resume Game";
            pause_sel2.GetComponent<TextMeshProUGUI>().text = "Return to Hub";
            pause_sel3.GetComponent<TextMeshProUGUI>().text = "Options";
            pause_sel4.GetComponent<TextMeshProUGUI>().text = "Return to Main Menu";

            pause_res.GetComponent<TextMeshProUGUI>().text = "Resolución: " + restable[restable_opt, 0] + "x" + restable[restable_opt, 1];
            pause_fscreen.GetComponent<TextMeshProUGUI>().text = "Fullscreen: " + fs_mode[fs_mode_opt];
            pause_lang.GetComponent<TextMeshProUGUI>().text = "Idioma: " + lang_mode[lang_mode_opt];
            pause_apply.GetComponent<TextMeshProUGUI>().text = "Aplicar y Retornar";

            fs_mode[0] = "Apagado";
            fs_mode[1] = "Exclusiva";
            fs_mode[2] = "Sin Bordes";
        }

        pause_vol_master_text.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(pause_vol_master_slider.GetComponent<Slider>().value * 100) + "%";
        pause_vol_bgm_text.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(pause_vol_bgm_slider.GetComponent<Slider>().value * 100) + "%";
        pause_vol_sfx_text.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(pause_vol_sfx_slider.GetComponent<Slider>().value * 100) + "%";


        if (pc.UI.Pause.WasPressedThisFrame())
        {
            if (GameManager.instance.GetAlive() == true)
            {
                audioSource.PlayOneShot(ui_confirm, audioSource.volume);
                if (isPaused && GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetBool("isDead") == false)
                {
                    if (submenu == true)
                    {
                        submenu = false;
                    } else
                    {
                        Hide_Pause();
                        pause_bg.SetActive(false);
                        pause_drop_shadow.SetActive(false);
                        pause_layered_shadow.SetActive(false);
                    }
                }
                else
                {
                    selection_position = 0;
                    canChange = false;
                    submenu = false;
                    Show_Pause();
                    pause_bg.SetActive(true);
                    pause_drop_shadow.SetActive(true);
                    pause_layered_shadow.SetActive(true);
                }
            }
            
        }
        
        if (submenu == false)
        {
            DialogSystem.getChildGameObject(gameObject, "Menu Principal").SetActive(true);
            DialogSystem.getChildGameObject(gameObject, "Menu de Opções").SetActive(false);
            switch (selection_position)
            {
                case 0: ypos = 34.3f; break;
                case 1: ypos = -15.7f; break;
                case 2: ypos = -65.7f; break;
                case 3: ypos = -132.5f; break;
            }
        } else
        {
            DialogSystem.getChildGameObject(gameObject, "Menu Principal").SetActive(false);
            DialogSystem.getChildGameObject(gameObject, "Menu de Opções").SetActive(true);
            switch (selection_position)
            {
                case 0: ypos = 135; break;
                case 1: ypos = 85; break;
                case 2: ypos = 35; break;
                case 3: ypos = -15; break;
                case 4: ypos = -65; break;
                case 5: ypos = -115; break;
                case 6: ypos = -165; break;
            }
        }

        if (canChange == true)
        {
            if (submenu == false)
            {
                switch (selection_position)
                {
                    case 0:
                        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel4.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        break;

                    case 1:
                        if (GameManager.instance.isTut)
                        {
                            pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                            pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                            pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                            pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel4.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        }
                        else
                        {
                            pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                            pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                            pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                            pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel4.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        }

                        break;

                    case 2:
                        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                        pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel4.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        break;

                    case 3:
                        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                        pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_sel4.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                        break;
                }
            } else
            {
                var alpha_res = 1.0f;
                var alpha_fscreen = 0.3f;
                var alpha_vol_master = 0.3f;
                var alpha_vol_bgm = 0.3f;
                var alpha_vol_sfx = 0.3f;
                var alpha_lang = 0.3f;
                var alpha_apply = 0.3f;
                switch (selection_position)
                {
                    case 0:

                        alpha_res = 1.0f;
                        alpha_fscreen = 0.3f;
                        alpha_vol_master = 0.3f;
                        alpha_vol_bgm = 0.3f;
                        alpha_vol_sfx = 0.3f;
                        alpha_lang = 0.3f;
                        alpha_apply = 0.3f;
                        break;

                    case 1:
                        alpha_res = 0.3f;
                        alpha_fscreen = 1.0f;
                        alpha_vol_master = 0.3f;
                        alpha_vol_bgm = 0.3f;
                        alpha_vol_sfx = 0.3f;
                        alpha_lang = 0.3f;
                        alpha_apply = 0.3f;
                        break;

                    case 2:
                        alpha_res = 0.3f;
                        alpha_fscreen = 0.3f;
                        alpha_vol_master = 1.0f;
                        alpha_vol_bgm = 0.3f;
                        alpha_vol_sfx = 0.3f;
                        alpha_lang = 0.3f;
                        alpha_apply = 0.3f;
                        break;

                    case 3:
                        alpha_res = 0.3f;
                        alpha_fscreen = 0.3f;
                        alpha_vol_master = 0.3f;
                        alpha_vol_bgm = 1.0f;
                        alpha_vol_sfx = 0.3f;
                        alpha_lang = 0.3f;
                        alpha_apply = 0.3f;
                        break;

                    case 4:
                        alpha_res = 0.3f;
                        alpha_fscreen = 0.3f;
                        alpha_vol_master = 0.3f;
                        alpha_vol_bgm = 0.3f;
                        alpha_vol_sfx = 1.0f;
                        alpha_lang = 0.3f;
                        alpha_apply = 0.3f;
                        break;

                    case 5:
                        alpha_res = 0.3f;
                        alpha_fscreen = 0.3f;
                        alpha_vol_master = 0.3f;
                        alpha_vol_bgm = 0.3f;
                        alpha_vol_sfx = 0.3f;
                        alpha_lang = 1.0f;
                        alpha_apply = 0.3f;
                        break;

                    case 6:
                        alpha_res = 0.3f;
                        alpha_fscreen = 0.3f;
                        alpha_vol_master = 0.3f;
                        alpha_vol_bgm = 0.3f;
                        alpha_vol_sfx = 0.3f;
                        alpha_lang = 0.3f;
                        alpha_apply = 1.0f;
                        break;
                }
                pause_res.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_res.GetComponent<TextMeshProUGUI>().color.a, alpha_res, Time.unscaledDeltaTime * 5f));
                // ---
                pause_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_fscreen.GetComponent<TextMeshProUGUI>().color.a, alpha_fscreen, Time.unscaledDeltaTime * 5f));
                // ---
                pause_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_vol_master.GetComponent<TextMeshProUGUI>().color.a, alpha_vol_master, Time.unscaledDeltaTime * 5f));
                pause_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_vol_master_text.GetComponent<TextMeshProUGUI>().color.a, alpha_vol_master, Time.unscaledDeltaTime * 5f));
                pause_vol_master_slider.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(pause_vol_master_slider.GetComponent<CanvasGroup>().alpha, alpha_vol_master, Time.unscaledDeltaTime * 5f);
                // ---
                pause_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_vol_bgm.GetComponent<TextMeshProUGUI>().color.a, alpha_vol_bgm, Time.unscaledDeltaTime * 5f));
                pause_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_vol_bgm_text.GetComponent<TextMeshProUGUI>().color.a, alpha_vol_bgm, Time.unscaledDeltaTime * 5f));
                pause_vol_bgm_slider.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(pause_vol_bgm_slider.GetComponent<CanvasGroup>().alpha, alpha_vol_bgm, Time.unscaledDeltaTime * 5f);
                // ---
                pause_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_vol_sfx.GetComponent<TextMeshProUGUI>().color.a, alpha_vol_sfx, Time.unscaledDeltaTime * 5f));
                pause_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_vol_sfx_text.GetComponent<TextMeshProUGUI>().color.a, alpha_vol_sfx, Time.unscaledDeltaTime * 5f));
                pause_vol_sfx_slider.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(pause_vol_sfx_slider.GetComponent<CanvasGroup>().alpha, alpha_vol_sfx, Time.unscaledDeltaTime * 5f);
                // ---
                pause_lang.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_lang.GetComponent<TextMeshProUGUI>().color.a, alpha_lang, Time.unscaledDeltaTime * 5f));
                // ---
                pause_apply.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_apply.GetComponent<TextMeshProUGUI>().color.a, alpha_apply, Time.unscaledDeltaTime * 5f));
            }
            
        }

        if (isPaused)
        {
            if (pc.Movimento.NorteSul.WasPressedThisFrame())
            {
                selection_position -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                if (submenu == false)
                {
                    if (selection_position > 3) { selection_position = 0; }
                    if (selection_position < 0) { selection_position = 3; }
                } else
                {
                    if (selection_position > 6) { selection_position = 0; }
                    if (selection_position < 0) { selection_position = 6; }
                }
                
                audioSource.PlayOneShot(ui_move, audioSource.volume);
            }
            if (pc.Movimento.LesteOeste.WasPressedThisFrame())
            {
                var x = pc.Movimento.LesteOeste.ReadValue<float>();
                if (submenu == true)
                {
                    switch (selection_position)
                    {
                        case 0:

                            if (x > 0) { restable_opt++; }
                            else if (x < 0) { restable_opt--; }
                            audioSource.PlayOneShot(ui_move, audioSource.volume);
                            break;

                        case 1:

                            if (x > 0) { fs_mode_opt++; }
                            else if (x < 0) { fs_mode_opt--; }
                            audioSource.PlayOneShot(ui_move, audioSource.volume);
                            break;

                        case 2:

                            if (x > 0) { pause_vol_master_slider.GetComponent<Slider>().value +=  0.1f; }
                            else if (x < 0) { pause_vol_master_slider.GetComponent<Slider>().value -= 0.1f; }
                            audioSource.PlayOneShot(ui_move, audioSource.volume);
                            break;

                        case 3:

                            if (x > 0) { pause_vol_bgm_slider.GetComponent<Slider>().value += 0.1f; }
                            else if (x < 0) { pause_vol_bgm_slider.GetComponent<Slider>().value -= 0.1f; }
                            audioSource.PlayOneShot(ui_move, audioSource.volume);
                            break;

                        case 4:

                            if (x > 0) { pause_vol_sfx_slider.GetComponent<Slider>().value += 0.1f; }
                            else if (x < 0) { pause_vol_sfx_slider.GetComponent<Slider>().value -= 0.1f; }
                            audioSource.PlayOneShot(ui_move, audioSource.volume);
                            break;

                        case 5:

                            if (x > 0) { lang_mode_opt++; }
                            else if (x < 0) { lang_mode_opt--; }
                            audioSource.PlayOneShot(ui_move, audioSource.volume);
                            break;
                    }
                }
            }

            if (pc.Movimento.Attack.WasPressedThisFrame())
            {
                if ((selection_position == 2 || selection_position == 3 || selection_position == 4) && submenu == true)
                {
                    
                } else
                {
                    selection_confirm = true;
                    audioSource.PlayOneShot(ui_confirm, audioSource.volume);
                }
            }

            //StopCoroutine(cr_pause_drop_sh);
            pause_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, ypos, 0), ref velocity_drop_shadow, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            
            if (selection_confirm == true)
            {
                if (submenu == false)
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
                            }
                            else
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

                            submenu = true;
                            selection_position = 0;
                            selection_confirm = false;
                            break;

                        case 3:
                            pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                            canvas.GetComponent<Transition_Manager>().RestartScene("MainMenu", 100, 3, true, null);
                            selection_confirm = false;
                            canChange = false;
                            isPaused = false;
                            break;
                    }
                } else
                {
                    switch (selection_position)
                    {
                        case 0:

                            restable_opt++;
                            selection_confirm = false;
                            break;

                        case 1:

                            fs_mode_opt++;
                            selection_confirm = false;
                            break;

                        case 5:

                            lang_mode_opt++;
                            selection_confirm = false;
                            break;

                        case 6:

                            Screen.SetResolution(restable[restable_opt, 0], restable[restable_opt, 1], fs_mode_out[fs_mode_opt]);

                            pause_apply.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                            selection_position = 2;
                            submenu = false;
                            selection_confirm = false;
                            PlayerPrefs.SetInt("RESOLUTION_SIZE", restable_opt);
                            PlayerPrefs.SetInt("FULLSCREEN_MODE", fs_mode_opt);
                            break;
                    }
                }
                
            }
        }

        if (restable_opt > restable.Length / 2 - 1) { restable_opt = 0; }
        if (restable_opt < 0) { restable_opt = restable.Length / 2 - 1; }

        if (fs_mode_opt > 2) { fs_mode_opt = 0; }
        if (fs_mode_opt < 0) { fs_mode_opt = 2; }

        if (lang_mode_opt > 2) { lang_mode_opt = 0; }
        if (lang_mode_opt < 0) { lang_mode_opt = 2; }

        PlayerPrefs.SetInt("LANGUAGE", lang_mode_opt);

    }

    public void Show_Pause()
    {
        PlayerMovement.DisableControl();
        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, pause_sel1.GetComponent<TextMeshProUGUI>().color.a);
        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, pause_sel2.GetComponent<TextMeshProUGUI>().color.a);
        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, pause_sel3.GetComponent<TextMeshProUGUI>().color.a);
        pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, pause_sel4.GetComponent<TextMeshProUGUI>().color.a);

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
        PlayerMovement.EnableControl();
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
            Color psel4c = pause_sel4.GetComponent<TextMeshProUGUI>().color;

            if (canChange == false)
            {
                pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(psel1c.r, psel1c.g, psel1c.b, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
                pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(psel2c.r, psel2c.g, psel2c.b, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
                pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(psel3c.r, psel3c.g, psel3c.b, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
                pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(psel4c.r, psel4c.g, psel4c.b, Mathf.Lerp(pause_sel4.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 4f));
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
                            Vector3.SmoothDamp(pause_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -65.7f, 0), ref velocity_sel3, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel4.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel4.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -132.5f, 0), ref velocity_sel4, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
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
            Color psel4c = pause_sel4.GetComponent<TextMeshProUGUI>().color;

            pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(psel1c.r, psel1c.g, psel1c.b, Mathf.Lerp(pause_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(psel2c.r, psel2c.g, psel2c.b, Mathf.Lerp(pause_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(psel3c.r, psel3c.g, psel3c.b, Mathf.Lerp(pause_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));
            pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(psel4c.r, psel4c.g, psel4c.b, Mathf.Lerp(pause_sel4.GetComponent<TextMeshProUGUI>().color.a, 0.0f, Time.unscaledDeltaTime * 7f));

            pause_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, 34.3f - 24, 0), ref velocity_sel1, 14 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel2.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -15.7f - 24, 0), ref velocity_sel2, 14 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel3.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -65.7f - 24, 0), ref velocity_sel3, 14 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            pause_sel4.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(pause_sel4.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -132.5f - 24, 0), ref velocity_sel4, 14 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);

            yield return null;
        }
        
        pause_sel1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 34.3f + 24, 0);
        pause_sel2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -15.7f + 24, 0);
        pause_sel3.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -65.7f + 24, 0);
        pause_sel4.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -132.5f + 24, 0);

        Color psel1cB = pause_sel1.GetComponent<TextMeshProUGUI>().color;
        Color psel2cB = pause_sel2.GetComponent<TextMeshProUGUI>().color;
        Color psel3cB = pause_sel3.GetComponent<TextMeshProUGUI>().color;
        Color psel4cB = pause_sel4.GetComponent<TextMeshProUGUI>().color;

        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(psel1cB.r, psel1cB.g, psel1cB.b, 0.0f);
        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(psel2cB.r, psel2cB.g, psel2cB.b, 0.0f);
        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(psel3cB.r, psel3cB.g, psel3cB.b, 0.0f);
        pause_sel4.GetComponent<TextMeshProUGUI>().color = new Color(psel4cB.r, psel4cB.g, psel4cB.b, 0.0f);
    }
    #endregion
}
