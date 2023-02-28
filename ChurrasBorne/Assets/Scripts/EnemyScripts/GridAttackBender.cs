using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAttackBender : MonoBehaviour
{
    public static GridAttackBender instance;
    public GameObject wholeGrid;
    public GameObject[] verticalGrid;
    public GameObject[] horizontalGrid;

    public GameObject rogueVertical, rogueHorizontal;
    private Transform playerTransform;

    [SerializeField] private float rogueTimer, matchTimer, gridTimer;
    private float rogueTimerCheck, matchTimerCheck, gridTimerCheck;
    private bool isTotalGridHappening, hasGottenPlayerPos, hasDecidedMatch;
    private int randomHorizontal, randomVertical;
    public bool isFromArmor, isFromTrickster;
    public ArmorAI armor;
    public TricksterAI trickster;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rogueTimerCheck = rogueTimer;
        matchTimerCheck = matchTimer;
        gridTimerCheck = gridTimer;
    }

    private void FixedUpdate()
    {
        // Manages Rogue Attack
        if (rogueTimerCheck > 0f)
        {
            rogueTimerCheck -= Time.deltaTime;
            if (rogueTimerCheck <= 0f)
            {
                StartCoroutine(RogueAttack());
            }
        }
        // Manages Random Match
        // Armor
        if(isFromArmor)
        {
            if (matchTimerCheck > 0 && armor.canMatch)
            {
                matchTimerCheck -= Time.deltaTime;
                if (matchTimerCheck <= 0)
                {
                    if (!hasDecidedMatch)
                    {
                        hasDecidedMatch = true;
                        randomHorizontal = Random.Range(0, horizontalGrid.Length);
                        randomVertical = Random.Range(0, verticalGrid.Length);
                    }

                    StartCoroutine(MatchAttack(randomHorizontal, randomVertical));
                }
            }
            // Manages Grid
            if (gridTimerCheck > 0 && armor.canGrid)
            {
                gridTimerCheck -= Time.deltaTime;

                if (gridTimerCheck <= 0)
                {
                    StartCoroutine(GridAttack());
                }
            }
        }
        else if(isFromTrickster)
        {
            if (matchTimerCheck > 0 && trickster.canMatch)
            {
                matchTimerCheck -= Time.deltaTime;
                if (matchTimerCheck <= 0)
                {
                    if (!hasDecidedMatch)
                    {
                        hasDecidedMatch = true;
                        randomHorizontal = Random.Range(0, horizontalGrid.Length);
                        randomVertical = Random.Range(0, verticalGrid.Length);
                    }

                    StartCoroutine(MatchAttack(randomHorizontal, randomVertical));
                }
            }
            // Manages Grid
            if (gridTimerCheck > 0 && trickster.canGrid)
            {
                gridTimerCheck -= Time.deltaTime;

                if (gridTimerCheck <= 0)
                {
                    StartCoroutine(GridAttack());
                }
            }
        }
        
    }

    IEnumerator RogueAttack()
    {
        if(!hasGottenPlayerPos)
        {
            rogueHorizontal.transform.position = new Vector3(rogueHorizontal.transform.position.x, playerTransform.position.y, rogueHorizontal.transform.position.z);
            rogueVertical.transform.position = new Vector3(playerTransform.position.x, rogueVertical.transform.position.y, rogueVertical.transform.position.z);
            hasGottenPlayerPos = true;
        }
        
        rogueHorizontal.SetActive(true);
        rogueVertical.SetActive(true);
        yield return new WaitForSeconds(3f);
        rogueTimerCheck = rogueTimer;
        rogueHorizontal.SetActive(false);
        rogueVertical.SetActive(false);
        hasGottenPlayerPos = false;

    }

    IEnumerator MatchAttack(int horizontal, int vertical)
    {
        horizontalGrid[horizontal].SetActive(true);
        verticalGrid[vertical].SetActive(true);

        yield return new WaitForSeconds(3f);
        matchTimerCheck = matchTimer;
        hasDecidedMatch = false;
        horizontalGrid[horizontal].SetActive(false);
        verticalGrid[vertical].SetActive(false);
    }

    IEnumerator GridAttack()
    {
        wholeGrid.SetActive(true);
        yield return new WaitForSeconds(3f);
        gridTimerCheck = gridTimer;
        wholeGrid.SetActive(false);
        isTotalGridHappening = false;
    }
}
