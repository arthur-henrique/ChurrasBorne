using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool hasRun = false;
    public LayerMask mask;
    public Vector2 size;

    private void OnEnable()
    {
        hasRun = false;
    }
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasRun)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(transform.position, size, mask);
            foreach (Collider2D inigo in enemiesHit)
            {
                if(enemiesHit != null)
                {
                    inigo.gameObject.GetComponent<TebasAI>().TakeDamage(20);
                }
            }
        }
        Debug.Log("Attaquei, lek");
        hasRun = true;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;        
    }

}
