using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public static ColorChanger instance;
    public Color startColor, endColor;
    public float speed;
    private float startTime;

    private void Start()
    {
        startTime = 0.25f;
    }

    public void ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, speed);
        StartCoroutine(BackToStartColor());

    }

    IEnumerator BackToStartColor()
    {
        yield return new WaitForSeconds(startTime);
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(endColor, startColor, speed);
    }
}
