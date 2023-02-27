using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEffector : MonoBehaviour
{
    private float playerX;
    public bool isReversedStairs;
    public AreaEffector2D areaE;
    [SerializeField]
    private float force = 180f;
    [SerializeField]
    private float angle = 90f;

    // Update is called once per frame
    void Update()
    {
        playerX = PlayerMovement.instance.x;
        if (!isReversedStairs)
        {
            if (playerX > 0)
            {
                areaE.forceAngle = angle;
                areaE.forceMagnitude = force;
            }
            else if (playerX < 0)
            {
                areaE.forceAngle = -angle;
                areaE.forceMagnitude = force;
            }
            else if (playerX == 0)
            {
                areaE.forceMagnitude = 0; areaE.forceAngle = 0;
            }
        }
        else if (isReversedStairs)
        {
            if (playerX > 0)
            {
                areaE.forceAngle = -angle;
                areaE.forceMagnitude = force;
            }
            else if (playerX < 0)
            {
                areaE.forceAngle = angle;
                areaE.forceMagnitude = force;
            }
            else if (playerX == 0)
            {
                areaE.forceMagnitude = 0; areaE.forceAngle = 0;
            }
        }
        
    }
}
