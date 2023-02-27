﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Transition_Manager : MonoBehaviour
{
    GameObject curtain_left_1;
    GameObject curtain_left_2;
    GameObject curtain_left_3;
    GameObject curtain_right_1;
    GameObject curtain_right_2;
    GameObject curtain_right_3;

    private Vector3 velocity_left1 = Vector3.zero;
    private Vector3 velocity_left2 = Vector3.zero;
    private Vector3 velocity_left3 = Vector3.zero;
    private Vector3 velocity_right1 = Vector3.zero;
    private Vector3 velocity_right2 = Vector3.zero;
    private Vector3 velocity_right3 = Vector3.zero;
    
    public static Vector2 fase1_spawn = new Vector2(-6.756674f, 3.171088f);
    public static Vector2 fase2_spawn = new Vector2(0f, 0f);
    public static Vector2 fase3_spawn = new Vector2(0f, 0f);
    public static Vector2 fase4_spawn = new Vector2(0f, 0f);

    string scene_detect;

    GameObject scene_text_display;

    Coroutine cr_transition_handle;
    Coroutine cr_transition_restart_handle;

    bool stop_descend = false;

    // Start is called before the first frame update
    void Start()
    {
        cr_transition_handle = StartCoroutine(VoidTask());
        cr_transition_restart_handle = StartCoroutine(VoidTask());

        DontDestroyOnLoad(this);

        scene_text_display = DialogSystem.getChildGameObject(gameObject, "Scene_Name_Display");
        scene_text_display.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        curtain_left_1 = DialogSystem.getChildGameObject(gameObject, "Curtain_Left_1");
        curtain_left_2 = DialogSystem.getChildGameObject(gameObject, "Curtain_Left_2");
        curtain_left_3 = DialogSystem.getChildGameObject(gameObject, "Curtain_Left_3");
        curtain_left_1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-368.5f, 670, 0);
        curtain_left_2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-203.5f, 670, 0);
        curtain_left_3.GetComponent<RectTransform>().anchoredPosition = new Vector3(-38.5f, 670, 0);

        curtain_right_1 = DialogSystem.getChildGameObject(gameObject, "Curtain_Right_1");
        curtain_right_2 = DialogSystem.getChildGameObject(gameObject, "Curtain_Right_2");
        curtain_right_3 = DialogSystem.getChildGameObject(gameObject, "Curtain_Right_3");
        curtain_right_1.GetComponent<RectTransform>().anchoredPosition = new Vector3(368.5f, 670, 0);
        curtain_right_2.GetComponent<RectTransform>().anchoredPosition = new Vector3(203.5f, 670, 0);
        curtain_right_3.GetComponent<RectTransform>().anchoredPosition = new Vector3(38.5f, 670, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (scene_detect != SceneManager.GetActiveScene().name)
        {
            StartCoroutine(Scene_Text_Display_Handle());
            scene_detect = SceneManager.GetActiveScene().name;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        var transManagers = GameObject.FindGameObjectsWithTag("TransitionManagerz");
        if (transManagers.Length > 1)
        {
            Destroy(transManagers[1]);
        }
    }

    public void TransitionToScene(string scene_name)
    {
        StopCoroutine(cr_transition_handle);
        ReturnOriginalPosition();
        cr_transition_handle = StartCoroutine(TransitionHandle(scene_name));
    }

    public void RestartScene(string scene_name, float health, float heals, bool isHoldingSword, GameObject destroyObj)
    {
        StopCoroutine(cr_transition_restart_handle);
        ReturnOriginalPosition();
        GameManager.instance.SwitchToDefaultCam();
        
        cr_transition_restart_handle = StartCoroutine(TransitionHandleRestart(scene_name, health, heals, isHoldingSword, destroyObj));
    }

    private IEnumerator TransitionHandle(string scene_name)
    {
        var smooth_time = 0.25f * 1.25f;
        for (int i = 0; curtain_left_3.GetComponent<RectTransform>().anchoredPosition.y > 5; i++)
        {
            curtain_left_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-368.5f, 0, 0), ref velocity_left1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_left_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-203.5f, 0, 0), ref velocity_left2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_left_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-38.5f, 0, 0), ref velocity_left3, smooth_time * 2f, 999, Time.unscaledDeltaTime);

            curtain_right_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(368.5f, 0, 0), ref velocity_right1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_right_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(203.5f, 0, 0), ref velocity_right2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_right_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(38.5f, 0, 0), ref velocity_right3, smooth_time * 2f, 999, Time.unscaledDeltaTime);
            yield return null;
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainMenu_Manager.menu_selection_confirm = false;
        }
        SceneManager.LoadScene(scene_name);
        if (GameManager.instance)
        {
            GameManager.instance.NextLevelSetter(Vector2.zero);
        }
        Time.timeScale = 1;
        
        stop_descend = false;
        for (int i = 0; i < 60 * 20; i++)
        {
            if (stop_descend) { break; }
            curtain_left_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-368.5f, -670, 0), ref velocity_left1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_left_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-203.5f, -670, 0), ref velocity_left2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_left_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-38.5f, -670, 0), ref velocity_left3, smooth_time * 2f, 999, Time.unscaledDeltaTime);

            curtain_right_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(368.5f, -670, 0), ref velocity_right1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_right_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(203.5f, -670, 0), ref velocity_right2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_right_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(38.5f, -670, 0), ref velocity_right3, smooth_time * 2f, 999, Time.unscaledDeltaTime);
            yield return null;
        }
        


        //ReturnOriginalPosition();

    }

    private IEnumerator TransitionHandleRestart(string scene_name, float health, float heals, bool isHoldingSword, GameObject destroyObj)
    {
        var smooth_time = 0.25f * 1.25f;
        for (int i = 0; i < 60 * 4; i++)
        {
            curtain_left_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-368.5f, 0, 0), ref velocity_left1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_left_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-203.5f, 0, 0), ref velocity_left2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_left_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-38.5f, 0, 0), ref velocity_left3, smooth_time * 2f, 999, Time.unscaledDeltaTime);

            curtain_right_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(368.5f, 0, 0), ref velocity_right1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_right_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(203.5f, 0, 0), ref velocity_right2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_right_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(38.5f, 0, 0), ref velocity_right3, smooth_time * 2f, 999, Time.unscaledDeltaTime);
            yield return null;
        }

        SceneManager.LoadScene("TransitionTest_2");
        SceneManager.LoadScene(scene_name);
        switch (scene_name)
        {
            case "Hub":
                GameManager.instance.SetPlayerPosition(new Vector2(0f, 0f));
                break;

            case "Tutorial":
                GameManager.instance.SetPlayerPosition(new Vector2(0f, 0f));
                break;

            case "FaseUm":
                GameManager.instance.SetPlayerPosition(fase1_spawn);
                break;

            case "FaseDois":
                GameManager.instance.SetPlayerPosition(fase2_spawn);
                break;

            case "FaseTres":
                GameManager.instance.SetPlayerPosition(fase3_spawn);
                break;

            case "FaseQuatro":
                GameManager.instance.SetPlayerPosition(fase4_spawn);
                break;
        }

        GameManager.instance.currentHealth = health;
        GameManager.instance.SetHealth(GameManager.instance.currentHealth);
        GameManager.instance.SwitchToDefaultCam();
        GameManager.instance.SetAlive();
        if (scene_name == "Tutorial")
        {
            GameManager.instance.SetHeals(heals, true, isHoldingSword);
        } else
        {
            GameManager.instance.SetHeals(heals, false, isHoldingSword);
        }
        if (destroyObj != null)
        {
            Destroy(destroyObj);
        }
        
        Time.timeScale = 1;
        stop_descend = false;
        for (int i = 0; i < 60 * 20; i++)
        {
            if (stop_descend) { break; }
            curtain_left_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(-368.5f, -670, 0), ref velocity_left1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_left_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(-203.5f, -670, 0), ref velocity_left2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_left_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_left_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(-38.5f, -670, 0), ref velocity_left3, smooth_time * 2f, 999, Time.unscaledDeltaTime);

            curtain_right_1.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_1.GetComponent<RectTransform>().anchoredPosition, new Vector3(368.5f, -670, 0), ref velocity_right1, smooth_time, 999, Time.unscaledDeltaTime);
            curtain_right_2.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_2.GetComponent<RectTransform>().anchoredPosition, new Vector3(203.5f, -670, 0), ref velocity_right2, smooth_time * 1.5f, 999, Time.unscaledDeltaTime);
            curtain_right_3.GetComponent<RectTransform>().anchoredPosition =
                Vector3.SmoothDamp(curtain_right_3.GetComponent<RectTransform>().anchoredPosition, new Vector3(38.5f, -670, 0), ref velocity_right3, smooth_time * 2f, 999, Time.unscaledDeltaTime);
            yield return null;
        }

        //ReturnOriginalPosition();
    }

    private IEnumerator Scene_Text_Display_Handle()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TransitionTest_1":

                scene_text_display.GetComponent<TextMeshProUGUI>().text = "Transition Test";
                break;

            case "TransitionTest_2":

                scene_text_display.GetComponent<TextMeshProUGUI>().text = "Test Transition 2";
                break;

            case "MainMenu":

                scene_text_display.GetComponent<TextMeshProUGUI>().text = "";
                break;

            case "Tutorial":

                if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Temple Outskirts"; }
                if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Arredores do Templo"; }
                if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Alrededores del Templo"; }
                break;

            case "Hub":

                if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Temple of Our Lady of Pão D'Alho"; }
                if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Templo da Nossa Senhora do Pão D'Alho"; }
                if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Templo de Nuestra Señora de Pão D'Alho"; }
                break;

            case "FaseUm":

                if (ManagerOfScenes.instance.isEclipse)
                {
                    if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Madalenhas Woods: Woods Orchard"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Bosque das Madalenhas: Pomar do Bosque"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Bosque de las Magdaleñas: Huerta del Bosque"; }
                } else
                {
                    if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Madalenhas Woods"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Bosque das Madalenhas"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Bosque de las Magdaleñas"; }
                }
                break;

            case "FaseDois":

                if (ManagerOfScenes.instance.isEclipse)
                {
                    if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Semprefria Grotto: Grotto Depths"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Gruta Semprefria: Profundezas da Gruta"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Gruta Semprefria: Profundidades da Gruta"; }
                } else
                {
                    if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Semprefria Grotto"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Gruta Semprefria"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Gruta Semprefria"; }
                }
                break;

            case "FaseTres":

                if (ManagerOfScenes.instance.isEclipse)
                {
                    if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Nuncadorme: Peripheral Route"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Nuncadorme: Rota Periférica"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Nuncadorme: Ruta Periférica"; }
                }else
                {
                    if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Nuncadorme: City Streets"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Nuncadorme: Ruas da Cidade"; }
                    if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Nuncadorme: Calles de la Ciudad"; }
                }
                break;

            case "FaseQuatro":

                if (PlayerPrefs.GetInt("LANGUAGE") == 0) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Dungeons"; }
                if (PlayerPrefs.GetInt("LANGUAGE") == 1) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Masmorras"; }
                if (PlayerPrefs.GetInt("LANGUAGE") == 2) { scene_text_display.GetComponent<TextMeshProUGUI>().text = "Mazmorras"; }
                break;
        }
        for (int i = 0; i < 60 * 4; i++)
        {
            var txt_col = scene_text_display.GetComponent<TextMeshProUGUI>().color;
            txt_col.a = Mathf.Lerp(txt_col.a, 1.0f, Time.unscaledDeltaTime * 2f);
            scene_text_display.GetComponent<TextMeshProUGUI>().color = txt_col;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(2);
        for (int i = 0; i < 60 * 12; i++)
        {
            var txt_col = scene_text_display.GetComponent<TextMeshProUGUI>().color;
            txt_col.a = Mathf.Lerp(txt_col.a, 0.0f, Time.unscaledDeltaTime * 2f);
            scene_text_display.GetComponent<TextMeshProUGUI>().color = txt_col;
            yield return null;
        }
        var txt_colb = scene_text_display.GetComponent<TextMeshProUGUI>().color;
        txt_colb.a = 0.0f;
        scene_text_display.GetComponent<TextMeshProUGUI>().color = txt_colb;
    }

    private void ReturnOriginalPosition()
    {
        curtain_left_1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-368.5f, 670, 0);
        curtain_left_2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-203.5f, 670, 0);
        curtain_left_3.GetComponent<RectTransform>().anchoredPosition = new Vector3(-38.5f, 670, 0);

        curtain_right_1.GetComponent<RectTransform>().anchoredPosition = new Vector3(368.5f, 670, 0);
        curtain_right_2.GetComponent<RectTransform>().anchoredPosition = new Vector3(203.5f, 670, 0);
        curtain_right_3.GetComponent<RectTransform>().anchoredPosition = new Vector3(38.5f, 670, 0);

        velocity_left1 = Vector3.zero;
        velocity_left2 = Vector3.zero;
        velocity_left3 = Vector3.zero;
        velocity_right1 = Vector3.zero;
        velocity_right2 = Vector3.zero;
        velocity_right3 = Vector3.zero;

        stop_descend = true;
}

    private IEnumerator VoidTask()
    {
        yield return null;
    }

    public static void FaseUmSpawnSetter(Vector2 position)
    {
        fase1_spawn = new Vector2(position.x, position.y);
    }

}
