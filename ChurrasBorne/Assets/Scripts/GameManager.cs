using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    //public Transform spawnPoint, lastCheckPoint;
    private Animator playerAnimator;
    public Slider slider;

    // Health and Stuff
    public int maxHealth, currentHealth;
    private float damageCD, damageCDCounter;
    public float respawnCooldown;
    private bool canTakeDamage, isAlive;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        damageCDCounter = damageCD;
    }
    private void Update()
    {
        if (damageCDCounter > 0f)
        {
            canTakeDamage = false;
            damageCDCounter -= Time.deltaTime;
        }
        else
        {
            canTakeDamage = true;
        }
        SetHealth(currentHealth);
    }

    // Damage

    public void TakeDamage(int damage)
    {
        if (canTakeDamage && isAlive)
        {
            playerAnimator.SetTrigger("IsHit");
            damageCDCounter = damageCD;
            canTakeDamage = false;
            currentHealth -= damage;
            SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                //StartCoroutine(GameOverRoutine());
            }
        }
    }
    public void HealPlayer(int healValue)
    {
        if (isAlive)
        {
            playerAnimator.SetTrigger("Healed");
            currentHealth += healValue;
            SetHealth(currentHealth);
        }
    }
    // HealthBarFunctions
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }


    //void OnLevelWasLoaded(int level)
    //{
    //    spawnPoint.transform.position = GameObject.FindWithTag("SpawnPoint").transform.position;
    //}

    //void IsDead()
    //{
    //    isAlive = false;
    //    Destroy(player.GetComponent<PlayerMovement>());
    //    player.GetComponent<PlayerMelee>().enabled = false;
    //    player.GetComponent<PlayerRanged>().enabled = false;
    //    playerAnimator.SetBool("IsDead", true);
    //}

    //IEnumerator PlayerRespawn()
    //{
    //    yield return new WaitForSeconds(respawnCooldown);
    //    player.transform.position = spawnPoint.transform.position;
    //    currentHealth = maxHealth;
    //    healthBar.SetHealth(currentHealth);
    //    player.SetActive(true);
    //}

    //IEnumerator GameOverRoutine()
    //{
    //    IsDead();
    //    yield return new WaitForSeconds(respawnCooldown);
    //    gameOverS.Setup();
    //}
}

// HealthBarFunctions

