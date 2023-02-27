using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bruxinha_Encounter_1_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public static bool bruxinha_encounter_1_occurred;
    public static bool bruxinha_encounter_2_occurred;

    public Material sprite_lit;
    public Material sprite_unlit;

    public GameObject notif_balloon;
    public Sprite notif_exclamation;

    private void Awake()
    {
        pc = new PlayerController();
        dbox = GameObject.Find("DialogBox");
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
        notif_balloon = DialogSystem.getChildGameObject(gameObject, "Notification_Balloon");
        if (!bruxinha_encounter_1_occurred || GameManager.instance.GetHasCleared(2) == false)
        {
            bruxinha_encounter_1_occurred = false;
        }
        if (!bruxinha_encounter_2_occurred || GameManager.instance.GetHasCleared(2) == false)
        {
            bruxinha_encounter_2_occurred = false;
        }
        if (GameManager.instance.GetHasCleared(2) == true)
        {
            gameObject.SetActive(false);
        }
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        notif_balloon.transform.localPosition = new Vector2(0, 4.75f + Mathf.Sin(Time.time * 1f) * 0.25f);
        if (target)
        {
            if (bruxinha_encounter_1_occurred == false && bruxinha_encounter_2_occurred == false)
            {
                notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_exclamation;
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (dist <= 3)
                {
                    GetComponent<SpriteRenderer>().material = sprite_unlit;
                }
                else
                {
                    GetComponent<SpriteRenderer>().material = sprite_lit;
                }
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3)
                {
                    notif_balloon.SetActive(false);
                    GetComponent<SpriteRenderer>().material = sprite_lit;
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(9, gameObject);
                    bruxinha_encounter_1_occurred = true;
                    GetComponent<Animator>().SetTrigger("TALKING");
                }
            }

            if (CEOofSpidersAI.spider_boss_died == true && bruxinha_encounter_2_occurred == false)
            {
                dbox.GetComponent<DialogSystem>().db_SetSceneComplex(10, gameObject);
                bruxinha_encounter_2_occurred = true;
                GetComponent<Animator>().SetTrigger("TALKING");
            }

        }
    }
}
