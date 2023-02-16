using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    public bool canKnockback = false;
    public bool squareHitBox; // if false hitbox = Circle
    public LayerMask mask;
    public Vector2 size;
    public float radius;
    private Collider2D[] playerHit;
    private float knockbackDuration = 1.5f, knockbackPower = 50f;

    private void FixedUpdate()
    {
        if (squareHitBox)
        {
            playerHit = Physics2D.OverlapBoxAll(transform.position, size, 0, mask, 0, 1);
            if (playerHit != null)
            {
                for (int i = 0; i < playerHit.Length; i++)
                {
                    if (playerHit[i].transform.GetComponent<PlayerMovement>() != null)
                    {
                        print("Damage");
                        if (canKnockback && GameManager.instance.canTakeDamage == true)
                            StartCoroutine(PlayerMovement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
                        GameManager.instance.TakeDamage(damage / GameManager.instance.GetArmor());
                    }
                }
            }
        }
        else
        {
            playerHit = Physics2D.OverlapCircleAll(transform.position, radius, mask, 0, 1);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (squareHitBox)
        {
            Gizmos.DrawCube(transform.position, size);
        }
        else
        {
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}


