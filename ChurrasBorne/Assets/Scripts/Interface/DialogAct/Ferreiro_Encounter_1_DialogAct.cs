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
        if (!ferreiro_encounter_1_occurred || GameManager.instance.GetHasCleared(0) == false)
        {
            ferreiro_encounter_1_occurred = false;
        }
        if (!ferreiro_encounter_2_occurred || GameManager.instance.GetHasCleared(0) == false)
        {
            ferreiro_encounter_2_occurred = false;
        }
        if (GameManager.instance.GetHasCleared(0) == true)
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
            if (ferreiro_encounter_1_occurred == false && ferreiro_encounter_2_occurred == false)
            {
                notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_exclamation;
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (dist <= 3)
                {
                    GetComponent<SpriteRenderer>().material = sprite_unlit;
                } else
                {
                    GetComponent<SpriteRenderer>().material = sprite_lit;
                }
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(0);
                    notif_balloon.SetActive(false);
                    ferreiro_encounter_1_occurred = true;
                    GetComponent<SpriteRenderer>().material = sprite_lit;
                }
            }

            if (GoatAI.goat_boss_died == true && ferreiro_encounter_2_occurred == false)
            {
                GetComponent<SpriteRenderer>().material = sprite_lit;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-56f, 262f), 8f * Time.deltaTime);
                target.transform.position = Vector2.MoveTowards(target.transform.position, new Vector2(-48f, 262f), 10f * Time.deltaTime);
                GetComponent<Animator>().SetBool("WALKING", true);
                PlayerMovement.DisableControl();
                GameManager.instance.SwitchToDefaultCam();
                if (Vector2.Distance(transform.position, new Vector2(-56f, 262f)) < 1f) 
                {
                    GetComponent<Animator>().SetBool("WALKING", false);
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(1);
                    ferreiro_encounter_2_occurred = true;
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
