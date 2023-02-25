using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    PlayerController pc;

    public GameObject canvas;
    public GameObject textObj;
    private Vector2 textScale;

    public GameObject skipProgress;
    private float skipValue = 0;

    private Coroutine textShow;

    private string[] english_intro =
    {
        "Some time ago, in the year 20XX, an Eclipse appeared for unknown reasons, casting the world in crimson darkness.",
        "Initially, it was thought to be a rare astronomic event, but soon it was discovered that it was no such thing.",
        "A few weeks pass, people that looked too much at the Eclipse started to go crazy, feral and primitive, attacking one another.",
        "Some time later, other mutations and monsters appeared, like animals that grew too big or became way too aggressive.",
        "All of that resulted in the disappearence of humanity's most vital activity, essentially removing it's status as a civilized species...",
        "The Barbecue.",
        "But in these dark and somber times, a hero emerges, Sabet.",
        "Born from barbecues, molded by them, this hero would save humanity, as nothing could come between him and his barbecue.",
    };

    private string[] portuguese_intro =
    {
        "Há alguns anos, em 20XX, surgiu um Eclipse por causas desconhecidas, jogando o mundo em uma escuridão vermelha/rubra.",
        "De início se imaginou ser um evento astrológico/astronômico (nn sei qual) raro, mas logo se descobriu que não era tal acontecimento.",
        "Com algumas semanas, pessoas que olhavam demais para o Eclipse começaram a ficar loucas, bestiais e primitivas, atacando umas às outras.",
        "Passado mais algum tempo, surgiram outras mutações e monstros, animais que cresciam demais, ou ficavam mais agressivos.",
        "Tudo isso culminou no desaparecimento da atividade mais importante para a humanidade, essencialmente a tirando do status de civilização...",
        "O Churrasco.",
        "Mas nessses tempos de escuridão, surge um herói, Sabet.",
        "Nascido no churrasco... Moldado no churrasco... este herói iria salvar a humanidade, pois nada ficaria entre ele e seu churrasco.",
    };

    private string[] spanish_intro =
    {
        "Há alguns anos, em 20XX, surgiu um Eclipse por causas desconhecidas, jogando o mundo em uma escuridão vermelha/rubra.",
        "De início se imaginou ser um evento astrológico/astronômico (nn sei qual) raro, mas logo se descobriu que não era tal acontecimento.",
        "Com algumas semanas, pessoas que olhavam demais para o Eclipse começaram a ficar loucas, bestiais e primitivas, atacando umas às outras.",
        "Passado mais algum tempo, surgiram outras mutações e monstros, animais que cresciam demais, ou ficavam mais agressivos.",
        "Tudo isso culminou no desaparecimento da atividade mais importante para a humanidade, essencialmente a tirando do status de civilização... ",
        "O Churrasco.",
        "Mas nessses tempos de escuridão, surge um herói, Sabet.",
        "Nascido no churrasco... Moldado no churrasco... este herói iria salvar a humanidade, pois nada ficaria entre ele e seu churrasco.",
    };

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
        canvas = GameObject.Find("TransitionCanvas");
        skipProgress = DialogSystem.getChildGameObject(gameObject, "ProgressBar_Highlight");

        textObj = DialogSystem.getChildGameObject(gameObject, "TextDisplay");
        textObj.GetComponent<TextMeshProUGUI>().text = "";
        textObj.GetComponent<TextMeshProUGUI>().alpha = 0;

        textScale = textObj.transform.localScale;
        textScale = new Vector2(0.5f, 0.5f);
        textObj.transform.localScale = textScale;

        textShow = StartCoroutine(Flow());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (skipValue < 1)
        {
            if (pc.Movimento.Attack.ReadValue<float>() != 0)
            {
                skipValue += 0.015f;
            }
            if (skipValue >= 1)
            {
                StopCoroutine(textShow);
                GetComponent<Canvas>().sortingOrder = 0;
                canvas.GetComponent<Transition_Manager>().TransitionToScene("StartVoid");
            }
        }
        skipProgress.GetComponent<Image>().fillAmount = skipValue;
        
    }

    public IEnumerator Flow()
    {
        for (int j = 0; j <= 7; j++)
        {
            if (PlayerPrefs.GetInt("LANGUAGE") == 0)
            {
                textObj.GetComponent<TextMeshProUGUI>().text = english_intro[j];
            }
            if (PlayerPrefs.GetInt("LANGUAGE") == 1)
            {
                textObj.GetComponent<TextMeshProUGUI>().text = portuguese_intro[j];
            }
            if (PlayerPrefs.GetInt("LANGUAGE") == 2)
            {
                textObj.GetComponent<TextMeshProUGUI>().text = spanish_intro[j];
            }

            textScale = textObj.transform.localScale;
            textScale = new Vector2(0.5f, 0.5f);
            textObj.transform.localScale = textScale;

            for (int i = 0; textScale.x < 1.49f && textObj.GetComponent<TextMeshProUGUI>().alpha < 0.99f; i++)
            {
                textScale = textObj.transform.localScale;
                textScale = new Vector2(
                                        Mathf.Lerp(textObj.transform.localScale.x, 1.5f, Time.deltaTime * 1f),
                                        Mathf.Lerp(textObj.transform.localScale.y, 1.5f, Time.deltaTime * 1f)
                                        );
                textObj.transform.localScale = textScale;
                textObj.GetComponent<TextMeshProUGUI>().alpha = Mathf.Lerp(textObj.GetComponent<TextMeshProUGUI>().alpha, 1f, Time.deltaTime * 1f);
                //Debug.Log("alpha = " + textObj.GetComponent<TextMeshProUGUI>().alpha);
                //Debug.Log("scale = " + textObj.transform.localScale.x);
                yield return null;
            }
            yield return new WaitForSeconds(1);
            for (int i = 0; textObj.GetComponent<TextMeshProUGUI>().alpha > 0.05f; i++)
            {
                textObj.GetComponent<TextMeshProUGUI>().alpha = Mathf.Lerp(textObj.GetComponent<TextMeshProUGUI>().alpha, 0f, Time.deltaTime * 1.5f);
                yield return null;
            }
        }
        GetComponent<Canvas>().sortingOrder = 0;
        yield return new WaitForSeconds(1);
        //SceneManager.LoadScene("StartVoid");
        canvas.GetComponent<Transition_Manager>().TransitionToScene("StartVoid");
    }
}
