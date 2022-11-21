using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    //public Transform spawnPoint, lastCheckPoint;
    private Animator playerAnimator;
    private PlayerController pc; 
    public Slider slider;
    public CinemachineVirtualCamera dft, death, boss;
    public GameObject gameOverPrefab;

    // Health and Stuff
    public int maxHealth, currentHealth;
    private float damageCDCounter, damagetime;
    private const float DamageCD = .5f;
    private readonly float rollDmgCd = 0.3f;
    public float healsLeft;
    public bool isTut;
    public float respawnCooldown;
    private bool canTakeDamage, isAlive, hasJustDied;

    public string scene_detect;
    public int og_health;
    public float og_meat;
    public bool og_sword;

    public bool[] hasCleared; // 0 - Fase Um, 1 - Fase Um Half;
    private GameObject[] gameManagers; 
    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        pc = new PlayerController();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
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
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        damageCDCounter = DamageCD;
        canTakeDamage = true;
        isAlive = true;
        dft.Priority = 1;
        death.Priority = 0;
        boss.Priority = 0;
    }
    private void Update()
    {
        if (scene_detect != SceneManager.GetActiveScene().name)
        {
            og_health = GameManager.instance.currentHealth;
            og_meat = GameManager.instance.GetMeat();
            og_sword = GameManager.instance.GetSword();
            scene_detect = SceneManager.GetActiveScene().name;
            //print("OG HEALTH" + og_health);
        }

        //print("GAME_MANAGER: " + currentHealth);

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
        {
            currentHealth = maxHealth;
        }
        if (pc.Tester.TKey.WasPressedThisFrame())
        {
            NextLevelSetter(Vector2.zero);
            UnityEngine.SceneManagement.SceneManager.LoadScene("FaseDois");
            //TutorialTriggerController.Instance.SecondGateTriggerOut();
        }
    }

    // Damage

    public void TakeDamage(int damage, float damageTime = DamageCD)
    {
        if (canTakeDamage && isAlive)
        {
            playerAnimator.SetTrigger("isHit");
            damageCDCounter = damageTime;
            SetDamagetime(damageTime);
            PlayerMovement.SetDamageState();
            canTakeDamage = false;
            currentHealth -= damage;
            SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                hasJustDied = true;
                isAlive = false;
                if (hasJustDied)
                {
                    DeathRoutine();
                }
            }
        }
    }
    public void SetDamagetime(float time)
    {
        damagetime = time;
    }
    public float GetDamagetime()
    {
        return damagetime;
    }
    public void HealPlayer(int healValue)
    {
        if (isAlive)
        {
            if(healsLeft > 0)
            {
                currentHealth += healValue;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;
            }
            healsLeft--;
            if (isTut && healsLeft < -1)
                healsLeft = -1;
            else if (!isTut && healsLeft < 0)
                healsLeft = 0;
            playerAnimator.SetFloat("numberOfMeat", healsLeft);
            print(playerAnimator.GetFloat("numberOfMeat"));
            SetHealth(currentHealth);
        }
    }

    public float GetMeat()
    {
        return playerAnimator.GetFloat("numberOfMeat");
    }
    public bool GetSword()
    {
        return playerAnimator.GetBool("isHoldingSword");
    }

    public void RollInvuln()
    {
        damageCDCounter = rollDmgCd;
        canTakeDamage = false;
    }
    public void SetHeals(float heals, bool isTutorial, bool isHoldingSword)
    {
        playerAnimator.SetFloat("numberOfMeat", heals);
        playerAnimator.SetBool("isHoldingSword", isHoldingSword);
        healsLeft = heals;
        isTut = isTutorial;
    }

    public float GetHeals()
    {
        return healsLeft;
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
    public float GetHealth()
    {
        return currentHealth;
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
    //    if (gameManagers.Length > 1)
    //    {
    //        gameManagers[0] = gameManagers[1];
    //        Destroy(gameManagers[1]);
    //    }
    //}
    public void NextLevelSetter(Vector2 spawn)
    {
        player.transform.position = spawn;
        currentHealth = maxHealth;
    }

    public void SetPlayerPosition(Vector2 position)
    {
        player.transform.position = position;
    }
    
    public void DeathRoutine()
    {
        PlayerMovement.SetDead();
        StartCoroutine(CameraDelay());
        hasJustDied = false;
        StartCoroutine(DeadCounter());
        isAlive = false;
    }
    
    public bool GetAlive()
    {
        return isAlive;
    }

    public void SetAlive()
    {
        playerAnimator.SetBool("isDead", false);
        playerAnimator.SetBool("isDied", false);
        isAlive = true;
        PlayerMovement.SetStateAlive();
    }

    public void SetHasCleared(int fase, bool cleared)
    {
        hasCleared[fase] = cleared;
    }
    
    public bool GetHasCleared(int fase)
    {
        return hasCleared[fase];
    }

    public void SwitchToDeathCam()
    {
        dft.Priority = 0;
        death.Priority = 1;
    }

    public void SwitchToBossCam()
    {
        dft.Priority = 0;
        boss.Priority = 1;
    }

    public void SwitchToDefaultCam()
    {
        dft.Priority = 1;
        boss.Priority = 0;
    }
    IEnumerator DeadCounter()
    {
        yield return new WaitForSeconds(1.6f);
        playerAnimator.SetBool("isDead", false);
    }
    IEnumerator CameraDelay()
    {
        SwitchToDeathCam();
        yield return new WaitForSeconds(1.5f);
        playerAnimator.SetBool("isDead", true);
        playerAnimator.SetBool("isDied", true);
        Instantiate(gameOverPrefab);
    }
}

// HealthBarFunctions

