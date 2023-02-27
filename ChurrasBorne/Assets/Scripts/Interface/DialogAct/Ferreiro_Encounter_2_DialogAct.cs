using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferreiro_Encounter_2_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public static bool ferreiro_encounter_3_occurred;
    public static bool ferreiro_encounter_4_occurred;
    public static bool ferreiro_encounter_5_occurred;
    public static bool ferreiro_encounter_6_occurred;
    public static bool ferreiro_encounter_7_occurred;

    public GameObject notif_balloon;
    public Sprite notif_exclamation;
    public Sprite notif_ellipsis;
    public Sprite notif_newquest;
    public Sprite notif_questcomplete;
    public Sprite notif_observation;
    public Sprite item_void;

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
    }

    // Update is called once per frame
    void Update()
    {
        notif_balloon.transform.localPosition = new Vector2(0, 4.75f + Mathf.Sin(Time.time * 1f) * 0.25f);

        if (GameManager.instance.GetHasCleared(0))
        {
            gameObject.SetActive(true);
        } else
        {
            gameObject.SetActive(false);
        }

        if (target)
        {
            if (ferreiro_encounter_3_occurred == false)
            {
                float dist = Vector2.Distance(target.transform.position, transform.position);
                notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_ellipsis;
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3 && PlayerMovement.pc.Movimento.enabled)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(2, gameObject, ChangeBalloon(notif_exclamation));
                    GetComponent<Animator>().SetTrigger("TALKING");
                    notif_balloon.GetComponent<SpriteRenderer>().sprite = item_void;
                    ferreiro_encounter_3_occurred = true;
                }
            } else if (ferreiro_encounter_4_occurred == false)
            {
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3 && PlayerMovement.pc.Movimento.enabled)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(3, gameObject, OpenDoor());
                    GetComponent<Animator>().SetTrigger("TALKING");
                    ferreiro_encounter_4_occurred = true;
                }
            } else if (ferreiro_encounter_5_occurred == false)
            {
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3 && PlayerMovement.pc.Movimento.enabled)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(4, gameObject);
                    GetComponent<Animator>().SetTrigger("TALKING");
                    ferreiro_encounter_5_occurred = true;
                }
            } else if (GameManager.instance.hasCompletedQuestOne && ferreiro_encounter_6_occurred == false)
            {
                notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_questcomplete;
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3 && PlayerMovement.pc.Movimento.enabled)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(6, gameObject, GiveArmor());
                    GetComponent<Animator>().SetTrigger("TALKING");
                    ferreiro_encounter_6_occurred = true;
                    GameManager.instance.HasCompletedFirstQuest();
                    HealthBar_Manager.newItem = false;
                }
            } else if (GameObject.Find("Encapuzado"))
            {
                notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_observation;
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3 && PlayerMovement.pc.Movimento.enabled)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(8, gameObject);
                    GetComponent<Animator>().SetTrigger("TALKING");
                }
            } else if (!GameManager.instance.hasCompletedQuestOne)
            {
                notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_ellipsis;
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3 && PlayerMovement.pc.Movimento.enabled)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(5, gameObject);
                    GetComponent<Animator>().SetTrigger("TALKING");
                }
            } else if (!GameObject.Find("Encapuzado"))
            {
                notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_ellipsis;
                float dist = Vector2.Distance(target.transform.position, transform.position);
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3 && PlayerMovement.pc.Movimento.enabled)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneComplex(7, gameObject);
                    GetComponent<Animator>().SetTrigger("TALKING");
                }
            }

        }
    }

    private IEnumerator OpenDoor()
    {
        PlayerMovement.DisableControl();
        GetComponent<Animator>().SetTrigger("IDLEX");
        yield return new WaitForSeconds(0.5f);
        ManagerOfScenes.instance.ShowSecondPhase();
        yield return new WaitForSeconds(1f);
        notif_balloon.GetComponent<SpriteRenderer>().sprite = notif_newquest;
    }

    private IEnumerator ChangeBalloon(Sprite notifIcon)
    {
        notif_balloon.GetComponent<SpriteRenderer>().sprite = notifIcon;
        yield return null;
    }

    private IEnumerator GiveArmor()
    {
        Inventory_Manager.instance.itemStorage.Add(9);
        yield return null;
    }
}

