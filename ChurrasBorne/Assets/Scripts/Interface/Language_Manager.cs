using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Language_Manager : MonoBehaviour
{
    public GameObject canvas;
    PlayerController pc;

    private GameObject english_flag;
    private GameObject brazil_flag;
    private GameObject spanish_flag;
    private GameObject dropout;

    public AudioSource audioSource;
    public AudioClip ui_move;
    public AudioClip ui_confirm;

    public static bool lockSelec = false;
    private bool dropout_enable = false;
    public static int selec = 0;
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

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("TransitionCanvas");

        english_flag = DialogSystem.getChildGameObject(gameObject, "English_Image");
        brazil_flag = DialogSystem.getChildGameObject(gameObject, "Portuguese_Image");
        spanish_flag = DialogSystem.getChildGameObject(gameObject, "Spanish_Image");
        dropout = DialogSystem.getChildGameObject(gameObject, "Dropout");

        lockSelec = false;
        Debug.Log(PlayerPrefs.GetInt("LANGUAGE"));

        if (PlayerPrefs.GetInt("BOOT") == 1)
        {
            SceneManager.LoadScene("MainMenu");
        }

        StartCoroutine(render_manager());
    }

    // Update is called once per frame
    void Update()
    {
        if (lockSelec == false)
        {
            if (pc.Movimento.Attack.WasPressedThisFrame())
            {
                
                PlayerPrefs.SetInt("LANGUAGE", selec);
                PlayerPrefs.SetInt("BOOT", 1);
                //canvas.GetComponent<Transition_Manager>().TransitionToScene("MainMenu");
                lockSelec = true;
                audioSource.PlayOneShot(ui_confirm, audioSource.volume);
                dropout.SetActive(true);
                dropout_enable = true;

            }

            if (pc.Movimento.LesteOeste.WasPressedThisFrame())
            {
                audioSource.PlayOneShot(ui_move, audioSource.volume);
                selec += (int)pc.Movimento.LesteOeste.ReadValue<float>();
            }
            //Debug.Log(selec);
            if (selec > 2) { selec = 0; }
            if (selec < 0) { selec = 2; }

            switch (selec)
            {
                case 0:

                    var en_col = english_flag.GetComponent<Image>().color;
                    en_col.a = 1f;
                    english_flag.GetComponent<Image>().color = en_col;

                    var br_col = brazil_flag.GetComponent<Image>().color;
                    br_col.a = 0.3333333f;
                    brazil_flag.GetComponent<Image>().color = br_col;

                    var sp_col = spanish_flag.GetComponent<Image>().color;
                    sp_col.a = 0.3333333f;
                    spanish_flag.GetComponent<Image>().color = sp_col;
                    break;

                case 1:

                    en_col = english_flag.GetComponent<Image>().color;
                    en_col.a = 0.3333333f;
                    english_flag.GetComponent<Image>().color = en_col;

                    br_col = brazil_flag.GetComponent<Image>().color;
                    br_col.a = 1f;
                    brazil_flag.GetComponent<Image>().color = br_col;

                    sp_col = spanish_flag.GetComponent<Image>().color;
                    sp_col.a = 0.3333333f;
                    spanish_flag.GetComponent<Image>().color = sp_col;
                    break;

                case 2:

                    en_col = english_flag.GetComponent<Image>().color;
                    en_col.a = 0.3333333f;
                    english_flag.GetComponent<Image>().color = en_col;

                    br_col = brazil_flag.GetComponent<Image>().color;
                    br_col.a = 0.3333333f;
                    brazil_flag.GetComponent<Image>().color = br_col;

                    sp_col = spanish_flag.GetComponent<Image>().color;
                    sp_col.a = 1f;
                    spanish_flag.GetComponent<Image>().color = sp_col;
                    break;
            }
        } else
        {
            if (dropout_enable == true)
            {
                if (dropout.GetComponent<Image>().color.a >= 0.999f)
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
        if (dropout.GetComponent<Image>().color.a <= 0.01f)
        {
            dropout.SetActive(false);
        } else
        {
            dropout.SetActive(true);
        }
    }

    IEnumerator render_manager()
    {
        while(true)
        {
            if (dropout_enable == true)
            {
                var dropcol = dropout.GetComponent<Image>().color;
                dropcol.a = Mathf.Lerp(dropcol.a, 1f, Time.deltaTime * 14f);
                dropout.GetComponent<Image>().color = dropcol;
            } else
            {
                var dropcol = dropout.GetComponent<Image>().color;
                dropcol.a = Mathf.Lerp(dropcol.a, 0f, Time.deltaTime * 14f);
                dropout.GetComponent<Image>().color = dropcol;
            }
            yield return null;
        }
        
    }
        
}
