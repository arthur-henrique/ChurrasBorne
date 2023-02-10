using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempPowerUps : MonoBehaviour
{
    private float standardSpeed = 10f,
    targetedSpeed = 15f;
    

    private void OnEnable()
    {
        gameObject.GetComponent<PlayerMovement>().speed = targetedSpeed;
        GameManager.instance.HasBetterSword();
        StartCoroutine(TimeIsOut());

    }

    private IEnumerator TimeIsOut()
    {
        yield return new WaitForSeconds(60f);
        gameObject.GetComponent<PlayerMovement>().speed = standardSpeed;
        GameManager.instance.HasSword();
        this.enabled = false;
    }
}
