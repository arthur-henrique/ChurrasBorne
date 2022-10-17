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
    GameObject menu_bg;
    GameObject menu_shadow;
    GameObject menu_drop_shadow;
    GameObject menu_copyright;
    GameObject menu_logo;
    GameObject menu_sel1;
    GameObject menu_sel2;
    GameObject menu_sel3;
    GameObject menu_dropout;

    private Vector3 velocity_bg = Vector3.zero;
    private Vector3 velocity_drop_shadow = Vector3.zero;
    private Vector3 velocity_copyright = Vector3.zero;
    private Vector3 velocity_logo = Vector3.zero;
    private Vector3 velocity_sel1 = Vector3.zero;
    private Vector3 velocity_sel2 = Vector3.zero;
    private Vector3 velocity_sel3 = Vector3.zero;

    Color bg_img_color;
    Color shadow_img_color;
    Color logo_img_color;
    Color sel1_img_color;
    Color sel2_img_color;
    Color sel3_img_color;
    Color dropout_img_color;

    Coroutine mn_drop_shadow = null;
    Coroutine mn_selections = null;

    public static bool menu_selection_confirm = false;
    public static int menu_position = 0;
    bool menu_transition = false;

    void Start()
    {
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

    }

    void Update()
    {
        // Slightly offsets the background image according to the mouse position
        menu_bg.GetComponent<RectTransform>().anchoredPosition = new Vector2(
        (Mouse.current.position.x.ReadValue() / Screen.width) * 10,
        (Mouse.current.position.y.ReadValue() / Screen.height) * 10
        );

        if (Time.fixedTime > 3)
        {
            StopCoroutine(mn_drop_shadow);
            StopCoroutine(mn_selections);
            var smooth_time = 0.25f * 0.5f;

            if (menu_selection_confirm == false)
            {
                switch (menu_position)
                {
                    case 0:
                        menu_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-275, -50, 0), ref velocity_sel1, smooth_time);
                        menu_sel2.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -85, 0), ref velocity_sel2, smooth_time);
                        menu_sel3.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -120, 0), ref velocity_sel3, smooth_time);

                        menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-145, -50, -1), ref velocity_drop_shadow, smooth_time);

                        break;

                    case 1:
                        menu_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -50, 0), ref velocity_sel1, smooth_time);
                        menu_sel2.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-275, -85, 0), ref velocity_sel2, smooth_time);
                        menu_sel3.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -120, 0), ref velocity_sel3, smooth_time);

                        menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-145, -85, -1), ref velocity_drop_shadow, smooth_time);
                        break;

                    case 2:
                        menu_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -50, 0), ref velocity_sel1, smooth_time);
                        menu_sel2.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-300, -85, 0), ref velocity_sel2, smooth_time);
                        menu_sel3.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-275, -120, 0), ref velocity_sel3, smooth_time);

                        menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-145, -120, -1), ref velocity_drop_shadow, smooth_time);
                        break;
                }
            } else
            {
                switch (menu_position)
                {
                    case 0:

                        if (menu_transition == false)
                        {
                            StartCoroutine(Transition_Start_Game());
                            menu_dropout.SetActive(true);
                            menu_transition = true;
                        }
                        menu_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(menu_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-170, -50, 0), ref velocity_sel1, smooth_time + 0.5f);

                        menu_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                        break;
                }
            }
            
        }

    }

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
                Vector3.SmoothDamp(menu_drop_shadow.GetComponent<RectTransform>().anchoredPosition, new Vector3(-145, -50, 1), ref velocity_drop_shadow, 0.7f);
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

    // ---- Post-initialization
    private IEnumerator Transition_Start_Game()
    {
        for (int i = 0; i < 60 * 32; i++)
        {
            dropout_img_color.a = Mathf.Lerp(dropout_img_color.a, 1, Time.deltaTime * 2.5f);
            menu_dropout.GetComponent<Image>().color = dropout_img_color;
            menu_bg.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(menu_bg.GetComponent<RectTransform>().localScale, new Vector3(4f, 4f, 1), ref velocity_bg, 1);
            yield return null;
        }
    }



}