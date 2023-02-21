using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ManagerOfScenes : MonoBehaviour
{
    public static ManagerOfScenes instance;
    public GameObject passado, eclipse;
    public GameObject particleEmmy;
    private bool clearedUm, clearedHalf, clearedDois, clearedDoisHalf, clearedTres, clearedTresHalf;
    public static int randomTimeline;
    public CinemachineVirtualCamera gate;
    public Collider2D portalUm;
    public Animator portalDois;
    public AudioSource audioS;
    public AudioClip questHubAudio;
    public bool isEclipse = false;

    

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
            if (clearedUm && !clearedHalf && !GameManager.instance.GetHasSeenGateTwoAnim())
            {
                GameManager.instance.SetHasSeenGateTwoAnim(true);
                StartCoroutine(ShowSecondPath());
                portalUm.enabled = true;
                particleEmmy.SetActive(true);
            }
            else if (clearedUm && GameManager.instance.GetHasSeenGateTwoAnim())
            {
                particleEmmy.SetActive(true);
                portalUm.enabled = true;
                portalDois.SetTrigger("ON");
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
        }
        if(gameObject.CompareTag("FASETRES"))
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
        }

    }


    public void ShowFirstPhase()
    {
        StartCoroutine(ShowFirstPath());
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

    IEnumerator ShowChurras()
    {
        PlayerMovement.DisableControl();
        gate.transform.position = GateCamPos[2].transform.position;
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.GateCAM();
    }

    public void SpawnPointResetter()
    {
        Transition_Manager.fase1_spawn = Vector2.zero;
        Transition_Manager.fase3_spawn = Vector2.zero;
    }
}
