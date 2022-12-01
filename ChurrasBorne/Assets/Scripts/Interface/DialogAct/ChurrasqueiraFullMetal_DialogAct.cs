using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurrasqueiraFullMetal_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

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

            if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 6)
            {
                var selec = DialogSystem.getChildGameObject(dbox.GetComponent<DialogSystem>().gameObject, "BalloonBox");
                if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
                {
                    dbox.GetComponent<DialogSystem>().db_PullUP();
                    dbox.GetComponent<DialogSystem>().db_SetSceneSimple(3);
                }
                GameManager.instance.currentHealth = 100;
                GameManager.instance.SetHeals(3, false, true);
                while (!pc.Movimento.Attack.WasPressedThisFrame()) { }
                GameManager.instance.SetHeals(3, false, true);
            }


        }
    }
}
