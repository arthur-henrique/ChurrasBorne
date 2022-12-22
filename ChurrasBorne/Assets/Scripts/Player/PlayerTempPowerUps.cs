using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempPowerUps : MonoBehaviour
{
    private float standardSpeed = 10;
    private float targetedSpeed = 15;

    private void OnEnable()
    {
        gameObject.GetComponent<PlayerMovement>().speed = targetedSpeed;
        StartCoroutine(TimeIsOut());

    }

    private IEnumerator TimeIsOut()
    {
        yield return new WaitForSeconds(15f);
        gameObject.GetComponent<PlayerMovement>().speed = standardSpeed;
        this.enabled = false;
    }
}
