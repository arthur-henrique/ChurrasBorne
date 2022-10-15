using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    //public Transform spawnPoint, lastCheckPoint;
    private Animator playerAnimator;
    private PlayerController pc; 
    public Slider slider;

    // Health and Stuff
    public int maxHealth, currentHealth;
    private float damageCD, damageCDCounter;
    public float respawnCooldown;
    private bool canTakeDamage, isAlive;

    public bool[] hasCleared;
    private GameObject[] gameManagers; 
    private void Awake()
    {
        DontDestroyOnLoad(this);
        pc = new PlayerController();
    }
    private void OnEnable()
    {
        pc.Enable();
    }
    private void OnDisable()
    {
        pc.Disable();
    }
    private void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        damageCD = 1.5f;
        damageCDCounter = damageCD;
        canTakeDamage = true;
        isAlive = true;
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

        if (pc.Tester.LKey.WasPressedThisFrame())
            TakeDamage(3);
        if (pc.Tester.PKey.WasPressedThisFrame())
            HealPlayer(2);
    }

    // Damage

    public void TakeDamage(int damage)
    {
        Debug.Log("damage");
        if (canTakeDamage && isAlive)
        {
            playerAnimator.SetTrigger("isHit");
            damageCDCounter = damageCD;
            canTakeDamage = false;
            currentHealth -= damage;
            SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                isAlive = false;
            }
        }
    }
    public void HealPlayer(int healValue)
    {
        if (isAlive)
        {
            //playerAnimator.SetTrigger("Healed");
            currentHealth += healValue;
            if (currentHealth >= maxHealth)
                currentHealth = maxHealth;
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

    private void OnLevelWasLoaded(int level)
    {
        gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers.Length > 1)
            Destroy(gameManagers[1]);
    }
    public void NextLevelSetter(Vector2 spawn)
    {
        player.transform.position = spawn;
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

