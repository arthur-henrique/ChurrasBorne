using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Idling,
        Chasing,
        Dashing,
        DashAttack,
        Stomping,
        SummoningSpikes,
        GettingHurt,
        Dead       
    }

    public Transform player;

    public float walkingSpeed, dashingSpeed;

    public float agroDistance, stompDistance, dashDistance;

    public float startSpawnTime;
    private float spawnTime;

    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject goat;

    public static Animator anim;

    private static State state;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        spawnTime = startSpawnTime;
    }

    void Update()
    {
        switch(state)
        {
            case State.Spawning:
                anim.SetTrigger("Spawning");
                if (spawnTime <= 0)
                {
                    goat.GetComponent<CapsuleCollider2D>().enabled = true;
                    state = State.Chasing;
                }
                else
                {
                    spawnTime -= Time.deltaTime;
                }
                break;
            case State.Idling:
                anim.SetBool("Idle", true);
                rb.velocity = Vector2.zero;
                break;
            case State.Chasing:
                
                break;
        }
    }
}
