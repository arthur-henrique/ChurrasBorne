using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurrasqueiraOneMetal_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public Sprite churrasmorta;

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
        dbox.GetComponent<DialogSystem>().db_PullDOWN();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            //print("Distance to other: " + dist);

            if (GetComponent<Animator>().GetBool("APAGAR") == false)
            {
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 4)
                {
                    var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                    if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                    {
                        dbox.GetComponent<DialogSystem>().db_PullUP();
                        dbox.GetComponent<DialogSystem>().db_SetSceneSimple(4);
                    }
                    GameManager.instance.currentHealth = GameManager.instance.maxHealth;
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetFloat("numberOfMeat") < 3)
                    {
                        GameManager.instance.SetHeals(GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetFloat("numberOfMeat") + 1, false, true);
                    }
                    DialogSystem.getChildGameObject(gameObject, "LuzChurras").SetActive(false);
                    GetComponent<Animator>().SetBool("APAGAR", true);
                    GetComponent<SpriteRenderer>().sprite = churrasmorta;
                    
                }
            }


        }
    }
}
