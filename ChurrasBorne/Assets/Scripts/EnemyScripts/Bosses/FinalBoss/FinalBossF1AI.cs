using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossF1AI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        EyeLasers,
        CrossLasers,
        Scream,
        Dead
    }

    private State state;

    public Transform player, rightEye, leftEye;

    private Vector3 eyeLaserTarget, target;

    public LineRenderer laser;

    public Animator anim;

    public GameObject spikes;

    public int health;

    private bool isAlreadyDying = false, isShootingEyeLasers = false, isShootingCrossLasers = false;

    public bool isBreathing;

    public float screamDistance, startTimeBTWCrossLasers;
    private float timeBTWCrossLasers;

    private float yOffset = 1.7f;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rightEye = GameObject.FindGameObjectWithTag("RightEye").transform;
        leftEye = GameObject.FindGameObjectWithTag("LeftEye").transform;

        eyeLaserTarget = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);
    }

    void Update()
    {
        switch (state)
        {
            case State.Spawning:
                Debug.Log("Ayyy lmao");
                break;

            case State.EyeLasers:
                anim.SetBool("EyeLasers", true);
                anim.SetBool("Scream", false);

                if (timeBTWCrossLasers <= 0)
                {
                    anim.SetTrigger("CrossLasers");

                    timeBTWCrossLasers = startTimeBTWCrossLasers;
                }
                else
                {
                    timeBTWCrossLasers -= Time.deltaTime;
                }

                SwitchToScream();
                break;

            case State.Scream:
                anim.SetBool("Scream", true);
                anim.SetBool("EyeLasers", false);

                SwitchToEyeLasers();
                break;

            case State.Dead:
                isAlreadyDying = true;
                isBreathing = false;

                anim.SetBool("EyeLasers", true);
                anim.SetBool("Scream", false);
                anim.SetTrigger("Die");
                break;

        }

        if (!isShootingEyeLasers)
        {
            target = eyeLaserTarget;

            Vector3 fator = eyeLaserTarget - transform.position;

            target.x = eyeLaserTarget.x + fator.x * 2;

            target.y = eyeLaserTarget.y + fator.y * 2;

            laser.enabled = false;
        }

        if (isShootingEyeLasers)
        {
            RaycastHit2D hitInfoRight = Physics2D.Raycast(rightEye.position, target);

            if (hitInfoRight)
            {
                GameManager gameManager = hitInfoRight.transform.GetComponent<GameManager>();

                if (gameManager != null)
                {
                    GameManager.instance.TakeDamage(10);
                }
            }

            laser.SetPosition(0, rightEye.position);
            laser.SetPosition(1, hitInfoRight.point);

            RaycastHit2D hitInfoLeft = Physics2D.Raycast(leftEye.position, target);

            if (hitInfoLeft)
            {
                GameManager gameManager = hitInfoLeft.transform.GetComponent<GameManager>();

                if (gameManager != null)
                {
                    GameManager.instance.TakeDamage(10);
                }
            }

            laser.SetPosition(0, leftEye.position);
            laser.SetPosition(1, hitInfoLeft.point);

            laser.enabled = true;
        }
    }

    void BeginCombat()
    {
        SwitchToEyeLasers();
        SwitchToScream();
    }

    void SwitchToEyeLasers()
    {
        if (Vector2.Distance(transform.position, player.position) > screamDistance && health > 0)
        {
            state = State.EyeLasers;
        }
    }
    void SwitchToScream()
    {
        if (Vector2.Distance(transform.position, player.position) <= screamDistance && health > 0)
        {
            state = State.Scream;
        }
    }
    void SwitchToDead()
    {
        if (health <= 0)
        {
            state = State.Dead;
        }
    }

    void SummonDrills()
    {
        Instantiate(spikes, transform.position, Quaternion.identity);

        if (Vector2.Distance(transform.position, player.position) <= screamDistance)
        {
            GameManager.instance.TakeDamage(15);
        }
    }
    void EyeLasersOn()
    {
        isShootingEyeLasers = true;
    }
    void EyeLasersOff()
    {
        isShootingEyeLasers = false;
    }

    public void TakeDamage()
    {
        gameObject.GetComponent<ColorChanger>().ChangeColor();
        int damage = 10;
        health -= damage;

        if (!isAlreadyDying)
        {
            SwitchToDead();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
