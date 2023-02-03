using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossF1AI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        EyeLasers,
        Scream,
        Dead
    }

    private State state;

    public Transform player, rightEye, leftEye;

    private Vector3 eyeLaserTarget, target;

    public LineRenderer laserLeft, laserRight, superLaserLeft, superLaserRight;

    public Animator anim;

    public GameObject[] tbPoints;

    public GameObject offensiveDrills, defensiveDrills, secondFase;

    private Collider2D[] playerHit;

    public int health;

    private bool isAlreadyDying = false, isShootingEyeLasers = false, isShootingSuperLasers;

    public bool isBreathing;

    public float screamDistance;
    private float timeToReposition, randomNumber, specialATKCooldown;

    private float yOffset = 1.7f;

    public LayerMask mask;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("NYA").transform;
        rightEye = GameObject.FindGameObjectWithTag("RightEye").transform;
        leftEye = GameObject.FindGameObjectWithTag("LeftEye").transform;

        timeToReposition = 2f;
        specialATKCooldown = 5f;
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

                if(specialATKCooldown <= 0)
                {
                    randomNumber = Random.Range(1, 10);

                    if(randomNumber <=6)
                    {
                        anim.SetTrigger("OffScream");
                    }
                    if (randomNumber >6)
                    {
                        anim.SetTrigger("SuperLaser");
                    }

                    specialATKCooldown = 7f;
                }
                else
                {
                    specialATKCooldown -= Time.deltaTime;
                }

                SwitchCombatState();
                break;

            case State.Scream:
                anim.SetBool("Scream", true);
                anim.SetBool("EyeLasers", false);

                specialATKCooldown = 5f;

                timeToReposition -= Time.deltaTime;

                SwitchCombatState();
                break;

            case State.Dead:
                isBreathing = false;

                anim.SetBool("EyeLasers", true);
                anim.SetBool("Scream", false);
                anim.SetTrigger("Die");

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                break;

        }

        //MIRA DOS LASERS
        if (!isShootingEyeLasers && !isShootingSuperLasers)
        {
            eyeLaserTarget = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            target = eyeLaserTarget;

            Vector3 fator = eyeLaserTarget - transform.position;

            target.x = eyeLaserTarget.x + fator.x * 2;

            target.y = eyeLaserTarget.y + fator.y * 2;

            laserLeft.enabled = false;
            laserRight.enabled = false;
            superLaserLeft.enabled = false;
            superLaserRight.enabled = false;
        }

        //LASER NORMAL
        if (isShootingEyeLasers)
        {
            RaycastHit2D[] hitInfoRight = Physics2D.RaycastAll(rightEye.position, target, Mathf.Infinity, mask);

            if (hitInfoRight != null)
            {
                GameManager.instance.TakeDamage(1);
            }

            laserRight.SetPosition(0, rightEye.position);
            laserRight.SetPosition(1, eyeLaserTarget);

            RaycastHit2D[] hitInfoLeft = Physics2D.RaycastAll(leftEye.position, target, Mathf.Infinity, mask);

            if (hitInfoLeft != null)
            {
                GameManager.instance.TakeDamage(1);
            }
            
            laserLeft.SetPosition(0, leftEye.position);
            laserLeft.SetPosition(1, eyeLaserTarget);

            laserLeft.enabled = true;
            laserRight.enabled = true;
        }

        //SUPER LASER
        if (isShootingSuperLasers)
        {
            RaycastHit2D[] hitInfoRight = Physics2D.RaycastAll(rightEye.position, target, Mathf.Infinity, mask);

            if (hitInfoRight != null)
            {
                GameManager.instance.TakeDamage(1000);
            }

            superLaserRight.SetPosition(0, rightEye.position);
            superLaserRight.SetPosition(1, eyeLaserTarget);

            RaycastHit2D[] hitInfoLeft = Physics2D.RaycastAll(leftEye.position, target, Mathf.Infinity, mask);

            if (hitInfoLeft != null)
            {
                GameManager.instance.TakeDamage(1000);
            }

            superLaserLeft.SetPosition(0, leftEye.position);
            superLaserLeft.SetPosition(1, eyeLaserTarget);

            superLaserLeft.enabled = true;
            superLaserRight.enabled = true;
        }
    }

    //STATES
    void aeHasSpawned()
    {
        SwitchCombatState();
    }
    void SwitchCombatState()
    {
        if (Vector2.Distance(transform.position, player.position) > screamDistance && health > 0)
        {
            state = State.EyeLasers;
        }
        else
        {
            state = State.Scream;
        }
    }
    void Die()
    {
        if (health <= 0)
        {
            isAlreadyDying = true;
            state = State.Dead;
        }
    }

    //ANIMATION EVENTS
    void aeScreamATK()
    {
        if (Vector2.Distance(transform.position, player.position) <= screamDistance)
        {
            GameManager.instance.TakeDamage(15);
            Instantiate(defensiveDrills, transform.position, Quaternion.identity);
        }
        else
        {
            timeToReposition = 2f;
        }

        if(timeToReposition <= 0)
        {
            timeToReposition = 2f;
            anim.SetTrigger("Disappear");
        }
    }
    void aeOffScreamATK()
    {
        Instantiate(offensiveDrills, player.position, Quaternion.identity);
    }

    void aeEyeLasersOn()
    {
        isShootingEyeLasers = true;
    }
    void aeEyeLasersOff()
    {
        isShootingEyeLasers = false;
    }

    void aeSuperLaserOn()
    {
        isShootingSuperLasers = true;   
    }
    void aeSuperLaserOff()
    {
        isShootingSuperLasers = false;
    }

    void aeHasDisappeared()
    {
        int rand = Random.Range(0, 6);

        transform.position = tbPoints[rand].transform.position;

        anim.SetTrigger("Reappear");
    }
    void aeHasReappeared()
    {
        specialATKCooldown = 5f;
        SwitchCombatState();
    }

    //HEALTH
    public void TakeDamage()
    {
        gameObject.GetComponent<ColorChanger>().ChangeColor();
        int damage = 10;
        health -= damage;

        if (!isAlreadyDying)
        {
            Die();
        }
    }
    void aeSpawnSecondFase()
    {
        Instantiate(secondFase);
        Destroy(gameObject, .2f);
    }
}
