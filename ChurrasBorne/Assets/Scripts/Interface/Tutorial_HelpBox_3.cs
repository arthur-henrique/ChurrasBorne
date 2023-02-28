using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_HelpBox_3 : MonoBehaviour
{
    GameObject TUT_BG;
    GameObject TUT_BAR_FILL;

    float attack_amount = 0f;
    bool transLock = false;
    PlayerController pc;

    GameObject subtext;
    GameObject textdesc;

    private void OnEnable()
    {
        pc.Enable();
    }
    private void OnDisable()
    {
        pc.Disable();
    }

    private void Awake()
    {
        pc = new PlayerController();
    }
    // Start is called before the first frame update
    void Start()
    {
        subtext = DialogSystem.getChildGameObject(gameObject, "SubText");
        textdesc = DialogSystem.getChildGameObject(gameObject, "TextDesc");
        if (PlayerPrefs.GetInt("LANGUAGE") == 0)
        {
            subtext.GetComponent<TextMeshProUGUI>().text = "Practice attacking";
            textdesc.GetComponent<TextMeshProUGUI>().text = "Use alongside with movement:";
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 1)
        {
            subtext.GetComponent<TextMeshProUGUI>().text = "Pratique o ataque";
            textdesc.GetComponent<TextMeshProUGUI>().text = "Use em conjunto com o movimento:";
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 2)
        {
            subtext.GetComponent<TextMeshProUGUI>().text = "Practicar el ataque";
            textdesc.GetComponent<TextMeshProUGUI>().text = "Utilizar junto con el movimiento:";
        }

        TUT_BG = DialogSystem.getChildGameObject(gameObject, "HelpBox_Background");
        TUT_BAR_FILL = DialogSystem.getChildGameObject(gameObject, "BAR_FULL");
        TUT_BG.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        StartCoroutine(Fade_In());
    }

    // Update is called once per frame
    void Update()
    {
        TUT_BAR_FILL.GetComponent<Image>().fillAmount = attack_amount;
        if (PauseManager.isPaused == false && pc.Movimento.Attack.WasPressedThisFrame())
        {
            attack_amount += 0.34f;
        }
        if (TUT_BAR_FILL.GetComponent<Image>().fillAmount >= 1 && transLock == false)
        {
            transLock = true;
            StartCoroutine(Fade_Out());
        }
    }

    private IEnumerator Fade_In()
    {
        for (int i = 0; attack_amount < 1; i++)
        {
            var tutbgsc = TUT_BG.GetComponent<RectTransform>().localScale;
            tutbgsc.x = Mathf.Lerp(tutbgsc.x, 1, 6f * Time.deltaTime);
            TUT_BG.GetComponent<RectTransform>().localScale = tutbgsc;
            if (attack_amount >= 1) { yield break; }
            yield return null;
        }
    }

    private IEnumerator Fade_Out()
    {
        for (int i = 0; TUT_BG.GetComponent<RectTransform>().localScale.x != 0; i++)
        {
            var tutbgsc = TUT_BG.GetComponent<RectTransform>().localScale;
            tutbgsc.x = Mathf.Lerp(tutbgsc.x, 0, 8f * Time.deltaTime);
            TUT_BG.GetComponent<RectTransform>().localScale = tutbgsc;
            yield return null;
        }
        Destroy(gameObject);
    }
}
