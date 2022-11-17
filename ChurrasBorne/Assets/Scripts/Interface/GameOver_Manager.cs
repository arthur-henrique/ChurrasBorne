using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;

public class GameOver_Manager : MonoBehaviour
{
    public GameObject canvas; // TransitionCanvas NEEDS to be in scene
    PlayerController pc;

    private GameObject blurobj;
    private DepthOfField dofComponent;

    public static bool gover_selection_confirm = false;
    public static int gover_selection_position;

    private GameObject gover_bg;
    private GameObject gover_lay;
    private GameObject gover_label;
    private GameObject gover_drop;
    private GameObject gover_sel1;
    private GameObject gover_sel2;
    private GameObject gover_sel3;

    private Vector3 velocity_drop_shadow = Vector3.zero;
    private Vector3 velocity_label = Vector3.zero;
    private Vector3 velocity_sel1 = Vector3.zero;
    private Vector3 velocity_sel2 = Vector3.zero;
    private Vector3 velocity_sel3 = Vector3.zero;

    float ypos = 34.3f;

    private bool canChange = false;

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
        canvas = GameObject.Find("TransitionCanvas"); // TransitionCanvas NEEDS to be in scene

        blurobj = GameObject.Find("BlurObject");
        Volume volume = blurobj.GetComponent<Volume>();
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
        {
            dofComponent = tmp;
        }

        dofComponent.focalLength.value = 1;

        gover_lay = DialogSystem.getChildGameObject(gameObject, "GOVER_LayeredShadow");
        gover_bg = DialogSystem.getChildGameObject(gameObject, "GOVER_Background");
        gover_drop = DialogSystem.getChildGameObject(gameObject, "GOVER_DropShadow");
        gover_label = DialogSystem.getChildGameObject(gameObject, "GOVER_Label");
        gover_sel1 = DialogSystem.getChildGameObject(gameObject, "GOVER_Retry");
        gover_sel2 = DialogSystem.getChildGameObject(gameObject, "GOVER_Hub");
        gover_sel3 = DialogSystem.getChildGameObject(gameObject, "GOVER_Title");

        gover_lay.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        gover_bg.GetComponent<Image>().color = new Color(1.0f, 0.2352941f, 0.2352941f, 0.0f);
        gover_bg.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        gover_drop.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        gover_drop.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -15.5f + 24, 0);

        gover_label.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.4103774f, 0.4103774f, 0.0f);
        gover_label.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 85f + 24, 0);

        gover_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        gover_sel1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -15.5f - 24, 0);

        gover_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        gover_sel2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -54f - 24, 0);

        gover_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        gover_sel3.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -93f - 24, 0);

        StartCoroutine(Gover_Fade_In());

    }

    // Update is called once per frame
    void Update()
    {
        if (canChange == true)
        {
            if (pc.Movimento.NorteSul.WasPressedThisFrame() && gover_selection_confirm == false)
            {
                gover_selection_position -= (int)pc.Movimento.NorteSul.ReadValue<float>();
                if (gover_selection_position > 2) { gover_selection_position = 0; }
                if (gover_selection_position < 0) { gover_selection_position = 2; }
            }

            switch (gover_selection_position)
            {
                case 0: ypos = -15.5f; break;
                case 1: ypos = -54f; break;
                case 2: ypos = -93f; break;
            }

            switch (gover_selection_position)
            {
                case 0:
                    gover_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel1.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                    gover_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    gover_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    break;

                case 1:
                    gover_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    gover_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel2.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                    gover_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel3.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    break;

                case 2:
                    gover_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel1.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    gover_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel2.GetComponent<TextMeshProUGUI>().color.a, 0.3f, Time.unscaledDeltaTime * 5f));
                    gover_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel3.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 5f));
                    break;
            }

            gover_drop.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(gover_drop.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, ypos, 0), ref velocity_drop_shadow, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);

            if (pc.Movimento.Attack.WasPressedThisFrame())
            {
                gover_selection_confirm = true;
            }

            if (gover_selection_confirm == true)
            {
                switch (gover_selection_position)
                {
                    case 0:
                        gover_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                        break;

                    case 1:
                        gover_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                        break;

                    case 2:
                        gover_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.7411765f, 0.4039216f, 1.0f);
                        canvas.GetComponent<Transition_Manager>().TransitionToScene("MainMenu");
                        break;
                }
            }
        }
        
    }

    private IEnumerator Gover_Fade_In()
    {
        for (int i = 0; i < 60 * 6; i++)
        {
            dofComponent.focalLength.value = Mathf.Lerp(dofComponent.focalLength.value, 32, 2f * Time.unscaledDeltaTime);

            gover_bg.GetComponent<RectTransform>().localScale = new Vector2(Mathf.Lerp(gover_bg.GetComponent<RectTransform>().localScale.x, 1f, Time.unscaledDeltaTime * 2f),
                                                                            Mathf.Lerp(gover_bg.GetComponent<RectTransform>().localScale.y, 1f, Time.unscaledDeltaTime * 2f));


            gover_lay.GetComponent<Image>().color = new Color(1.0f, 0.2352941f, 0.2352941f, Mathf.Lerp(gover_bg.GetComponent<Image>().color.a, 0.8f, Time.unscaledDeltaTime * 1f));
            gover_bg.GetComponent<Image>().color = new Color(1.0f, 0.2352941f, 0.2352941f, Mathf.Lerp(gover_bg.GetComponent<Image>().color.a, 0.6f, Time.unscaledDeltaTime * 1f));
            gover_drop.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_drop.GetComponent<Image>().color.a, 1.0f, Time.unscaledDeltaTime * 1f));
            gover_label.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.4103774f, 0.4103774f, Mathf.Lerp(gover_label.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 1f));
            gover_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel1.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 1f));
            gover_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel2.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 1f));
            gover_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(gover_sel3.GetComponent<TextMeshProUGUI>().color.a, 1.0f, Time.unscaledDeltaTime * 1f));

            gover_drop.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(gover_drop.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -15.5f, 0), ref velocity_drop_shadow, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            gover_label.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(gover_label.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, 85f, 0), ref velocity_label, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            gover_sel1.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(gover_sel1.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -15.5f, 0), ref velocity_sel1, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            gover_sel2.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(gover_sel2.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -54f, 0), ref velocity_sel2, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            gover_sel3.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(gover_sel3.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -93f, 0), ref velocity_sel3, 5 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);
            yield return null;
        }
        canChange = true;
    }
}
