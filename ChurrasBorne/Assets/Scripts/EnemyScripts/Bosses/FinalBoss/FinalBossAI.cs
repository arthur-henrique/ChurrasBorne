using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAI : MonoBehaviour
{
    private enum State
    {
        EyeLasers,
        Scream,
        Dead
    }

    private State state;

    public Transform player, rightEye, leftEye, mouth;

    private Vector3 superLaserTarget, target;

    public LineRenderer superLaserLeft, superLaserRight;

    public Animator anim;

    public GameObject[] tbPoints;

    public GameObject offensiveDrills, defensiveDrills, fireBalls, secondFase, projectiles;

    private Collider2D[] playerHit;

    public int health;

    private bool isAlreadyDying = false, isShootingSuperLasers = false, canTakeDamage = true;

    public bool isBreathing, isF2;

    public float screamDistance;
    private float timeToReposition, randomNumber, specialATKCooldown;

    private float yOffset = 1.7f;

    public LayerMask mask;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("NYA").transform;

        timeToReposition = 2f;
        specialATKCooldown = 5f;
    }

    void Update()
    {
        switch (state)
        {
            case State.EyeLasers:
                anim.SetBool("EyeLasers", true);
                anim.SetBool("Scream", false);

                if (specialATKCooldown <= 0)
                {
                    randomNumber = Random.Range(2, 5);

                    if (randomNumber == 2)
                    {
                        anim.SetTrigger("OffScream");
                        randomNumber = 1;
                    }
                    if (randomNumber == 3)
                    {
                        anim.SetTrigger("SuperLaser");
                        randomNumber = 1;
                    }
                    if (!isF2 && randomNumber == 4)
                    {
                        anim.SetTrigger("OffScream");
                        randomNumber = 1;
                    }
                    if (isF2 && randomNumber == 4)
                    {
                        anim.SetTrigger("FireBalls");
                        randomNumber = 1;
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

                anim.SetTrigger("Die");

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                break;

        }

        //MIRA DOS LASERS
        
        if (!isShootingSuperLasers)
        {
            superLaserTarget = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            target = superLaserTarget;

            Vector3 fator = superLaserTarget - transform.position;

            target.x = superLaserTarget.x + fator.x;

            target.y = superLaserTarget.y + fator.y;

            superLaserLeft.enabled = false;
            superLaserRight.enabled = false;
        }      

        //SUPER LASER
        if (isShootingSuperLasers)
        {
            RaycastHit2D[] hitInfoRight = Physics2D.RaycastAll(rightEye.position, target, Mathf.Infinity, mask);

            if (hitInfoRight != null)
            {
                GameManager.instance.TakeDamage(60);
            }

            superLaserRight.SetPosition(0, rightEye.position);
            superLaserRight.SetPosition(1, target);

            RaycastHit2D[] hitInfoLeft = Physics2D.RaycastAll(leftEye.position, target, Mathf.Infinity, mask);

            if (hitInfoLeft != null)
            {
                GameManager.instance.TakeDamage(60);
            }

            superLaserLeft.SetPosition(0, leftEye.position);
            superLaserLeft.SetPosition(1, target);

            superLaserLeft.enabled = true;
            superLaserRight.enabled = true;
        }
    }

    //STATES
    void aeHasSpawned()
    {
        SwitchCombatState();
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    void SwitchCombatState()
    {
        if (Vector2.Distance(transform.position, player.position) > screamDistance && health > 0)
        {
            state = State.EyeLasers;
        }
        if (Vector2.Distance(transform.position, player.position) <= screamDistance && health > 0)
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
            Debug.Log("SUS");
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

        if (timeToReposition <= 0)
        {
            timeToReposition = 2f;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.SetTrigger("Disappear");
        }
    }
    void aeOffScreamATK()
    {
        Instantiate(offensiveDrills, player.position, Quaternion.identity);
    }
    void aeFireBallATK()
    {
        Instantiate(fireBalls, mouth.position, Quaternion.identity);
    }

    void ProjectileATK()
    {
        Instantiate(projectiles, leftEye.position, Quaternion.identity);
        Instantiate(projectiles, rightEye.position, Quaternion.identity);
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
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        SwitchCombatState();
    }

    //HEALTH
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            gameObject.GetComponent<ColorChanger>().ChangeColor();
            StartCoroutine(CanTakeDamageCD());
            int damage = 10;
            health -= damage;

            if (!isAlreadyDying)
            {
                Die();
            }
        }
    }
    void aeDie()
    {
        if(!isF2)
        {
            secondFase.SetActive(true);
        }
        
        Destroy(gameObject);
    }

    private IEnumerator CanTakeDamageCD()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    }
}
