using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool hasRun = false;
    public LayerMask mask, bossMask;
    public Vector2 size;
    private Collider2D[] enemiesHit;
    private void OnEnable()
    {
        hasRun = false;
        if (!hasRun)
        { 
            enemiesHit = Physics2D.OverlapBoxAll(transform.position, size, 0, mask, 0, 1);
            if (enemiesHit != null)
            {
                for (int i = 0; i < enemiesHit.Length; i++)
                {
                    if (enemiesHit[i].transform.GetComponent<MobAI>() != null)
                        enemiesHit[i].transform.GetComponent<MobAI>().TakeDamage();
                    else if (enemiesHit[i].transform.GetComponent<BullAI>() != null)
                        enemiesHit[i].transform.GetComponent<BullAI>().TakeDamage();
                }
            }
            hasRun = true;
        }
        Debug.Log("Attaquei, lek");
    }
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, size);
    }

}
