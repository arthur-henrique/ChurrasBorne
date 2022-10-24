using System.Collections;
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

    string scene_detect;

    GameObject scene_text_display;

    // Start is called before the first frame update
    void Start()
    {
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

    public void TransitionToScene(string scene_name)
    {
        StartCoroutine(TransitionHandle(scene_name));
    }

    private IEnumerator TransitionHandle(string scene_name)
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
        SceneManager.LoadScene(scene_name);
        Time.timeScale = 1;
        for (int i = 0; i < 60 * 20; i++)
        {
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
    }

    private IEnumerator Scene_Text_Display_Handle()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TransitionTest_1":

                scene_text_display.GetComponent<TextMeshProUGUI>().text = "Teste de transição";
                break;

            case "TransitionTest_2":

                scene_text_display.GetComponent<TextMeshProUGUI>().text = "Boss final Nazaré Tedesco";
                break;

            case "Tutorial":

                scene_text_display.GetComponent<TextMeshProUGUI>().text = "Tutorial";
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
        for (int i = 0; i < 60 * 8; i++)
        {
            var txt_col = scene_text_display.GetComponent<TextMeshProUGUI>().color;
            txt_col.a = Mathf.Lerp(txt_col.a, 0.0f, Time.unscaledDeltaTime * 2f);
            scene_text_display.GetComponent<TextMeshProUGUI>().color = txt_col;
            yield return null;
        }
    }
}
