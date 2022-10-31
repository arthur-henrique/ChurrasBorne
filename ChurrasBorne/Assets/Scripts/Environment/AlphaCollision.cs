using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaCollision : MonoBehaviour
{
    //public float opacity;
    //private SpriteRenderer sr;
    //private Color originalAlpha;
    //public GameObject[] treeParts;
    // Start is called before the first frame update

    public Animator treeAnim, copaAnim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("TRONCO"))
                treeAnim.SetTrigger("FadeIn");
            else if (gameObject.CompareTag("COPA"))
                copaAnim.SetTrigger("FadeIn");
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("TRONCO"))
                treeAnim.SetTrigger("FadeOut");
            else if (gameObject.CompareTag("COPA"))
                copaAnim.SetTrigger("FadeOut");
        }
    }
}
