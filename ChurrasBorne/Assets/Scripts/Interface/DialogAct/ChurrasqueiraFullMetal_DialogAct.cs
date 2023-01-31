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
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float dist = Vector2.Distance(target.transform.position, transform.position);
            //print("Distance to other: " + dist);

            if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 4)
            {
                dbox.GetComponent<DialogSystem>().db_SetSceneSimple(3);
                GameManager.instance.currentHealth = GameManager.instance.maxHealth;
                GameManager.instance.SetHeals(3, false, true);
                while (!pc.Movimento.Attack.WasPressedThisFrame()) { }
                GameManager.instance.SetHeals(3, false, true);
            }


        }
    }
}
