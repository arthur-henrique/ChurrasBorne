using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = (player.position + mousePos) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + player.position.x, threshold + player.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPosition;
    }
}
