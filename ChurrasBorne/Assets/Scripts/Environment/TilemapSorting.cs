using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TilemapSorting : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 0;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;
    private Renderer myRenderer;
    private SortingGroup sortingGroup;
    public bool isASortingGroup = false;

    private float timer;
    private float timerMax = .1f;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        if(isASortingGroup)
            sortingGroup = gameObject.GetComponent<SortingGroup>();
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer = timerMax;
            if (!isASortingGroup)
                myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
            else if (isASortingGroup)
                sortingGroup.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);


            if(runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
