using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    //public Transform spawnPoint, lastCheckPoint;
    private Animator playerAnimator;
    private PlayerController pc; 
    public Slider slider;
    public CinemachineVirtualCamera dft, death;

    // Health and Stuff
    public int maxHealth, currentHealth;
    private float damageCDCounter, damagetime;
    private const float DamageCD = .5f;
    private readonly float rollDmgCd = 0.3f;
    public float healsLeft;
    public bool isTut;
    public float respawnCooldown;
    private bool canTakeDamage, isAlive, hasJustDied;

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
            HealPlayer(15);
        if (pc.Tester.TKey.WasPressedThisFrame())
        {
            NextLevelSetter(Vector2.zero);
            UnityEngine.SceneManagement.SceneManager.LoadScene("FaseUm");
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
    
    public bool GetHasCleared(int fase)
    {
        return hasCleared[fase];
    }


    public void SwitchToDeathCam()
    {
        dft.Priority = 0;
        death.Priority = 1;
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
    }
}

// HealthBarFunctions

