using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateChecker : MonoBehaviour
{
    public static GateChecker Instance;
    private bool isTheBossDead = false, areTheMobsDead = false, hasRun = false;
    public Animator faseDoisHalf;

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
            hasRun = true;
            FaseDoisTriggerController.Instance.GateOpener();
            faseDoisHalf.SetTrigger("ON");
            GameManager.instance.SetHasCleared(3, true);

        }
    }

}
