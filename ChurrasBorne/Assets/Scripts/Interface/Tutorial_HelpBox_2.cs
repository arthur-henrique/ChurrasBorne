using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_HelpBox_2 : MonoBehaviour
{
    public GameObject tutbox3;
    GameObject TUT_BG;
    GameObject TUT_BAR_FILL;

    float roll_amount = 0f;
    bool transLock = false;
    PlayerController pc;

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
        TUT_BG = DialogSystem.getChildGameObject(gameObject, "HelpBox_Background");
        TUT_BAR_FILL = DialogSystem.getChildGameObject(gameObject, "BAR_FULL");
        TUT_BG.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        StartCoroutine(Fade_In());
    }

    // Update is called once per frame
    void Update()
    {
        TUT_BAR_FILL.GetComponent<Image>().fillAmount = roll_amount;
        if (PauseManager.isPaused == false && pc.Movimento.Rolar.WasPressedThisFrame())
        {
            roll_amount += 0.34f;
        }
        if (TUT_BAR_FILL.GetComponent<Image>().fillAmount >= 1 && transLock == false)
        {
            transLock = true;
            StartCoroutine(Fade_Out());
            //var hb3 = GameObject.Find("HelpBox_3");
            //hb3.SetActive(true);
            Instantiate(tutbox3);
        }
    }

    private IEnumerator Fade_In()
    {
        for (int i = 0; i < 60 * 2; i++)
        {
            var tutbgsc = TUT_BG.GetComponent<RectTransform>().localScale;
            tutbgsc.x = Mathf.Lerp(tutbgsc.x, 1, 6f * Time.deltaTime);
            TUT_BG.GetComponent<RectTransform>().localScale = tutbgsc;
            yield return null;
        }
    }

    private IEnumerator Fade_Out()
    {
        for (int i = 0; i < 60 * 2; i++)
        {
            var tutbgsc = TUT_BG.GetComponent<RectTransform>().localScale;
            tutbgsc.x = Mathf.Lerp(tutbgsc.x, 0, 8f * Time.deltaTime);
            TUT_BG.GetComponent<RectTransform>().localScale = tutbgsc;
            yield return null;
        }
        Destroy(gameObject);
    }
}
