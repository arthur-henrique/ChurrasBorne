using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFloatingHeadAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Disappearing,
        Reappearing,
        EyeLasersF1,
        CrossLasersF1,
        ScreamingF1,
        SwitchingFase,
        EyeLaserF2,
        CrossLaserF2,
        ScreamingF2,
        Dead
    }
    private State state;

    public Transform player;
    private Vector3 target;

    public Animator anim;

    public float startTimeBTWEyeLasers, eyeLaserDistance, startTimeBTWCrossLasers, crossLaserDistance, startTimeBTWScreams;
    private float timeBTWEyeLasers, timeBTWCrossLasers, timeBTWScreams;

    public int health;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        timeBTWEyeLasers = .5f;
        timeBTWCrossLasers = .5f;
        timeBTWScreams = .5f;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        switch (state)
        {
            case State.Spawning:
                anim.SetBool("F1", true);

                Debug.Log("Spawning");
                break;

            case State.Disappearing:
                anim.SetTrigger("F1Disappear");

                Debug.Log("Disappearing");
                break;

            case State.Reappearing:
                anim.SetTrigger("F1Reappear");

                Debug.Log("Reappearing");

                if (Vector2.Distance(transform.position, player.position) <= crossLaserDistance && health > 50)
                {
                    state = State.CrossLasersF1;
                }
                else if (Vector2.Distance(transform.position, player.position) > crossLaserDistance && Vector2.Distance(transform.position, player.position) <= eyeLaserDistance && health > 50)
                {
                    state = State.EyeLasersF1;
                }
                break;

            case State.CrossLasersF1:
                Debug.Log("CrossLasersF1");
                if (timeBTWCrossLasers <= 0)
                {
                    anim.SetTrigger("F1ATK2");

                    timeBTWCrossLasers = startTimeBTWCrossLasers;
                }
                else
                {
                    timeBTWCrossLasers -= Time.deltaTime;
                }

                if (Vector2.Distance(transform.position, player.position) > crossLaserDistance && Vector2.Distance(transform.position, player.position) <= eyeLaserDistance && health > 50)
                {
                    state = State.EyeLasersF1;
                }
                break;

            case State.EyeLasersF1:
                Debug.Log("EyeLasersF1");
                if (timeBTWEyeLasers <= 0)
                {
                    anim.SetTrigger("F1ATK1");

                    timeBTWEyeLasers = startTimeBTWEyeLasers;
                }
                else
                {
                    timeBTWEyeLasers -= Time.deltaTime;
                }
                break;
        }
    }

    void BeginCombat()
    {
        if(Vector2.Distance(transform.position, player.position) <= crossLaserDistance)
        {
            state = State.CrossLasersF1;
        }
        else if (Vector2.Distance(transform.position, player.position) > crossLaserDistance && Vector2.Distance(transform.position, player.position) <= eyeLaserDistance)
        {
            state = State.EyeLasersF1;
        }
    }

    void Reappear()
    {
        state = State.Reappearing;
    }

    void CrossLasers()
    {
        _ = Physics2D.Raycast(transform.position, Vector2.up, 30f);
    }
}
