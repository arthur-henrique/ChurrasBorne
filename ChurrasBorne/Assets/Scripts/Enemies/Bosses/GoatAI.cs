using System.Collections;
using System.Collections.Generic;
using System.Resources;
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

    private bool mustStomp, mustDash, mustDashAttack, mustSummonSpikes;

    public float walkingSpeed, dashingSpeed;

    public float agroDistance, stompDistance, dashDistance;

    public float startSpawnTime, startDashTime, startStompTime;
    private float spawnTime, dashTime, stompTime;

    public int maxHealth;
    int currentHealth;

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

        dashTime = startDashTime;

        stompTime = startStompTime;

        currentHealth = maxHealth;
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
                anim.SetBool("Walking", true);
                Vector2.MoveTowards(transform.position, player.position, walkingSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, player.position) <= stompDistance)
                {
                    state = State.Stomping;
                }
                if(Vector2.Distance(transform.position, player.position) > dashDistance)
                {
                    dashTime -= Time.deltaTime;
                }
                if (dashTime <= 0)
                {
                    state = State.Dashing;

                    dashTime = startDashTime;
                }
                if(currentHealth == 50)
                {
                    state = State.SummoningSpikes;
                }
                break;
            case State.Stomping:
                rb.velocity = Vector2.zero;
                if(stompTime <0)
                {
                    GameManager.instance.TakeDamage(20);

                    stompTime = startStompTime;
                }
                else
                {
                    stompTime -= Time.deltaTime;    
                }
                break;
        }
    }
}
