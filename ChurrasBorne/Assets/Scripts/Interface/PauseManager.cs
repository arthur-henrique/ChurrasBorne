using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    PlayerController pc;

    private GameObject pause_bg;
    private GameObject pause_drop_shadow;
    private GameObject pause_layered_shadow;

    private GameObject pause_label;
    private GameObject pause_sel1;
    private GameObject pause_sel2;
    private GameObject pause_sel3;

    private Vector3 velocity_bg = Vector3.zero;

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
        pause_bg = DialogSystem.getChildGameObject(gameObject, "PAUSE_Background");
        pause_bg.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
        pause_bg.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        //pause_bg.GetComponent<Image>().color = new Color(0.5607843f, 1.0f, 1.0f, 1.0f);

        pause_layered_shadow = DialogSystem.getChildGameObject(gameObject, "PAUSE_LayeredShadow");
        pause_layered_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // pause_drop_shadow.GetComponent<Image>().color = new Color(0.8705882f, 1.0f, 1.0f, 1.0f);

        pause_drop_shadow = DialogSystem.getChildGameObject(gameObject, "PAUSE_DropShadow");
        pause_drop_shadow.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // pause_drop_shadow.GetComponent<Image>().color = new Color(0.6745098f, 1.0f, 1.0f, 1.0f);

        pause_label = DialogSystem.getChildGameObject(gameObject, "PAUSE_Pause");
        pause_label.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        pause_sel1 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Continuar");
        pause_sel1.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        pause_sel2 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Hub");
        pause_sel2.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        pause_sel3 = DialogSystem.getChildGameObject(gameObject, "PAUSE_Titulo");
        pause_sel3.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (pc.Movimento.Attack.WasPressedThisFrame())
        {
            Show_Pause();
        }
    }

    private void Show_Pause()
    {
        StartCoroutine(Pause_Background_Zoom_In());
    }

    private IEnumerator Pause_Background_Zoom_In()
    {
        for (int i = 0; i < 60 * 2; i++)
        {
            pause_bg.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(pause_bg.GetComponent<RectTransform>().localScale, new Vector3(1, 1, 1), ref velocity_bg, 4 * Time.unscaledDeltaTime);
            pause_bg.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(pause_bg.GetComponent<Image>().color.a, 1, Time.unscaledDeltaTime * 5f));
            yield return null;
        }
    }
}
