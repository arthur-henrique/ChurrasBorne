using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempPowerUps : MonoBehaviour
{
    private float standardSpeed = 10f,
    targetedSpeed = 15f;
    public GameObject particles;
    

    private void OnEnable()
    {
        gameObject.GetComponent<PlayerMovement>().speed = targetedSpeed;
        GameManager.instance.HasBetterSword();
        particles.SetActive(true);
        StartCoroutine(TimeIsOut());

    }

    private IEnumerator TimeIsOut()
    {
        yield return new WaitForSeconds(60f);
        gameObject.GetComponent<PlayerMovement>().speed = standardSpeed;
        GameManager.instance.HasSword();
        particles.SetActive(false);
        this.enabled = false;
    }
}
