using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu_Manager : MonoBehaviour
{
    public static MainMenu_Manager instance;
    public GameObject canvas;
    PlayerController pc;

    public AudioSource audioSource;
    public AudioClip ui_move;
    public AudioClip ui_confirm;
    public AudioClip ui_zoom;

    GameObject menu_bg;
    GameObject menu_shadow;
    GameObject menu_drop_shadow;
    GameObject menu_copyright;
    GameObject menu_logo;
    GameObject menu_sel1;
    GameObject menu_sel2;
    GameObject menu_sel3;
    GameObject menu_dropout;

    GameObject menu_res;
    GameObject menu_res_text;
    GameObject menu_fscreen;
    GameObject menu_fscreen_text;
    GameObject menu_vol_master;
    GameObject menu_vol_master_text;
    GameObject menu_vol_master_slider;
    GameObject menu_vol_bgm;
    GameObject menu_vol_bgm_text;
    GameObject menu_vol_bgm_slider;
    GameObject menu_vol_sfx;
    GameObject menu_vol_sfx_text;
    GameObject menu_vol_sfx_slider;
    GameObject menu_apply;

    private Vector3 velocity_bg = Vector3.zero;
    private Vector3 velocity_drop_shadow = Vector3.zero;
    private Vector3 velocity_copyright = Vector3.zero;
    private Vector3 velocity_logo = Vector3.zero;
    private Vector3 velocity_sel1 = Vector3.zero;
    private Vector3 velocity_sel2 = Vector3.zero;
    private Vector3 velocity_sel3 = Vector3.zero;
    private Vector3 velocity_res = Vector3.zero;
    private Vector3 velocity_fscreen = Vector3.zero;
    private Vector3 velocity_vol_master = Vector3.zero;
    private Vector3 velocity_vol_bgm = Vector3.zero;
    private Vector3 velocity_vol_sfx = Vector3.zero;
    private Vector3 velocity_apply = Vector3.zero;

    Color bg_img_color;
    Color shadow_img_color;
    Color logo_img_color;
    Color sel1_img_color;
    Color sel2_img_color;
    Color sel3_img_color;
    Color dropout_img_color;

    Coroutine mn_drop_shadow = null;
    Coroutine mn_selections = null;

    public float interactDelay = 3.5f;
    public static bool menu_selection_confirm = false;
    public static int menu_position = 0;
    private bool menu_transition = false;
    private bool stage_transition = false;
    public static bool menu_submenu = false;
    
    private int restable_opt = 0;
    private int[,] restable = { { 640, 360 },
                                { 854, 480 },
                                { 1024, 576 },
                                { 1280, 720 },
                                { 1920, 1080 },
                                { 3840, 2160 }};

    private int fs_mode_opt = 0;
    private string[] fs_mode = { "Desligada", "Exclusiva", "Borderless" };
    private FullScreenMode[] fs_mode_out = { FullScreenMode.Windowed, FullScreenMode.ExclusiveFullScreen, FullScreenMode.FullScreenWindow };

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

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();

        var resolution_size = PlayerPrefs.GetInt("RESOLUTION_SIZE", 3);
        var fullscreen_mode = PlayerPrefs.GetInt("FULLSCREEN_MODE", 0);
        restable_opt = resolution_size;
        fs_mode_opt = fullscreen_mode;
        //Debug.Log(resolution_size);
        //Debug.Log(fullscreen_mode);
        Screen.SetResolution(restable[restable_opt, 0], restable[restable_opt, 1], fs_mode_out[fs_mode_opt]);


        canvas = GameObject.Find("TransitionCanvas");
        instance = this;

        #region Menu Background
        menu_bg = DialogSystem.getChildGameObject(gameObject, "MENU_Background");
        menu_bg.GetComponent<RectTransform>().localScale = new Vector3(3f, 3f, 1);

        bg_img_color = menu_bg.GetComponent<Image>().color;
        bg_img_color.a = 0f;

        StartCoroutine(Intro_BG_Zoom());
        #endregion

        #region Menu Layered Shadow
        menu_shadow = DialogSystem.getChildGameObject(gameObject, "MENU_LayeredShadow");

        shadow_img_color = menu_shadow.GetComponent<Image>().color;
        shadow_img_color.a = 0f;

        StartCoroutine(Intro_Shadow_Fade());
        #endregion

        #region Menu Drop Shadow
        menu_drop_shadow = DialogSystem.getChildGameObject(gameObject, "MENU_DropShadow");
        menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-520, -50, -1);

        mn_drop_shadow = StartCoroutine(Intro_Drop_Shadow_Move());
        #endregion

        #region Menu Copyright
        menu_copyright = DialogSystem.getChildGameObject(gameObject, "MENU_Copyright");
        menu_copyright.GetComponent<RectTransform>().anchoredPosition = new Vector3(540, -228, -1);

        StartCoroutine(Intro_Copyright_Move());
        #endregion

        #region Menu Logo
        menu_logo = DialogSystem.getChildGameObject(gameObject, "MENU_Logo");
        menu_logo.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1);

        logo_img_color = menu_logo.GetComponent<Image>().color;
        logo_img_color.a = 0f;
        menu_logo.GetComponent<Image>().color = logo_img_color;

        StartCoroutine(Intro_Logo_Fade());
        #endregion

        #region Menu Selections
        menu_sel1 = DialogSystem.getChildGameObject(gameObject, "MENU_Start");
        menu_sel2 = DialogSystem.getChildGameObject(gameObject, "MENU_Options");
        menu_sel3 = DialogSystem.getChildGameObject(gameObject, "MENU_Quit");

        menu_sel1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-600, -50, 0);
        menu_sel2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-600, -85, 0);
        menu_sel3.GetComponent<RectTransform>().anchoredPosition = new Vector3(-600, -120, 0);

        mn_selections = StartCoroutine(Intro_Selections_Move());
        #endregion

        #region Menu Dropout
        menu_dropout = DialogSystem.getChildGameObject(gameObject, "MENU_Dropout");

        dropout_img_color = menu_dropout.GetComponent<Image>().color;
        dropout_img_color.a = 0f;

        menu_dropout.GetComponent<Image>().color = dropout_img_color;
        menu_dropout.SetActive(false);
        #endregion

        #region -------- Options Menu

        menu_res = DialogSystem.getChildGameObject(gameObject, "MENU_Resolution");
        menu_res.GetComponent<RectTransform>().anchoredPosition = new Vector3(-700, -15, 0);

        menu_res_text = DialogSystem.getChildGameObject(gameObject, "MENU_ResolutionTable");

        menu_fscreen = DialogSystem.getChildGameObject(gameObject, "MENU_Fullscreen");
        menu_fscreen.GetComponent<RectTransform>().anchoredPosition = new Vector3(-700, -50, 0);

        menu_fscreen_text = DialogSystem.getChildGameObject(gameObject, "MENU_FullscreenMode");

        menu_vol_master = DialogSystem.getChildGameObject(gameObject, "MENU_Master");
        menu_vol_master.GetComponent<RectTransform>().anchoredPosition = new Vector3(-825, -85, 0);

        menu_vol_master_text = DialogSystem.getChildGameObject(gameObject, "MENU_MasterVolume");
        menu_vol_master_slider = DialogSystem.getChildGameObject(gameObject, "MENU_MasterSlider");

        menu_vol_bgm = DialogSystem.getChildGameObject(gameObject, "MENU_BGM");
        menu_vol_bgm.GetComponent<RectTransform>().anchoredPosition = new Vector3(-825, -120, 0);

        menu_vol_bgm_text = DialogSystem.getChildGameObject(gameObject, "MENU_BGMVolume");
        menu_vol_bgm_slider = DialogSystem.getChildGameObject(gameObject, "MENU_BGMSlider");

        menu_vol_sfx = DialogSystem.getChildGameObject(gameObject, "MENU_SFX");
        menu_vol_sfx.GetComponent<RectTransform>().anchoredPosition = new Vector3(-825, -155, 0);

        menu_vol_sfx_text = DialogSystem.getChildGameObject(gameObject, "MENU_SFXVolume");
        menu_vol_sfx_slider = DialogSystem.getChildGameObject(gameObject, "MENU_SFXSlider");

        menu_apply = DialogSystem.getChildGameObject(gameObject, "MENU_Apply");
        menu_apply.GetComponent<RectTransform>().anchoredPosition = new Vector3(-700, -190, 0);

        #endregion
    }

    void Update()
    {
        // Slightly offsets the background image according to the mouse position
        menu_bg.GetComponent<RectTransform>().anchoredPosition = new Vector2(
        (Mouse.current.position.x.ReadValue() / Screen.width) * 10,
        (Mouse.current.position.y.ReadValue() / Screen.height) * 10
        );

        if (pc.Movimento.NorteSul.WasPressedThisFrame())
        {
            if (interactDelay <= 0)
            {
                audioSource.PlayOneShot(ui_move, audioSource.volume);
            }
            if (menu_submenu == true)
            {
                menu_position -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                if (menu_position > 5) { menu_position = 0; }
                if (menu_position < 0) { menu_position = 5; }
            } else
            {
                menu_position -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                if (menu_position > 2) { menu_position = 0; }
                if (menu_position < 0) { menu_position = 2; }
            }
            
        }

        if (pc.Movimento.LesteOeste.WasPressedThisFrame())
        {
            if (menu_submenu == true)
            {
                audioSource.PlayOneShot(ui_move, audioSource.volume);
                float x = pc.Movimento.LesteOeste.ReadValue<float>();
                switch (menu_position)
                {
                    case 0:

                        if (x > 0)
                        {
                            restable_opt++;
                        } else if (x < 0)
                        {
                            restable_opt--;
                        }
                        
                        break;

                    case 1:

                        if (x > 0)
                        {
                            fs_mode_opt++;
                        }
                        else if (x < 0)
                        {
                            fs_mode_opt--;
                        }
                        break;

                    case 2:

                        if (x > 0)
                        {
                            menu_vol_master_slider.GetComponent<Slider>().value = menu_vol_master_slider.GetComponent<Slider>().value + 0.1f;
                        }
                        else if (x < 0)
                        {
                            menu_vol_master_slider.GetComponent<Slider>().value = menu_vol_master_slider.GetComponent<Slider>().value - 0.1f;
                        }
                        
                        break;

                    case 3:

                        if (x > 0)
                        {
                            menu_vol_bgm_slider.GetComponent<Slider>().value = menu_vol_bgm_slider.GetComponent<Slider>().value + 0.1f;
                        }
                        else if (x < 0)
                        {
                            menu_vol_bgm_slider.GetComponent<Slider>().value = menu_vol_bgm_slider.GetComponent<Slider>().value - 0.1f;
                        }

                        break;

                    case 4:

                        if (x > 0)
                        {
                            menu_vol_sfx_slider.GetComponent<Slider>().value = menu_vol_sfx_slider.GetComponent<Slider>().value + 0.1f;
                        }
                        else if (x < 0)
                        {
                            menu_vol_sfx_slider.GetComponent<Slider>().value = menu_vol_sfx_slider.GetComponent<Slider>().value - 0.1f;
                        }

                        break;
                }
            }

        }

        if (interactDelay <= 0)
        {
            if (pc.Movimento.Attack.WasPressedThisFrame())
            {
                if (menu_submenu == true)
                {
                    if (menu_position != 2 && menu_position != 3 && menu_position != 4) {
                        menu_selection_confirm = true;
                        audioSource.PlayOneShot(ui_confirm, audioSource.volume);
                    }
                } else
                {
                    menu_selection_confirm = true;
                    audioSource.PlayOneShot(ui_confirm, audioSource.volume);
                }
                
            }
        }
        else
        {
            interactDelay -= Time.deltaTime;
        }
        //print(interactDelay);


        if (Time.fixedTime > 3)
        {
            StopCoroutine(mn_drop_shadow);
            StopCoroutine(mn_selections);
            var smooth_time = 0.25f * 0.5f;

            if (menu_selection_confirm == false)
            {
                if (menu_submenu == true)
                {
                    ElementTranslate(menu_sel1, new Vector3(-600, -50, 0), ref velocity_sel1, smooth_time);
                    ElementTranslate(menu_sel2, new Vector3(-600, -85, 0), ref velocity_sel2, smooth_time);
                    ElementTranslate(menu_sel3, new Vector3(-600, -120, 0), ref velocity_sel3, smooth_time);

                    menu_sel1.GetComponent<EventTrigger>().enabled = false;
                    menu_sel2.GetComponent<EventTrigger>().enabled = false;
                    menu_sel3.GetComponent<EventTrigger>().enabled = false;

                    menu_res.GetComponent<EventTrigger>().enabled = true;
                    menu_fscreen.GetComponent<EventTrigger>().enabled = true;
                    menu_vol_master.GetComponent<EventTrigger>().enabled = true;
                    menu_vol_bgm.GetComponent<EventTrigger>().enabled = true;
                    menu_vol_sfx.GetComponent<EventTrigger>().enabled = true;
                    menu_apply.GetComponent<EventTrigger>().enabled = true;

                    switch (menu_position)
                    {
                        case 0:

                            #region Render behavior

                            ElementTranslate(menu_res, new Vector3(-275, -15, 0), ref velocity_res, smooth_time);
                            ElementTranslate(menu_fscreen, new Vector3(-300, -50, 0), ref velocity_fscreen, smooth_time);
                            ElementTranslate(menu_vol_master, new Vector3(-300, -85, 0), ref velocity_vol_master, smooth_time);
                            ElementTranslate(menu_vol_bgm, new Vector3(-300, -120, 0), ref velocity_vol_bgm, smooth_time);
                            ElementTranslate(menu_vol_sfx, new Vector3(-300, -155, 0), ref velocity_vol_sfx, smooth_time);
                            ElementTranslate(menu_apply, new Vector3(-300, -190, 0), ref velocity_apply, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -15, -1), ref velocity_drop_shadow, smooth_time);

                            #region Element colors
                            var mn_res = menu_res.GetComponent<TextMeshProUGUI>().color;
                            menu_res.GetComponent<TextMeshProUGUI>().color = new Color(mn_res.r, mn_res.g, mn_res.b, Mathf.Lerp(mn_res.a, 1.0f, Time.deltaTime * 4f));
                            var mn_rest = menu_res_text.GetComponent<TextMeshProUGUI>().color;
                            menu_res_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_rest.r, mn_rest.g, mn_rest.b, Mathf.Lerp(mn_rest.a, 1.0f, Time.deltaTime * 4f));

                            var mn_fs = menu_fscreen.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(mn_fs.r, mn_fs.g, mn_fs.b, Mathf.Lerp(mn_fs.a, 0.25f, Time.deltaTime * 4f));
                            var mn_fst = menu_fscreen_text.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_fst.r, mn_fst.g, mn_fst.b, Mathf.Lerp(mn_fst.a, 0.25f, Time.deltaTime * 4f));

                            var mn_vmas = menu_vol_master.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmas.r, mn_vmas.g, mn_vmas.b, Mathf.Lerp(mn_vmas.a, 0.25f, Time.deltaTime * 4f));
                            var mn_vmast = menu_vol_master_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmast.r, mn_vmast.g, mn_vmast.b, Mathf.Lerp(mn_vmast.a, 0.25f, Time.deltaTime * 4f));

                            var mn_vbgm = menu_vol_bgm.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgm.r, mn_vbgm.g, mn_vbgm.b, Mathf.Lerp(mn_vbgm.a, 0.25f, Time.deltaTime * 4f));
                            var mn_vbgmt = menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgmt.r, mn_vbgmt.g, mn_vbgmt.b, Mathf.Lerp(mn_vbgmt.a, 0.25f, Time.deltaTime * 4f));

                            var mn_vsfx = menu_vol_sfx.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfx.r, mn_vsfx.g, mn_vsfx.b, Mathf.Lerp(mn_vsfx.a, 0.25f, Time.deltaTime * 4f));
                            var mn_vsfxt = menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfxt.r, mn_vsfxt.g, mn_vsfxt.b, Mathf.Lerp(mn_vsfxt.a, 0.25f, Time.deltaTime * 4f));

                            var mn_app = menu_apply.GetComponent<TextMeshProUGUI>().color;
                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(mn_app.r, mn_app.g, mn_app.b, Mathf.Lerp(mn_app.a, 0.25f, Time.deltaTime * 4f));
                            #endregion

                            #endregion
                            break;

                        case 1:

                            #region Render behavior

                            ElementTranslate(menu_res, new Vector3(-300, -15, 0), ref velocity_res, smooth_time);
                            ElementTranslate(menu_fscreen, new Vector3(-275, -50, 0), ref velocity_fscreen, smooth_time);
                            ElementTranslate(menu_vol_master, new Vector3(-300, -85, 0), ref velocity_vol_master, smooth_time);
                            ElementTranslate(menu_vol_bgm, new Vector3(-300, -120, 0), ref velocity_vol_bgm, smooth_time);
                            ElementTranslate(menu_vol_sfx, new Vector3(-300, -155, 0), ref velocity_vol_sfx, smooth_time);
                            ElementTranslate(menu_apply, new Vector3(-300, -190, 0), ref velocity_apply, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -50, -1), ref velocity_drop_shadow, smooth_time);

                            #region Element colors
                            mn_res = menu_res.GetComponent<TextMeshProUGUI>().color;
                            menu_res.GetComponent<TextMeshProUGUI>().color = new Color(mn_res.r, mn_res.g, mn_res.b, Mathf.Lerp(mn_res.a, 0.25f, Time.deltaTime * 4f));
                            mn_rest = menu_res_text.GetComponent<TextMeshProUGUI>().color;
                            menu_res_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_rest.r, mn_rest.g, mn_rest.b, Mathf.Lerp(mn_rest.a, 0.25f, Time.deltaTime * 4f));

                            mn_fs = menu_fscreen.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(mn_fs.r, mn_fs.g, mn_fs.b, Mathf.Lerp(mn_fs.a, 1.0f, Time.deltaTime * 4f));
                            mn_fst = menu_fscreen_text.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_fst.r, mn_fst.g, mn_fst.b, Mathf.Lerp(mn_fst.a, 1.0f, Time.deltaTime * 4f));

                            mn_vmas = menu_vol_master.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmas.r, mn_vmas.g, mn_vmas.b, Mathf.Lerp(mn_vmas.a, 0.25f, Time.deltaTime * 4f));
                            mn_vmast = menu_vol_master_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmast.r, mn_vmast.g, mn_vmast.b, Mathf.Lerp(mn_vmast.a, 0.25f, Time.deltaTime * 4f));

                            mn_vbgm = menu_vol_bgm.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgm.r, mn_vbgm.g, mn_vbgm.b, Mathf.Lerp(mn_vbgm.a, 0.25f, Time.deltaTime * 4f));
                            mn_vbgmt = menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgmt.r, mn_vbgmt.g, mn_vbgmt.b, Mathf.Lerp(mn_vbgmt.a, 0.25f, Time.deltaTime * 4f));

                            mn_vsfx = menu_vol_sfx.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfx.r, mn_vsfx.g, mn_vsfx.b, Mathf.Lerp(mn_vsfx.a, 0.25f, Time.deltaTime * 4f));
                            mn_vsfxt = menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfxt.r, mn_vsfxt.g, mn_vsfxt.b, Mathf.Lerp(mn_vsfxt.a, 0.25f, Time.deltaTime * 4f));

                            mn_app = menu_apply.GetComponent<TextMeshProUGUI>().color;
                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(mn_app.r, mn_app.g, mn_app.b, Mathf.Lerp(mn_app.a, 0.25f, Time.deltaTime * 4f));
                            #endregion
                            #endregion
                            break;

                        case 2:

                            #region Render behavior

                            ElementTranslate(menu_res, new Vector3(-300, -15, 0), ref velocity_res, smooth_time);
                            ElementTranslate(menu_fscreen, new Vector3(-300, -50, 0), ref velocity_fscreen, smooth_time);
                            ElementTranslate(menu_vol_master, new Vector3(-275, -85, 0), ref velocity_vol_master, smooth_time);
                            ElementTranslate(menu_vol_bgm, new Vector3(-300, -120, 0), ref velocity_vol_bgm, smooth_time);
                            ElementTranslate(menu_vol_sfx, new Vector3(-300, -155, 0), ref velocity_vol_sfx, smooth_time);
                            ElementTranslate(menu_apply, new Vector3(-300, -190, 0), ref velocity_apply, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -85, -1), ref velocity_drop_shadow, smooth_time);

                            #region Element colors
                            mn_res = menu_res.GetComponent<TextMeshProUGUI>().color;
                            menu_res.GetComponent<TextMeshProUGUI>().color = new Color(mn_res.r, mn_res.g, mn_res.b, Mathf.Lerp(mn_res.a, 0.25f, Time.deltaTime * 4f));
                            mn_rest = menu_res_text.GetComponent<TextMeshProUGUI>().color;
                            menu_res_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_rest.r, mn_rest.g, mn_rest.b, Mathf.Lerp(mn_rest.a, 0.25f, Time.deltaTime * 4f));

                            mn_fs = menu_fscreen.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(mn_fs.r, mn_fs.g, mn_fs.b, Mathf.Lerp(mn_fs.a, 0.25f, Time.deltaTime * 4f));
                            mn_fst = menu_fscreen_text.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_fst.r, mn_fst.g, mn_fst.b, Mathf.Lerp(mn_fst.a, 0.25f, Time.deltaTime * 4f));

                            mn_vmas = menu_vol_master.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmas.r, mn_vmas.g, mn_vmas.b, Mathf.Lerp(mn_vmas.a, 1.0f, Time.deltaTime * 4f));
                            mn_vmast = menu_vol_master_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmast.r, mn_vmast.g, mn_vmast.b, Mathf.Lerp(mn_vmast.a, 1.0f, Time.deltaTime * 4f));

                            mn_vbgm = menu_vol_bgm.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgm.r, mn_vbgm.g, mn_vbgm.b, Mathf.Lerp(mn_vbgm.a, 0.25f, Time.deltaTime * 4f));
                            mn_vbgmt = menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgmt.r, mn_vbgmt.g, mn_vbgmt.b, Mathf.Lerp(mn_vbgmt.a, 0.25f, Time.deltaTime * 4f));

                            mn_vsfx = menu_vol_sfx.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfx.r, mn_vsfx.g, mn_vsfx.b, Mathf.Lerp(mn_vsfx.a, 0.25f, Time.deltaTime * 4f));
                            mn_vsfxt = menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfxt.r, mn_vsfxt.g, mn_vsfxt.b, Mathf.Lerp(mn_vsfxt.a, 0.25f, Time.deltaTime * 4f));

                            mn_app = menu_apply.GetComponent<TextMeshProUGUI>().color;
                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(mn_app.r, mn_app.g, mn_app.b, Mathf.Lerp(mn_app.a, 0.25f, Time.deltaTime * 4f));
                            #endregion
                            #endregion
                            break;

                        case 3:

                            #region Render behavior
                            ElementTranslate(menu_res, new Vector3(-300, -15, 0), ref velocity_res, smooth_time);
                            ElementTranslate(menu_fscreen, new Vector3(-300, -50, 0), ref velocity_fscreen, smooth_time);
                            ElementTranslate(menu_vol_master, new Vector3(-300, -85, 0), ref velocity_vol_master, smooth_time);
                            ElementTranslate(menu_vol_bgm, new Vector3(-275, -120, 0), ref velocity_vol_bgm, smooth_time);
                            ElementTranslate(menu_vol_sfx, new Vector3(-300, -155, 0), ref velocity_vol_sfx, smooth_time);
                            ElementTranslate(menu_apply, new Vector3(-300, -190, 0), ref velocity_apply, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -120, -1), ref velocity_drop_shadow, smooth_time);

                            #region Element colors
                            mn_res = menu_res.GetComponent<TextMeshProUGUI>().color;
                            menu_res.GetComponent<TextMeshProUGUI>().color = new Color(mn_res.r, mn_res.g, mn_res.b, Mathf.Lerp(mn_res.a, 0.25f, Time.deltaTime * 4f));
                            mn_rest = menu_res_text.GetComponent<TextMeshProUGUI>().color;
                            menu_res_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_rest.r, mn_rest.g, mn_rest.b, Mathf.Lerp(mn_rest.a, 0.25f, Time.deltaTime * 4f));

                            mn_fs = menu_fscreen.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(mn_fs.r, mn_fs.g, mn_fs.b, Mathf.Lerp(mn_fs.a, 0.25f, Time.deltaTime * 4f));
                            mn_fst = menu_fscreen_text.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_fst.r, mn_fst.g, mn_fst.b, Mathf.Lerp(mn_fst.a, 0.25f, Time.deltaTime * 4f));

                            mn_vmas = menu_vol_master.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmas.r, mn_vmas.g, mn_vmas.b, Mathf.Lerp(mn_vmas.a, 0.25f, Time.deltaTime * 4f));
                            mn_vmast = menu_vol_master_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmast.r, mn_vmast.g, mn_vmast.b, Mathf.Lerp(mn_vmast.a, 0.25f, Time.deltaTime * 4f));

                            mn_vbgm = menu_vol_bgm.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgm.r, mn_vbgm.g, mn_vbgm.b, Mathf.Lerp(mn_vbgm.a, 1.0f, Time.deltaTime * 4f));
                            mn_vbgmt = menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgmt.r, mn_vbgmt.g, mn_vbgmt.b, Mathf.Lerp(mn_vbgmt.a, 1.0f, Time.deltaTime * 4f));

                            mn_vsfx = menu_vol_sfx.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfx.r, mn_vsfx.g, mn_vsfx.b, Mathf.Lerp(mn_vsfx.a, 0.25f, Time.deltaTime * 4f));
                            mn_vsfxt = menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfxt.r, mn_vsfxt.g, mn_vsfxt.b, Mathf.Lerp(mn_vsfxt.a, 0.25f, Time.deltaTime * 4f));

                            mn_app = menu_apply.GetComponent<TextMeshProUGUI>().color;
                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(mn_app.r, mn_app.g, mn_app.b, Mathf.Lerp(mn_app.a, 0.25f, Time.deltaTime * 4f));
                            #endregion
                            #endregion
                            break;

                        case 4:

                            #region Render behavior

                            ElementTranslate(menu_res, new Vector3(-300, -15, 0), ref velocity_res, smooth_time);
                            ElementTranslate(menu_fscreen, new Vector3(-300, -50, 0), ref velocity_fscreen, smooth_time);
                            ElementTranslate(menu_vol_master, new Vector3(-300, -85, 0), ref velocity_vol_master, smooth_time);
                            ElementTranslate(menu_vol_bgm, new Vector3(-300, -120, 0), ref velocity_vol_bgm, smooth_time);
                            ElementTranslate(menu_vol_sfx, new Vector3(-275, -155, 0), ref velocity_vol_sfx, smooth_time);
                            ElementTranslate(menu_apply, new Vector3(-300, -190, 0), ref velocity_apply, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -155, -1), ref velocity_drop_shadow, smooth_time);

                            #region Element colors
                            mn_res = menu_res.GetComponent<TextMeshProUGUI>().color;
                            menu_res.GetComponent<TextMeshProUGUI>().color = new Color(mn_res.r, mn_res.g, mn_res.b, Mathf.Lerp(mn_res.a, 0.25f, Time.deltaTime * 4f));
                            mn_rest = menu_res_text.GetComponent<TextMeshProUGUI>().color;
                            menu_res_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_rest.r, mn_rest.g, mn_rest.b, Mathf.Lerp(mn_rest.a, 0.25f, Time.deltaTime * 4f));

                            mn_fs = menu_fscreen.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(mn_fs.r, mn_fs.g, mn_fs.b, Mathf.Lerp(mn_fs.a, 0.25f, Time.deltaTime * 4f));
                            mn_fst = menu_fscreen_text.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_fst.r, mn_fst.g, mn_fst.b, Mathf.Lerp(mn_fst.a, 0.25f, Time.deltaTime * 4f));

                            mn_vmas = menu_vol_master.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmas.r, mn_vmas.g, mn_vmas.b, Mathf.Lerp(mn_vmas.a, 0.25f, Time.deltaTime * 4f));
                            mn_vmast = menu_vol_master_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmast.r, mn_vmast.g, mn_vmast.b, Mathf.Lerp(mn_vmast.a, 0.25f, Time.deltaTime * 4f));

                            mn_vbgm = menu_vol_bgm.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgm.r, mn_vbgm.g, mn_vbgm.b, Mathf.Lerp(mn_vbgm.a, 0.25f, Time.deltaTime * 4f));
                            mn_vbgmt = menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgmt.r, mn_vbgmt.g, mn_vbgmt.b, Mathf.Lerp(mn_vbgmt.a, 0.25f, Time.deltaTime * 4f));

                            mn_vsfx = menu_vol_sfx.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfx.r, mn_vsfx.g, mn_vsfx.b, Mathf.Lerp(mn_vsfx.a, 1.0f, Time.deltaTime * 4f));
                            mn_vsfxt = menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfxt.r, mn_vsfxt.g, mn_vsfxt.b, Mathf.Lerp(mn_vsfxt.a, 1.0f, Time.deltaTime * 4f));

                            mn_app = menu_apply.GetComponent<TextMeshProUGUI>().color;
                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(mn_app.r, mn_app.g, mn_app.b, Mathf.Lerp(mn_app.a, 0.25f, Time.deltaTime * 4f));
                            #endregion
                            #endregion
                            break;

                        case 5:

                            #region Render behavior

                            ElementTranslate(menu_res, new Vector3(-300, -15, 0), ref velocity_res, smooth_time);
                            ElementTranslate(menu_fscreen, new Vector3(-300, -50, 0), ref velocity_fscreen, smooth_time);
                            ElementTranslate(menu_vol_master, new Vector3(-300, -85, 0), ref velocity_vol_master, smooth_time);
                            ElementTranslate(menu_vol_bgm, new Vector3(-300, -120, 0), ref velocity_vol_bgm, smooth_time);
                            ElementTranslate(menu_vol_sfx, new Vector3(-300, -155, 0), ref velocity_vol_sfx, smooth_time);
                            ElementTranslate(menu_apply, new Vector3(-275, -190, 0), ref velocity_apply, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -190, -1), ref velocity_drop_shadow, smooth_time);

                            #region Element colors
                            mn_res = menu_res.GetComponent<TextMeshProUGUI>().color;
                            menu_res.GetComponent<TextMeshProUGUI>().color = new Color(mn_res.r, mn_res.g, mn_res.b, Mathf.Lerp(mn_res.a, 0.25f, Time.deltaTime * 4f));
                            mn_rest = menu_res_text.GetComponent<TextMeshProUGUI>().color;
                            menu_res_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_rest.r, mn_rest.g, mn_rest.b, Mathf.Lerp(mn_rest.a, 0.25f, Time.deltaTime * 4f));

                            mn_fs = menu_fscreen.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen.GetComponent<TextMeshProUGUI>().color = new Color(mn_fs.r, mn_fs.g, mn_fs.b, Mathf.Lerp(mn_fs.a, 0.25f, Time.deltaTime * 4f));
                            mn_fst = menu_fscreen_text.GetComponent<TextMeshProUGUI>().color;
                            menu_fscreen_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_fst.r, mn_fst.g, mn_fst.b, Mathf.Lerp(mn_fst.a, 0.25f, Time.deltaTime * 4f));

                            mn_vmas = menu_vol_master.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmas.r, mn_vmas.g, mn_vmas.b, Mathf.Lerp(mn_vmas.a, 0.25f, Time.deltaTime * 4f));
                            mn_vmast = menu_vol_master_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_master_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vmast.r, mn_vmast.g, mn_vmast.b, Mathf.Lerp(mn_vmast.a, 0.25f, Time.deltaTime * 4f));

                            mn_vbgm = menu_vol_bgm.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgm.r, mn_vbgm.g, mn_vbgm.b, Mathf.Lerp(mn_vbgm.a, 0.25f, Time.deltaTime * 4f));
                            mn_vbgmt = menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vbgmt.r, mn_vbgmt.g, mn_vbgmt.b, Mathf.Lerp(mn_vbgmt.a, 0.25f, Time.deltaTime * 4f));

                            mn_vsfx = menu_vol_sfx.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfx.r, mn_vsfx.g, mn_vsfx.b, Mathf.Lerp(mn_vsfx.a, 0.25f, Time.deltaTime * 4f));
                            mn_vsfxt = menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color;
                            menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().color = new Color(mn_vsfxt.r, mn_vsfxt.g, mn_vsfxt.b, Mathf.Lerp(mn_vsfxt.a, 0.25f, Time.deltaTime * 4f));

                            mn_app = menu_apply.GetComponent<TextMeshProUGUI>().color;
                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(mn_app.r, mn_app.g, mn_app.b, Mathf.Lerp(mn_app.a, 1.0f, Time.deltaTime * 4f));
                            #endregion
                            #endregion
                            break;
                    }

                } else
                {
                    ElementTranslate(menu_res, new Vector3(-700, -15, 0), ref velocity_res, smooth_time);
                    ElementTranslate(menu_fscreen, new Vector3(-700, -50, 0), ref velocity_fscreen, smooth_time);
                    ElementTranslate(menu_vol_master, new Vector3(-825, -85, 0), ref velocity_vol_master, smooth_time);
                    ElementTranslate(menu_vol_bgm, new Vector3(-825, -120, 0), ref velocity_vol_bgm, smooth_time);
                    ElementTranslate(menu_vol_sfx, new Vector3(-825, -155, 0), ref velocity_vol_sfx, smooth_time);
                    ElementTranslate(menu_apply, new Vector3(-700, -190, 0), ref velocity_apply, smooth_time);

                    menu_sel1.GetComponent<EventTrigger>().enabled = true;
                    menu_sel2.GetComponent<EventTrigger>().enabled = true;
                    menu_sel3.GetComponent<EventTrigger>().enabled = true;

                    menu_res.GetComponent<EventTrigger>().enabled = false;
                    menu_fscreen.GetComponent<EventTrigger>().enabled = false;
                    menu_vol_master.GetComponent<EventTrigger>().enabled = false;
                    menu_vol_bgm.GetComponent<EventTrigger>().enabled = false;
                    menu_vol_sfx.GetComponent<EventTrigger>().enabled = false;
                    menu_apply.GetComponent<EventTrigger>().enabled = false;

                    switch (menu_position)
                    {
                        case 0:

                            #region Render behavior

                            ElementTranslate(menu_sel1, new Vector3(-275, -50, 0), ref velocity_sel1, smooth_time);
                            ElementTranslate(menu_sel2, new Vector3(-300, -85, 0), ref velocity_sel2, smooth_time);
                            ElementTranslate(menu_sel3, new Vector3(-300, -120, 0), ref velocity_sel3, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -50, -1), ref velocity_drop_shadow, smooth_time);

                            var mns1 = menu_sel1.GetComponent<TextMeshProUGUI>().color;
                            menu_sel1.GetComponent<TextMeshProUGUI>().color = new Color(mns1.r, mns1.g, mns1.b, Mathf.Lerp(mns1.a, 1.0f, Time.deltaTime * 4f));
                            var mns2 = menu_sel2.GetComponent<TextMeshProUGUI>().color;
                            menu_sel2.GetComponent<TextMeshProUGUI>().color = new Color(mns2.r, mns2.g, mns2.b, Mathf.Lerp(mns2.a, 0.25f, Time.deltaTime * 4f));
                            var mns3 = menu_sel3.GetComponent<TextMeshProUGUI>().color;
                            menu_sel3.GetComponent<TextMeshProUGUI>().color = new Color(mns3.r, mns3.g, mns3.b, Mathf.Lerp(mns3.a, 0.25f, Time.deltaTime * 4f));
                            #endregion
                            break;


                        case 1:

                            #region Render behavior

                            ElementTranslate(menu_sel1, new Vector3(-300, -50, 0), ref velocity_sel1, smooth_time);
                            ElementTranslate(menu_sel2, new Vector3(-275, -85, 0), ref velocity_sel2, smooth_time);
                            ElementTranslate(menu_sel3, new Vector3(-300, -120, 0), ref velocity_sel3, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -85, -1), ref velocity_drop_shadow, smooth_time);

                            mns1 = menu_sel1.GetComponent<TextMeshProUGUI>().color;
                            menu_sel1.GetComponent<TextMeshProUGUI>().color = new Color(mns1.r, mns1.g, mns1.b, Mathf.Lerp(mns1.a, 0.25f, Time.deltaTime * 4f));
                            mns2 = menu_sel2.GetComponent<TextMeshProUGUI>().color;
                            menu_sel2.GetComponent<TextMeshProUGUI>().color = new Color(mns2.r, mns2.g, mns2.b, Mathf.Lerp(mns2.a, 1.00f, Time.deltaTime * 4f));
                            mns3 = menu_sel3.GetComponent<TextMeshProUGUI>().color;
                            menu_sel3.GetComponent<TextMeshProUGUI>().color = new Color(mns3.r, mns3.g, mns3.b, Mathf.Lerp(mns3.a, 0.25f, Time.deltaTime * 4f));
                            #endregion
                            break;

                        case 2:

                            #region Render behavior

                            ElementTranslate(menu_sel1, new Vector3(-300, -50, 0), ref velocity_sel1, smooth_time);
                            ElementTranslate(menu_sel2, new Vector3(-300, -85, 0), ref velocity_sel2, smooth_time);
                            ElementTranslate(menu_sel3, new Vector3(-275, -120, 0), ref velocity_sel3, smooth_time);

                            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -120, -1), ref velocity_drop_shadow, smooth_time);

                            mns1 = menu_sel1.GetComponent<TextMeshProUGUI>().color;
                            menu_sel1.GetComponent<TextMeshProUGUI>().color = new Color(mns1.r, mns1.g, mns1.b, Mathf.Lerp(mns1.a, 0.25f, Time.deltaTime * 4f));
                            mns2 = menu_sel2.GetComponent<TextMeshProUGUI>().color;
                            menu_sel2.GetComponent<TextMeshProUGUI>().color = new Color(mns2.r, mns2.g, mns2.b, Mathf.Lerp(mns2.a, 0.25f, Time.deltaTime * 4f));
                            mns3 = menu_sel3.GetComponent<TextMeshProUGUI>().color;
                            menu_sel3.GetComponent<TextMeshProUGUI>().color = new Color(mns3.r, mns3.g, mns3.b, Mathf.Lerp(mns3.a, 1.00f, Time.deltaTime * 4f));
                            #endregion
                            break;


                    }
                }
                
            } else
            {
                if (menu_submenu == true)
                {
                    switch (menu_position)
                    {
                        case 0:

                            restable_opt++;
                            menu_selection_confirm = false;
                            break;

                        case 1:

                            fs_mode_opt++;
                            menu_selection_confirm = false;
                            break;

                        case 5:
                            
                            Screen.SetResolution(restable[restable_opt, 0], restable[restable_opt, 1], fs_mode_out[fs_mode_opt]);

                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                            menu_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                            menu_position = 0;
                            menu_submenu = false;
                            menu_selection_confirm = false;
                            PlayerPrefs.SetInt("RESOLUTION_SIZE", restable_opt);
                            PlayerPrefs.SetInt("FULLSCREEN_MODE", fs_mode_opt);
                            break;
                    }
                } else
                {
                    switch (menu_position)
                    {
                        case 0:

                            if (menu_transition == false)
                            {
                                audioSource.PlayOneShot(ui_zoom, audioSource.volume);
                                StartCoroutine(Transition_Start_Game());
                                menu_dropout.SetActive(true);
                                menu_transition = true;
                            }
                            menu_sel1.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(menu_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-170, -50, 0), ref velocity_sel1, smooth_time + 0.5f);

                            menu_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);

                            if (stage_transition == false)
                            {
                                canvas.GetComponent<Transition_Manager>().TransitionToScene("StartVoid");
                                stage_transition = true;
                            }
                            break;

                        case 1:

                            menu_apply.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                            menu_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                            menu_position = 0;
                            menu_submenu = true;
                            menu_selection_confirm = false;
                            break;

                        case 2:

                            Application.Quit();
                            break;
                    }
                }
            }
            
        }

        menu_vol_master_text.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(menu_vol_master_slider.GetComponent<Slider>().value * 100) + "%";
        menu_vol_bgm_text.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(menu_vol_bgm_slider.GetComponent<Slider>().value * 100) + "%";
        menu_vol_sfx_text.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(menu_vol_sfx_slider.GetComponent<Slider>().value * 100) + "%";

        if (restable_opt > restable.Length/2 - 1) { restable_opt = 0; }
        if (restable_opt < 0) { restable_opt = restable.Length / 2 - 1; }

        menu_res_text.GetComponent<TextMeshProUGUI>().text = restable[restable_opt, 0] + "x" + restable[restable_opt, 1];

        if (fs_mode_opt > 2) { fs_mode_opt = 0; }
        if (fs_mode_opt < 0) { fs_mode_opt = 2; }

        menu_fscreen_text.GetComponent<TextMeshProUGUI>().text = fs_mode[fs_mode_opt];

        if (menu_submenu == true)
        {
            if (menu_position > 5) { menu_position = 0; }
            if (menu_position < 0) { menu_position = 5; }
        } else
        {
            if (menu_position > 2) { menu_position = 0; }
            if (menu_position < 0) { menu_position = 2; }
        }

        //print("length is: " + restable.Length);


    }

    #region Transition animations
    private IEnumerator Intro_BG_Zoom()
    {
        for (int i = 0; i < 60 * 32; i++)
        {
            bg_img_color.a = Mathf.Lerp(bg_img_color.a, 1, Time.deltaTime * 0.4f);
            menu_bg.GetComponent<Image>().color = bg_img_color;
            menu_bg.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(menu_bg.GetComponent<RectTransform>().localScale, new Vector3(1.1f, 1.1f, 1), ref velocity_bg, 2);
            yield return null;
        }
    }

    private IEnumerator Intro_Shadow_Fade()
    {
        for (int i = 0; i < 60 * 32; i++)
        {
            shadow_img_color.a = Mathf.Lerp(shadow_img_color.a, 1, Time.deltaTime * 0.4f);
            menu_shadow.GetComponent<Image>().color = shadow_img_color;
            yield return null;
        }
    }

    private IEnumerator Intro_Drop_Shadow_Move()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 60 * 32; i++)
        {
            menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-165, -50, 1), ref velocity_drop_shadow, 0.7f);
            yield return null;
        }
    }

    private IEnumerator Intro_Copyright_Move()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 60 * 32; i++)
        {
            menu_copyright.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(menu_copyright.GetComponent<RectTransform>().anchoredPosition, new Vector3(210, -228, 1), ref velocity_copyright, 0.4f);
            yield return null;
        }
    }

    private IEnumerator Intro_Logo_Fade()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 60 * 32; i++)
        {
            logo_img_color.a = Mathf.Lerp(logo_img_color.a, 1, Time.deltaTime * 1f);
            menu_logo.GetComponent<Image>().color = logo_img_color;
            menu_logo.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(menu_logo.GetComponent<RectTransform>().localScale, new Vector3(1f, 1f, 1), ref velocity_logo, 0.65f);
            yield return null;
        }
    }

    private IEnumerator Intro_Selections_Move()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 60 * 32; i++)
        {
            menu_sel1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(menu_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -50, 0), ref velocity_sel1, 0.25f * 1.25f);
            menu_sel2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(menu_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -85, 0), ref velocity_sel2, 0.325f * 1.25f);
            menu_sel3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(menu_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -120, 0), ref velocity_sel3, 0.4f * 1.25f);
            yield return null;
        }
    }
    #endregion

    // ---- Post-initialization
    private IEnumerator Transition_Start_Game()
    {
        for (int i = 0; i < 60 * 32; i++)
        {
            audioSource.volume = audioSource.volume - 0.005f;
            dropout_img_color.a = Mathf.Lerp(dropout_img_color.a, 1, Time.deltaTime * 2.5f);
            menu_dropout.GetComponent<Image>().color = dropout_img_color;
            menu_bg.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(menu_bg.GetComponent<RectTransform>().localScale, new Vector3(4f, 4f, 1), ref velocity_bg, 1);
            yield return null;
        }
    }

    private void ElementTranslate(GameObject gm, Vector3 pos, ref Vector3 vel, float time)
    {
        gm.GetComponent<RectTransform>().anchoredPosition =
                                Vector3.SmoothDamp(gm.GetComponent<RectTransform>().anchoredPosition, pos, ref vel, time);
    }

   /* private void ElementTranslateF(GameObject gm, float pos1, float pos2, float vel1, float vel2, float time)
    {
        var gmpos = gm.GetComponent<RectTransform>().anchoredPosition;
        gmpos.x = Mathf.SmoothDamp(gmpos.x, pos1, ref vel1, time);
        gmpos.y = Mathf.SmoothDamp(gmpos.y, pos2, ref vel2, time);
        gm.GetComponent<RectTransform>().anchoredPosition = gmpos;
    }

    private Vector3 ElementTranslateA(GameObject gm, Vector3 pos, Vector3 vel, float time)
    {
        return Vector3.SmoothDamp(gm.GetComponent<RectTransform>().anchoredPosition, new Vector3(pos.x, pos.y, pos.z), ref vel, time);
    }*/



}