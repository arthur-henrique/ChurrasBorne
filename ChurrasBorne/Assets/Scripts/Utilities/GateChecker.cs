using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateChecker : MonoBehaviour
{
    public static GateChecker Instance;
    public bool isOnFaseUm = false, IsOnFaseDois = false, isOnFaseTres = false;
    public bool isTheBossDead = false, areTheMobsDead = false, hasRun = false;
    public Animator faseDoisHalf;
    public Collider2D coll;
    private ManagerOfScenes manager;
    public GameObject ferramentas, gelo, astrolabio;


    private void Start()
    {
        manager = FindObjectOfType<ManagerOfScenes>();
        isTheBossDead = false;
        areTheMobsDead = false;
    }
    public void TheBossDied()
    {
        isTheBossDead = true;
    }

    public void MobsDied()
    {
        areTheMobsDead = true;
    }

    private void FixedUpdate()
    {
        if(isTheBossDead && areTheMobsDead && !hasRun)
        {
            if(isOnFaseUm)
            {
                hasRun = true;
                if (!GameManager.instance.hasCompletedQuestOne)
                {
                    ferramentas.SetActive(true);
                    GameManager.instance.SetHasCleared(1, true);
                }
                else if(GameManager.instance.hasCompletedQuestOne)
                {
                    FaseUmOpenRoutine();
                    GameManager.instance.SetHasCleared(1, true);
                }
            }
            if(IsOnFaseDois)
            {
                hasRun = true;
                if (!GameManager.instance.hasCompletedQuestTwo)
                {
                    gelo.SetActive(true);
                    GameManager.instance.SetHasCleared(3, true);
                }
                else if (GameManager.instance.hasCompletedQuestTwo)
                {
                    FaseDoisOpenRoutine();
                    GameManager.instance.SetHasCleared(3, true);
                }
            }
            if(isOnFaseTres)
            {
                if(!manager.isEclipse)
                {
                    hasRun = true;
                    FaseTresTriggerController.Instance.GateOpener();
                    // Ativa o Portal para o Hub
                    GameManager.instance.SetHasCleared(4, true);
                }
                else
                {
                    hasRun = true;
                    if (!GameManager.instance.hasCompletedQuestThree)
                    {
                        astrolabio.SetActive(true);
                        GameManager.instance.SetHasCleared(5, true);
                    }
                    else if (GameManager.instance.hasCompletedQuestThree)
                    {
                        FaseTresTriggerController.Instance.GateOpener();
                        GameManager.instance.SetHasCleared(5, true);
                    }
                    //FaseTresTriggerController.Instance.GateOpener();
                    // Ativa o Portal para o Hub

                }
            }
            

        }
    }
    public void FaseUmOpenRoutine()
    {
        FaseUmTriggerController.Instance.SideSecondGateOpen();
        coll.enabled = true;
        coll.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void FaseDoisOpenRoutine()
    {
        FaseDoisTriggerController.Instance.GateOpener();
        faseDoisHalf.SetTrigger("ON");
    }

    public void SetFerramentasPos(Vector2 pos)
    {
        ferramentas.transform.position = pos;
    }
    public void SetGeloPos(Vector2 pos)
    {
        gelo.transform.position = pos;
    }
    public void SetAstrolabePos(Vector2 pos)
    {
        astrolabio.transform.position = pos;
    }

}
