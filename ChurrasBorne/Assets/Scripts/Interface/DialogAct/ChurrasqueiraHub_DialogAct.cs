using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurrasqueiraHub_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public Collider2D col;
    private bool hasShownPath;

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
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetFloat("numberOfMeat") < 0)
                        {
                            dbox.GetComponent<DialogSystem>().db_SetSceneSimple(2);
                        } else
                        {
                            dbox.GetComponent<DialogSystem>().db_SetSceneSimple(5);
                        }
                    }
                    GameManager.instance.currentHealth = 100;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("numberOfMeat", 3);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isHoldingSword", true);
                    GameManager.instance.SetHeals(3, false, true);

                    //
                    col.enabled = true;
                    if(!hasShownPath)
                    {
                        hasShownPath = true;
                        ManagerOfScenes.instance.ShowFirstPhase();
                        StartCoroutine(ActivatePortal());
                    }
                    

                    while (!pc.Movimento.Attack.WasPressedThisFrame()) { }

                }
            }


        }
    }

    IEnumerator ActivatePortal()
    {
        yield return new WaitForSeconds(1.5f);
        col.gameObject.GetComponent<Animator>().SetTrigger("ON");
    }
}
