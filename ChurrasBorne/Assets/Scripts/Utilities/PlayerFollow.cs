using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] GameObject gameObj;
    [SerializeField] Transform player;
    [SerializeField] float threshold;

    private Vector3 startPosition;


    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = (player.position + startPosition) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + gameObj.transform.position.x, threshold + gameObj.transform.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -threshold + gameObj.transform.position.y, threshold + gameObj.transform.position.y);

        this.transform.position = targetPosition;
    }
}
