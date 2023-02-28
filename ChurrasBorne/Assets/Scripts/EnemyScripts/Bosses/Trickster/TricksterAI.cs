using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterAI : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Ascending,
        Chasing,
        Descendng,
        LongRange,
        CloseRange,
        Idling,
        Dead
    }
    private State state;

    public Transform player;
    public GameObject gameManager;

    public Rigidbody2D rb;
    public Animator anim;

    public GameObject closeRangeSpikes, longRangeSpikes;

    public int health;

    private bool isAlreadyDying = false;

    public bool isBreathing;

    public float chasingSpeed, chaseDistance, timeBTWLRATKs, closeRangeDistance, timeBTWCRATKs;
    private float currentTimeBTWLRATKs, currentTimeBTWCRATKs, timeToDie;
    private bool canTakeDamage = true;

    public GateChecker gc;
    public GameObject gridMaster;

    private void Awake()
    {
        state = State.Spawning;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        currentTimeBTWLRATKs = .5f;

        currentTimeBTWCRATKs = .5f;

        timeToDie = .1f;
    }

    void Update()
    {
        switch(state)
        {
            case State.Spawning:
                Flip();
                break;

            case State.Ascending:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetTrigger("Ascend");
                anim.SetBool("Float", true);
                anim.SetBool("Idle", false);
                break;

            case State.Chasing:
                Flip();

                anim.SetBool("Float", true);
                anim.SetBool("Idle", false);

                transform.position = Vector2.MoveTowards(transform.position, player.position, chasingSpeed * Time.deltaTime);

                SwitchToDead();
                break;

            case State.Descendng:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetTrigger("Descend");
                anim.SetBool("Float", true);
                anim.SetBool("Idle", false);
                break;

            case State.LongRange:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Float", false);

                if(currentTimeBTWLRATKs <= 0)
                {
                    anim.SetTrigger("LR");

                    currentTimeBTWLRATKs = timeBTWLRATKs;
                }
                else
                {
                    currentTimeBTWLRATKs -= Time.deltaTime;
                }

                SwitchToAscending();
                SwitchToCloseRange();
                break;

            case State.CloseRange:
                Flip();

                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Float", false);

                if (currentTimeBTWCRATKs <= 0)
                {
                    anim.SetTrigger("CR");

                    currentTimeBTWCRATKs = timeBTWCRATKs;
                }
                else
                {
                    currentTimeBTWCRATKs -= Time.deltaTime;
                }

                SwitchToLongRange();
                SwitchToAscending();
                break;

            case State.Dead:
                rb.velocity = Vector2.zero;

                isBreathing = false;
                isAlreadyDying = true;

                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;   

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                gc.SetAstrolabePos(gameObject.transform.position);

                if (timeToDie <= 0)
                {
                    anim.SetTrigger("Die");

                    timeToDie = 1000;
                }
                else
                {
                    timeToDie -= Time.deltaTime;
                }

                gc.isTheBossDead = true;
                gc.areTheMobsDead= true;
                gridMaster.SetActive(false);
                break;

            case State.Idling:
                rb.velocity = Vector2.zero;

                anim.SetBool("Idle", true);
                anim.SetBool("Dash", false);
                anim.SetBool("Walk", false);
                break;
        }

        if (!gameManager.GetComponent<GameManager>().isAlive)
        {
            state = State.Idling;
        }
    }

    void Flip()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void BeginCombat()
    {
        SwitchToAscending();
        SwitchToLongRange();
        SwitchToCloseRange();
    }

    void SwitchToAscending()
    {
        if (health > 0 && Vector2.Distance(transform.position, player.position) > chaseDistance)
        {
            state = State.Ascending;
        }
    }
    void SwitchToChasing()
    {
        state = State.Chasing;
    }
    void SwitchToDescending()
    {
        if (health > 0 && Vector2.Distance(transform.position, player.position) <= chaseDistance)
        {
            state = State.Descendng;
        }
    }
    void SwitchToLongRange()
    {
        if (health > 0 && Vector2.Distance(transform.position, player.position) <= chaseDistance && Vector2.Distance(transform.position, player.position) > closeRangeDistance)
        {
            state = State.LongRange;
        }
    }
    void SwitchToCloseRange()
    {
        if (health > 0 && Vector2.Distance(transform.position, player.position) <= closeRangeDistance)
        {
            state = State.CloseRange;
        }
    }
    void SwitchToDead()
    {
        if (health <= 0)
        {
            state = State.Dead;
            GameManager.instance.SwitchToDefaultCam();
        }
    }

    void ToAscendOrToBeatPlayerUp()
    {
        SwitchToAscending();
        SwitchToCloseRange();
        SwitchToLongRange();
    }

    void CloseRangeATK()
    {
        Instantiate(closeRangeSpikes, transform.position, Quaternion.identity);
    }
    void LongRangeATK()
    {
        Instantiate(longRangeSpikes, player.position, Quaternion.identity);
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            CanTakeDamageCD();
            gameObject.GetComponent<ColorChanger>().ChangeColor();
            int damage = 10;
            health -= damage;

            if (!isAlreadyDying)
            {
                SwitchToDead();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
    private IEnumerator CanTakeDamageCD()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    }
}
