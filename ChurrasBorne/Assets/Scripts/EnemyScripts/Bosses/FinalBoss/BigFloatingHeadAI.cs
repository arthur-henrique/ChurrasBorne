using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFloatingHeadAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Appearing,
        Reappearing,
        EyeLasersF1,
        CrossLasersF2,
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

    public Rigidbody2D rb;
    public Animator anim;

    public float startTimeBTWEyeLasers, startTimeBTWCrossLasers, startTimeBTWScreams;
    private float timeBTWEyeLasers, timeBTWCrossLasers, timeBTWScreams;

    public int health;

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
                Debug.Log("rawr :3");
                break;
        }
    }
}
