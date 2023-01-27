using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowAddOn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerFollow.instance.SetBoolTrue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerFollow.instance.SetBoolFalse();
        }
    }
}
