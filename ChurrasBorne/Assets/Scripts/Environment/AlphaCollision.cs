using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaCollision : MonoBehaviour
{
    public float opacity;
    private SpriteRenderer sr;
    private Color originalAlpha;
    public GameObject[] treeParts;
    // Start is called before the first frame update
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        originalAlpha = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i < treeParts.Length; i++)
            {
                treeParts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < treeParts.Length; i++)
            {
                treeParts[i].GetComponent<SpriteRenderer>().color = originalAlpha;
            }
        }
    }
}
