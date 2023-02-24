using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ManagerOfScenes : MonoBehaviour
{
    public static ManagerOfScenes instance;
    public GameObject passado, eclipse;
    public GameObject particleEmmy;
    private bool clearedUm, clearedHalf, clearedDois, clearedDoisHalf, clearedTres, clearedTresHalf, clearedQuatro;
    public static int randomTimeline;
    public CinemachineVirtualCamera gate;
    public Collider2D portalUm;
    public Animator portalDois;
    public GameObject portalTres;
    public AudioSource audioS;
    public AudioClip questHubAudio;
    public bool isEclipse = false;

    public bool test;

    

    // Hub Cam Positions:
    public GameObject[] GateCamPos;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        clearedUm = GameManager.instance.GetHasCleared(0);
        clearedHalf = GameManager.instance.GetHasCleared(1);
        clearedDois = GameManager.instance.GetHasCleared(2);
        clearedDoisHalf = GameManager.instance.GetHasCleared(3);
        clearedTres = GameManager.instance.GetHasCleared(4);
        clearedTresHalf = GameManager.instance.GetHasCleared(5);
        clearedQuatro = GameManager.instance.GetHasCleared(6);
        if(gate != null)
            GameManager.instance.GateCamSetter(gate);

        if (gameObject.CompareTag("Tutorial"))
        {
            GameManager.instance.SetHeals(-1f, true, false);
        }
        else if (gameObject.CompareTag("HUB") && !GameManager.instance.GetHasCleared(0))
        {
            GameManager.instance.SetHeals(-1f, true, false);
        }
        else
        {
            GameManager.instance.SetHeals(3f, false, true);
        }

        // Fase Checker
        // HUB
        if (gameObject.CompareTag("HUB"))
        {
            PostProcessingControl.Instance.TurnOffVignette();
            gate.transform.position = GateCamPos[0].transform.position;
            if(!clearedUm && !clearedHalf)
            {
                StartCoroutine(ShowChurras());
            }

            if (clearedUm)
            {
                particleEmmy.SetActive(true);

                portalUm.enabled = true;
            }
            
            if (clearedDois)
            {
                portalDois.SetTrigger("ON");
            }

            if (clearedTres)
            {
                StartCoroutine(OpenFaseTres());
            }

            if(GameManager.instance.hasCompletedQuestThree)
            {
                // play the new hub song
                audioS.clip = questHubAudio;
                audioS.Play();
            }
            else
            {
                audioS.Play();
            }
            GameManager.instance.faseumBossFire = false;
            SpawnPointResetter();
        }
        

        // Fase Um
        if(gameObject.CompareTag("FASEUM"))
        {
            PostProcessingControl.Instance.TurnOffVignette();
            if (!clearedUm && !clearedHalf)
            {
                passado.SetActive(true);
                eclipse.SetActive(false);
                isEclipse = false;
            }
            else if(clearedUm && !clearedHalf)
            {
                passado.SetActive(false);
                eclipse.SetActive(true);
                isEclipse = true;
            }
            else if(clearedUm && clearedHalf)
            {
                randomTimeline = Random.Range(1, 3);
                if (randomTimeline == 1)
                {
                    passado.SetActive(true);
                    eclipse.SetActive(false);
                    isEclipse = false;
                }
                else if (randomTimeline == 2)
                {
                    passado.SetActive(false);
                    eclipse.SetActive(true);
                    isEclipse = true;
                }
            }
            GameManager.instance.EnableTheControl();
        }

        if (gameObject.CompareTag("FASEDOIS"))
        {
            PostProcessingControl.Instance.TurnOnVignette();
            PlayerMovement.instance.EnterSnowParticles();
            PlayerMovement.instance.SetFaseDois();
            if (!clearedDois && !clearedDoisHalf)
            {
                eclipse.SetActive(false);
                //FaseDoisTriggerController.Instance.SalaCincoTrigger();
                //FaseDoisTriggerController.Instance.SalaSeisTrigger();
                //FaseDoisTriggerController.Instance.SalaSeteTrigger();
                //FaseDoisTriggerController.Instance.SalaOitoTrigger();
            }
            else if (clearedDois && !clearedDoisHalf)
            {
                passado.SetActive(false);
                //FaseDoisTriggerController.Instance.SalaUmTrigger();
                //FaseDoisTriggerController.Instance.SalaDoisTrigger();
                //FaseDoisTriggerController.Instance.SalaTresTrigger();
                //FaseDoisTriggerController.Instance.SalaQuatroTrigger();
                eclipse.SetActive(true);
            }
            else if (clearedDois && clearedDoisHalf)
            {
                randomTimeline = Random.Range(1, 3);
                if (randomTimeline == 1)
                {
                    passado.SetActive(true);
                    eclipse.SetActive(false);
                    //FaseDoisTriggerController.Instance.SalaCincoTrigger();
                    //FaseDoisTriggerController.Instance.SalaSeisTrigger();
                    //FaseDoisTriggerController.Instance.SalaSeteTrigger();
                    //FaseDoisTriggerController.Instance.SalaOitoTrigger();
                }
                else if (randomTimeline == 2)
                {
                    passado.SetActive(false);
                    //FaseDoisTriggerController.Instance.SalaUmTrigger();
                    //FaseDoisTriggerController.Instance.SalaDoisTrigger();
                    //FaseDoisTriggerController.Instance.SalaTresTrigger();
                    //FaseDoisTriggerController.Instance.SalaQuatroTrigger();
                    eclipse.SetActive(true);
                }
            }
            GameManager.instance.EnableTheControl();

        }
        if (gameObject.CompareTag("FASETRES"))
        {
            PostProcessingControl.Instance.TurnOffVignette();
            if (!clearedTres && !clearedTresHalf)
            {
                passado.SetActive(true);
                eclipse.SetActive(false);
                isEclipse = false;
            }
            else if (clearedTres && !clearedTresHalf)
            {
                passado.SetActive(false);
                eclipse.SetActive(true);
                PostProcessingControl.Instance.TurnOnVignette();
                isEclipse = true;
            }
            else if (clearedTres && clearedTresHalf)
            {
                randomTimeline = Random.Range(1, 3);
                if (randomTimeline == 1)
                {
                    passado.SetActive(true);
                    eclipse.SetActive(false);
                    isEclipse = false;
                }
                else if (randomTimeline == 2)
                {
                    passado.SetActive(false);
                    eclipse.SetActive(true);
                    PostProcessingControl.Instance.TurnOnVignette();
                    isEclipse = true;
                }
            }
            GameManager.instance.EnableTheControl();
        }


    }

    private void FixedUpdate()
    {
        if(test)
        {
            test = false;
            StartCoroutine(ShowThirdPath());
        }
    }


    public void ShowFirstPhase()
    {
        StartCoroutine(ShowFirstPath());
    }
    // NPCs show stuff
    public void ShowSecondPhase()
    {
        GameManager.instance.SetHasSeenGateTwoAnim(true);
        StartCoroutine(ShowSecondPath());
    }

    public void ShowThirdPhase()
    {
        StartCoroutine(ShowThirdPath());

    }

    IEnumerator ShowFirstPath()
    {
        PlayerMovement.DisableControl();
        gate.transform.position = GateCamPos[0].transform.position;
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.GateCAM();
    }


    IEnumerator ShowSecondPath()
    {
        gate.transform.position = GateCamPos[1].transform.position;
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.GateCAM();
        yield return new WaitForSeconds(1.5f);
        portalDois.SetTrigger("ON");
    }

    IEnumerator ShowThirdPath()
    {
        gate.transform.position = GateCamPos[2].transform.position;
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.GateCAM();
        StartCoroutine(OpenFaseTres());
    }

    IEnumerator ShowChurras()
    {
        PlayerMovement.DisableControl();
        gate.transform.position = GateCamPos[3].transform.position;
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.GateCAM();
    }

    public void SpawnPointResetter()
    {
        Transition_Manager.fase1_spawn = Vector2.zero;
        Transition_Manager.fase3_spawn = Vector2.zero;
    }

    IEnumerator OpenFaseTres()
    {
        yield return new WaitForSeconds(1.5f);
        portalTres.SetActive(true);
    }
}
