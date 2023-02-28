using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_HelpBox_6 : MonoBehaviour
{
    //public GameObject canvas; // TransitionCanvas NEEDS to be in scene
    GameObject TUT_BG;
    GameObject TUT_BAR_FILL;

    float heal_amount = 0f;
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
            subtext.GetComponent<TextMeshProUGUI>().text = "Game End";
            textdesc.GetComponent<TextMeshProUGUI>().text = "Congratulations on completing the game! More content will be made available soon...";
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 1)
        {
            subtext.GetComponent<TextMeshProUGUI>().text = "Fim de Jogo";
            textdesc.GetComponent<TextMeshProUGUI>().text = "Parabéns por completar o jogo! Em breve, mais conteúdo será disponibilizado...";
        }
        if (PlayerPrefs.GetInt("LANGUAGE") == 2)
        {
            subtext.GetComponent<TextMeshProUGUI>().text = "Fin del juego";
            textdesc.GetComponent<TextMeshProUGUI>().text = "¡Enhorabuena por completar el juego! Pronto habrá más contenido disponible...";
        }

        //canvas = GameObject.Find("TransitionCanvas"); // TransitionCanvas NEEDS to be in scene
        TUT_BG = DialogSystem.getChildGameObject(gameObject, "HelpBox_Background");
        TUT_BAR_FILL = DialogSystem.getChildGameObject(gameObject, "BAR_FULL");
        TUT_BG.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        StartCoroutine(Fade_In());
    }

    // Update is called once per frame
    void Update()
    {
        TUT_BAR_FILL.GetComponent<Image>().fillAmount = heal_amount;
        if (PauseManager.isPaused == false)
        {
            heal_amount += Time.deltaTime * 0.07f;
        }
        if (TUT_BAR_FILL.GetComponent<Image>().fillAmount >= 1 && transLock == false)
        {
            transLock = true;
            StartCoroutine(Fade_Out());
            //GameManager.instance.EndTheGame();
        }
    }
    private IEnumerator Fade_In()
    {
        for (int i = 0; heal_amount < 1; i++)
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
