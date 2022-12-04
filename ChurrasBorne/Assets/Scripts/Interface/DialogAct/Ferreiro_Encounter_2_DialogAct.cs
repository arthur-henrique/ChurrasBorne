using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferreiro_Encounter_2_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public static bool ferreiro_encounter_3_occurred;

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
        if (!ferreiro_encounter_3_occurred)
        {
            ferreiro_encounter_3_occurred = false;
        }
        target = GameObject.FindGameObjectWithTag("Player");
        dbox.GetComponent<DialogSystem>().db_PullDOWN();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (ferreiro_encounter_3_occurred == false)
            {
                float dist = Vector2.Distance(target.transform.position, transform.position);

                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3)
                {
                    var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                    if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                    {
                        dbox.GetComponent<DialogSystem>().db_PullUP();
                        dbox.GetComponent<DialogSystem>().db_SetSceneComplex(2);
                        ferreiro_encounter_3_occurred = true;
                    }
                }
            }

        }
    }
}
