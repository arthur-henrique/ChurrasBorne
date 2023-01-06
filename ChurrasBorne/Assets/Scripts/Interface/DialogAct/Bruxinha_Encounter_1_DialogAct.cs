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
        dbox.GetComponent<DialogSystem>().db_PullDOWN();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (bruxinha_encounter_1_occurred == false && bruxinha_encounter_2_occurred == false)
            {
                float dist = Vector2.Distance(target.transform.position, transform.position);

                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3)
                {
                    var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                    if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                    {
                        dbox.GetComponent<DialogSystem>().db_PullUP();
                        dbox.GetComponent<DialogSystem>().db_SetSceneComplex(3);
                        bruxinha_encounter_1_occurred = true;
                    }
                }
            }

            if (CEOofSpidersAI.spider_boss_died == true && bruxinha_encounter_2_occurred == false)
            {
                var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                {
                    dbox.GetComponent<DialogSystem>().db_PullUP();
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(4);
                    bruxinha_encounter_2_occurred = true;
                }
            }

        }
    }
}
