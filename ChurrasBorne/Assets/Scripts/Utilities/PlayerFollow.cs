using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public static PlayerFollow instance;
    [SerializeField] RectTransform gameObj;
    [SerializeField] Transform player;
    [SerializeField] float thresholdNear;
    [SerializeField] float thresholdFar;
    public bool isPlayerTooClose = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Awake()
    {
        instance= this;
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        startPosition = this.transform.position;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(!isPlayerTooClose)
        {
            MoveTheImage();
        }
    }

    private void MoveTheImage()
    {
        targetPosition = (player.position / 2 + gameObj.transform.position) / 2f;


        targetPosition.x = Mathf.Clamp(targetPosition.x, -thresholdFar + gameObj.transform.position.x, thresholdFar + gameObj.transform.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -thresholdFar + gameObj.transform.position.y, thresholdFar + gameObj.transform.position.y);

        this.transform.position = targetPosition;
        //float distance = Vector3.Distance(gameObj.transform.position, player.position);
        //distance /= 15f;
        //if (player.position.x > gameObj.position.x)
        //{
        //    this.transform.position = targetPosition * distance;
        //}
        //else if(player.position.x < gameObj.transform.position.x)
        //{
        //    distance = -distance;
        //    this.transform.position = targetPosition * distance;
        //    print(targetPosition * distance);
        //}
        //if (distance > 1)
        //    distance = 1;
        //if (distance < -1)
        //    distance = -1;



    }

    public void SetBoolTrue()
    {
        isPlayerTooClose = true;
    }

    public void SetBoolFalse()
    {
        isPlayerTooClose = false;
        this.transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }
}
