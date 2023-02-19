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
                FaseUmTriggerController.Instance.SecondGateOpen();
                coll.enabled = true;
                coll.transform.GetChild(0).gameObject.SetActive(true);
                GameManager.instance.SetHasCleared(1, true);
            }
            if(IsOnFaseDois)
            {
                hasRun = true;
                FaseDoisTriggerController.Instance.GateOpener();
                faseDoisHalf.SetTrigger("ON");
                GameManager.instance.SetHasCleared(3, true);
            }
            if(isOnFaseTres)
            {
                if(!ManagerOfScenes.instance.isEclipse)
                {
                    hasRun = true;
                    FaseTresTriggerController.Instance.GateOpener();
                    // Ativa o Portal para o Hub
                    GameManager.instance.SetHasCleared(4, true);
                }
                else
                {
                    hasRun = true;
                    FaseTresTriggerController.Instance.GateOpener();
                    // Ativa o Portal para o Hub
                    GameManager.instance.SetHasCleared(5, true);
                }
            }
            

        }
    }

}
