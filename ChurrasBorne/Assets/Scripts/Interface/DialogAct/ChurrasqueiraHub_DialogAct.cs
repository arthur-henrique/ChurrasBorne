using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurrasqueiraHub_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public GameObject tutboxmeat;

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
                if (PlayerMovement.pc.Movimento.enabled)
                {
                    if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 4)
                    {
                        if (GameManager.instance.healsLeft < 0)
                        {
                            if(GameManager.instance.HasFlask())
                            {
                                dbox.GetComponent<DialogSystem>().db_SetSceneSimple(6);
                            } else
                            {
                                dbox.GetComponent<DialogSystem>().db_SetSceneSimple(2);
                            }
                            
                        }
                        else
                        {
                            if (GameManager.instance.HasFlask())
                            {
                                dbox.GetComponent<DialogSystem>().db_SetSceneSimple(6);
                            }
                            else
                            {
                                dbox.GetComponent<DialogSystem>().db_SetSceneSimple(5);
                            }
                        }
                        
                        if (GameManager.instance.HasFlask())
                        {
                            GameManager.instance.FlaskRefill();
                        }
                        GameManager.instance.currentHealth = GameManager.instance.maxHealth;
                        GameManager.instance.SetHeals(3, false, true);
                        

                        //
                        col.enabled = true;
                        if (!hasShownPath && !GameManager.instance.GetHasCleared(0))
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
    }

    IEnumerator ActivatePortal()
    {
        col.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(8f);
        Instantiate(tutboxmeat);
    }
}
