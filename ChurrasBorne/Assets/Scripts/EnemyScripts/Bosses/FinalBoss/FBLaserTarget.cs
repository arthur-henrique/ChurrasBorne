using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBLaserTarget : MonoBehaviour
{
    public Transform player;
    private Vector2 target;

    public float speed;

    private bool canBeDestroyed;

    public GameObject finalBoss;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("NYA").transform;
        finalBoss = GameObject.FindGameObjectWithTag("FinalBoss");

        Vector3 fator = player.position - transform.position;

        target.x = player.position.x + fator.x;

        target.y = player.position.y + fator.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(finalBoss.GetComponent<FinalBossAI>().movingTargetIsActive == true)
        {
            canBeDestroyed = true;
        }
        if(canBeDestroyed && finalBoss.GetComponent<FinalBossAI>().movingTargetIsActive == false)
        {
            Destroy(gameObject);
        }
    }
}
