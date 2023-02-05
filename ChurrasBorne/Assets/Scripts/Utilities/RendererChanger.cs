using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererChanger : MonoBehaviour
{
    public SpriteRenderer[] srs;
    public string upLayer = "Portoes e Paredes";
    public string downLayer = "Objects";
    private Transform player;
    public bool needXTransform = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Player"))
    //    {
    //        for (int i = 0; i < srs.Length; i++)
    //        {
    //            srs[i].sortingLayerName = upLayer;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        if (player.position.y >= this.transform.position.y)
        {
            for (int i = 0; i < srs.Length; i++)
            {
                srs[i].sortingLayerName = upLayer;
            }
        }
        else if (player.position.y < this.transform.position.y)
        {
            for (int i = 0; i < srs.Length; i++)
            {
                srs[i].sortingLayerName = downLayer;
            }
        }

        if(needXTransform)
        {
            if (player.position.x >= this.transform.position.x)
            {
                for (int i = 0; i < srs.Length; i++)
                {
                    srs[i].sortingLayerName = downLayer;
                }
            }
            else if (player.position.x < this.transform.position.x)
            {
                for (int i = 0; i < srs.Length; i++)
                {
                    srs[i].sortingLayerName = upLayer;
                }
            }
        }
    }
}
