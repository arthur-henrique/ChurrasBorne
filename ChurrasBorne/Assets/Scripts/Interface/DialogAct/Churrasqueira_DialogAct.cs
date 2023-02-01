using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Churrasqueira_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public Sprite churrasmorta;

    [SerializeField]
    private TMP_Text _title;

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
        target = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Animator>().SetBool("APAGAR", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float dist = Vector2.Distance(target.transform.position, transform.position);
            //print("Distance to other: " + dist);

            if (GetComponent<Animator>().GetBool("APAGAR") == false)
            {
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 4)
                {
                    dbox.GetComponent<DialogSystem>().db_SetSceneSimple(1);
                    GameManager.instance.SetHeals(3, false, true);
                    DialogSystem.getChildGameObject(gameObject, "LuzChurras").SetActive(false);
                    GetComponent<Animator>().SetBool("APAGAR", true);
                    GetComponent<SpriteRenderer>().sprite = churrasmorta;
                }
            }
            
            
        }
    }
}
