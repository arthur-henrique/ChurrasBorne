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
        //hasRun = false;
        //if (!hasRun)
        //{ 
        //    enemiesHit = Physics2D.OverlapBoxAll(transform.position, size, 0, mask, 0, 1);
        //    if (enemiesHit != null)
        //    {
        //        for (int i = 0; i < enemiesHit.Length; i++)
        //        {
        //            if (enemiesHit[i].transform.GetComponent<MobAI>() != null)
        //                enemiesHit[i].transform.GetComponent<MobAI>().TakeDamage();
        //            else if (enemiesHit[i].transform.GetComponent<BullAI>() != null)
        //                enemiesHit[i].transform.GetComponent<BullAI>().TakeDamage();
        //            else if (enemiesHit[i].transform.GetComponent<ArmorAI>() != null)
        //                enemiesHit[i].transform.GetComponent<ArmorAI>().TakeDamage();
        //            else if (enemiesHit[i].transform.GetComponent<GoatAI>() != null)
        //                enemiesHit[i].transform.GetComponent<GoatAI>().TakeDamage();
        //            else if (enemiesHit[i].transform.GetComponent<TricksterAI>() != null)
        //                enemiesHit[i].transform.GetComponent<TricksterAI>().TakeDamage();
        //            else if (enemiesHit[i].transform.GetComponent<Projectile>() != null)
        //                enemiesHit[i].transform.GetComponent<Projectile>().TakeDamage();
        //            else if (enemiesHit[i].transform.GetComponent<CEOofSpidersAI>() != null)
        //                enemiesHit[i].transform.GetComponent<CEOofSpidersAI>().TakeDamage();
        //            else if (enemiesHit[i].transform.GetComponent<FinalBossF1AI>() != null)
        //                enemiesHit[i].transform.GetComponent<FinalBossF1AI>().TakeDamage();
        //        }
        //    }
        //    hasRun = true;
        //}
        //Debug.Log("Attaquei, lek");
    }

    private void FixedUpdate()
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
                else if (enemiesHit[i].transform.GetComponent<ArmorAI>() != null)
                    enemiesHit[i].transform.GetComponent<ArmorAI>().TakeDamage();
                else if (enemiesHit[i].transform.GetComponent<GoatAI>() != null)
                    enemiesHit[i].transform.GetComponent<GoatAI>().TakeDamage();
                else if (enemiesHit[i].transform.GetComponent<TricksterAI>() != null)
                    enemiesHit[i].transform.GetComponent<TricksterAI>().TakeDamage();
                else if (enemiesHit[i].transform.GetComponent<Projectile>() != null)
                    enemiesHit[i].transform.GetComponent<Projectile>().TakeDamage();
                else if (enemiesHit[i].transform.GetComponent<CEOofSpidersAI>() != null)
                    enemiesHit[i].transform.GetComponent<CEOofSpidersAI>().TakeDamage();
                else if (enemiesHit[i].transform.GetComponent<FinalBossAI>() != null)
                    enemiesHit[i].transform.GetComponent<FinalBossAI>().TakeDamage();
            }
        }
        hasRun = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, size);
    }

}
