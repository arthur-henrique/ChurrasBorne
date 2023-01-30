using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;
using Cinemachine.PostFX;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public GameObject canvas; // TransitionCanvas NEEDS to be in scene
    public static GameManager instance;
    private GameObject player;
    public UnityEngine.Experimental.Rendering.Universal.Light2D ltd;
    //public Transform spawnPoint, lastCheckPoint;
    private Animator playerAnimator;
    public Animator reflAnim;
    private PlayerController pc; 
    public Slider slider;
    public CinemachineVirtualCamera dft, death, gate, boss;
    public Cinemachine.PostFX.CinemachineVolumeSettings vignette;
    public GameObject gameOverPrefab;
    public AudioSource audioSource;
    public AudioClip gateOpen;
    public GameObject clearGamePrefab;

    // Health and Stuff
    public float maxHealth, currentHealth;
    private float damageCDCounter, damagetime;
    private const float DamageCD = .5f;
    private readonly float rollDmgCd = 0.3f;
    public float healsLeft;
    public bool isTut;
    public float respawnCooldown;
    public bool canTakeDamage, hasJustDied;
    public bool isAlive;

    public string scene_detect;
    public int og_health;
    public float og_meat;
    public bool og_sword;

    public static bool isInDialog = false;
    public bool clearGame = false;

    public bool[] hasCleared; // 0 - Fase Um, 1 - Fase Um Half, 2 - Fase Dois, 3 - Fase Dois Half;
    private bool hasSeenGateTwo = false;
    private GameObject[] gameManagers;
    private bool isPoisoned = false;
    private bool isPoisonTicking = false;
    private float poisonTime;
    public ParticleSystem poison;
    private ParticleSystem.EmissionModule poisonEm;

    private float playerDamage, playerArmor;
    public bool hasBetterSword = false;
    private float swordDamage = 15f, betterSwordDamage = 25f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        pc = new PlayerController();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        SetHasCleared();
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
        canvas = GameObject.Find("TransitionCanvas"); // TransitionCanvas NEEDS to be in scene
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        damageCDCounter = DamageCD;
        canTakeDamage = true;
        isAlive = true;
        dft.Priority = 1;
        death.Priority = 0;
        boss.Priority = 0;

        poisonEm = poison.emission;
        playerArmor = 1f;
        playerDamage = 10f;
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            Destroy(gameObject);
        }
        if (scene_detect != SceneManager.GetActiveScene().name)
        {
            og_health = (int)GameManager.instance.currentHealth;
            og_meat = GameManager.instance.GetMeat();
            og_sword = GameManager.instance.GetSword();
            scene_detect = SceneManager.GetActiveScene().name;
            //print("OG HEALTH" + og_health);
        }
        if (SceneManager.GetActiveScene().name == "Hub")
        {
            var churrasTio = GameObject.Find("ChurrasTio");
            if (churrasTio)
            {
                if (GetHasCleared(0) == false)
                {
                    churrasTio.SetActive(false);
                }
                else
                {
                    churrasTio.SetActive(true);
                }
            }

            var bruxinhaKawaii = GameObject.Find("Bruxa");
            if (bruxinhaKawaii)
            {
                if (GetHasCleared(2) == false)
                {
                    bruxinhaKawaii.SetActive(false);
                }
                else
                {
                    bruxinhaKawaii.SetActive(true);
                }
            }

            if (GetHasCleared(3) == true && clearGame == false)
            {
                Instantiate(clearGamePrefab);
                PlayerMovement.DisableControl();
                clearGame = true;
            }

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


        if (pc.Tester.PKey.WasPressedThisFrame())
        {

            //SaveGame();
            Poison(1f);
        }
        if (pc.Tester.TKey.WasPressedThisFrame())
        {
            
            //LoadGame();
            //UnityEngine.SceneManagement.SceneManager.LoadScene("FaseDois");

            //TutorialTriggerController.Instance.SecondGateTriggerOut();
        }

        if (poisonTime > 0)
        {
            isPoisoned = true;
            if (!isPoisonTicking)
            {
                isPoisonTicking = true;
                StartCoroutine(PoisonTick());
            }
        }
        if (isPoisoned)
        {
            ltd.color = new Color(0.3517012f, 0.8679245f, 0.2571021f, 1f);
            poisonEm.rateOverTime = 7;
            poisonTime -= Time.deltaTime;
            if (poisonTime <= 0)
                isPoisoned= false;

        }
        if (!isPoisoned)
        {
            ltd.color = new Color(0.7745855f, 0.7125668f, 0.9056604f, 1f);
            poisonEm.rateOverTime = 0;
            isPoisonTicking = false;
        }
    }

    // Damage

    public void TakeDamage(float damage, float damageTime = DamageCD)
    {
        if (canTakeDamage && isAlive)
        {
            playerAnimator.SetTrigger("isHit");
            reflAnim.SetTrigger("isHit");
            PostProcessingControl.Instance.TurnOnCA();
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

    public void PoisonBurn()
    {
        currentHealth -= 3;
        SetHealth(currentHealth);
    }

    public void Poison(float poisonT)
    {
        poisonTime += poisonT;
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
            reflAnim.SetFloat("numberOfMeat", healsLeft);
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
        PostProcessingControl.Instance.TurnOnLens();
    }

    public bool GetCanTakeDamage()
    {
        return canTakeDamage;
    }
    public void SetHeals(float heals, bool isTutorial, bool isHoldingSword)
    {
        if (playerAnimator)
        {
            playerAnimator.SetFloat("numberOfMeat", heals);
            playerAnimator.SetBool("isHoldingSword", isHoldingSword);
            reflAnim.SetFloat("numberOfMeat", heals);
            reflAnim.SetBool("isHoldingSword", isHoldingSword);
        }
        if (isHoldingSword)
            HasSword();
        if(hasBetterSword)
        {
            HasBetterSword();
        }
        healsLeft = heals;
        isTut = isTutorial;
    }

    public float GetHeals()
    {
        return healsLeft;
    }
    // HealthBarFunctions
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void SetHealth(float health)
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
        //currentHealth = maxHealth;
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
        reflAnim.SetBool("isDead", false);
        reflAnim.SetBool("isDied", false);
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

    public void SetHasSeenGateTwoAnim(bool hasIt)
    {
        hasSeenGateTwo = hasIt;
    }

    public bool GetHasSeenGateTwoAnim()
    {
        return hasSeenGateTwo;
    }


    public void SwitchToDeathCam()
    {
        dft.Priority = 0;
        death.Priority = 1;
    }
    public float GetDamage()
    {
        return playerDamage;
    }
    public void HasSword()
    {
        playerDamage = swordDamage;
    }
    public void SetHasBetterSword()
    {
        hasBetterSword = true;
    }
    public void HasBetterSword()
    {
        playerDamage= betterSwordDamage;
    }
    public float GetArmor()
    {
        return playerArmor;
    }


    public void SwitchToGateCam()
    {
        dft.Priority = 0;
        gate.Priority = 1;
    }

    public void SwitchFromGateCam()
    {
        dft.Priority = 1;
        gate.Priority = 0;
        boss.Priority = 0;
    }

    public void SwitchToBossCam()
    {
        dft.Priority = 0;
        boss.Priority = 1;
    }

    public void SwitchToDefaultCam()
    {
        dft.Priority = 1;
        death.Priority = 0;
        boss.Priority = 0;
    }

    public void EndTheGame()
    {
        canvas.GetComponent<Transition_Manager>().TransitionToScene("MainMenu");
    }

    public void GateCamSetter(CinemachineVirtualCamera gateCam)
    {
        gate = gateCam;
    }
    IEnumerator DeadCounter()
    {
        yield return new WaitForSeconds(1.6f);
        playerAnimator.SetBool("isDead", false);
        reflAnim.SetBool("isDead", false);
        if (isTut)
        {
            yield return new WaitForSeconds(1f);
            SwitchToDefaultCam();
            canvas.GetComponent<Transition_Manager>().RestartScene("Tutorial", maxHealth, 0, false, null);
        }
    }

    IEnumerator PoisonTick()
    {
        while (poisonTime > 0)
        {
            yield return new WaitForSeconds(0.25f);
            PoisonBurn();
        }
    }
    IEnumerator CameraDelay()
    {
        SwitchToDeathCam();
        yield return new WaitForSeconds(1.5f);
        playerAnimator.SetBool("isDead", true);
        playerAnimator.SetBool("isDied", true);
        reflAnim.SetBool("isDead", true);
        reflAnim.SetBool("isDied", true);
        if (!isTut)
        {
            Instantiate(gameOverPrefab);
        }
    }

    // Mudar para a gate CAM
    public void GateCAM()
    {
        PlayerMovement.DisableControl();
        SwitchToGateCam();
        StartCoroutine(ReturnFromGateCam());

    }

    IEnumerator ReturnFromGateCam()
    {
        yield return new WaitForSeconds(6);
        SwitchFromGateCam();
        yield return new WaitForSeconds(1.5f);
        PlayerMovement.EnableControl();
    }


    // Teste, favor remover
    public void SaveGame()
    {
        SaveSystem.SavePlayer(GameManager.instance);
    }
    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        hasCleared[0] = data.clearedPhaseOne;
        hasCleared[1] = data.clearedPhaseOneHalf;
        hasCleared[2] = data.clearedPhaseTwo;
        hasCleared[3] = data.clearedPhaseTwoHalf;
        maxHealth = data.maxHealth;
        hasSeenGateTwo= data.hasSeenGateTwo;
        player.transform.position = new Vector2(0, 0);
    }

    private void SetHasCleared()
    {
        for (int i = 0; i < 8; i++)
        {
            hasCleared[i] = false;
        }
    }

    // Cameras Post Processing
    public void TurnVignetteOn()
    {
        PostProcessingControl.Instance.TurnOnVignette();
    }
}

// HealthBarFunctions

