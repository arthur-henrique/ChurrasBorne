using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferreiro_Encounter_1_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public static bool ferreiro_encounter_1_occurred;
    public static bool ferreiro_encounter_2_occurred;

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
        if (!ferreiro_encounter_1_occurred || GameManager.instance.GetHasCleared(0) == false)
        {
            ferreiro_encounter_1_occurred = false;
        }
        if (!ferreiro_encounter_2_occurred || GameManager.instance.GetHasCleared(0) == false)
        {
            ferreiro_encounter_2_occurred = false;
        }
        target = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Animator>().SetBool("APAGAR", false);
        dbox.GetComponent<DialogSystem>().db_PullDOWN();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (ferreiro_encounter_1_occurred == false && ferreiro_encounter_2_occurred == false)
            {
                float dist = Vector2.Distance(target.transform.position, transform.position);

                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3)
                {
                    var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                    if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                    {
                        dbox.GetComponent<DialogSystem>().db_PullUP();
                        dbox.GetComponent<DialogSystem>().db_SetSceneComplex(0);
                        ferreiro_encounter_1_occurred = true;
                    }
                }
            }

            if (GoatAI.goat_boss_died == true && ferreiro_encounter_2_occurred == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-56f, 262f), 8f * Time.deltaTime);
                target.transform.position = Vector2.MoveTowards(target.transform.position, new Vector2(-48f, 262f), 10f * Time.deltaTime);
                GetComponent<Animator>().SetBool("WALKING", true);
                PlayerMovement.DisableControl();
                GameManager.instance.SwitchToDefaultCam();
                if (Vector2.Distance(transform.position, new Vector2(-56f, 262f)) < 1f) 
                {
                    GetComponent<Animator>().SetBool("WALKING", false);
                    var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                    if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                    {
                        dbox.GetComponent<DialogSystem>().db_PullUP();
                        dbox.GetComponent<DialogSystem>().db_SetSceneComplex(1);
                        ferreiro_encounter_2_occurred = true;
                    }

                }
            }
            
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        //Gizmos.color = Color.white;
        //Gizmos.DrawSphere(transform.position, 3);
    }
}
