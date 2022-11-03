using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Churrasqueira_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

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
                if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 6)
                {
                    var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                    if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                    {
                        dbox.GetComponent<DialogSystem>().db_PullUP();
                        dbox.GetComponent<DialogSystem>().db_SetSceneSimple(1);
                    }
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("numberOfMeat", 3);
                    DialogSystem.getChildGameObject(gameObject, "LuzChurras").SetActive(false);
                    GetComponent<Animator>().SetBool("APAGAR", true);
                }
            }
            
            
        }
    }
}
