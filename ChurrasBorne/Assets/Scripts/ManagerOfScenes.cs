using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ManagerOfScenes : MonoBehaviour
{
    public static ManagerOfScenes instance;
    public GameObject passado, eclipse;
    public GameObject particleEmmy;
    private bool clearedUm, clearedHalf, clearedDois, clearedDoisHalf;
    public static int randomTimeline;
    public CinemachineVirtualCamera gate;
    public Collider2D portalUm;
    public Animator portalDois;

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
        }
        

        // Fase Um
        if(gameObject.CompareTag("FASEUM"))
        {
            if(!clearedUm && !clearedHalf)
            {
                eclipse.SetActive(false);
            }
            else if(clearedUm && !clearedHalf)
            {
                passado.SetActive(false);
                eclipse.SetActive(true);
            }
            else if(clearedUm && clearedHalf)
            {
                randomTimeline = Random.Range(1, 3);
                if (randomTimeline == 1)
                {
                    passado.SetActive(true);
                    eclipse.SetActive(false);
                }
                else if (randomTimeline == 2)
                {
                    passado.SetActive(false);
                    eclipse.SetActive(true);
                }
            }
        }

        if (gameObject.CompareTag("FASEDOIS"))
        {
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

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.GetAlive())
        {
            if (gameObject.CompareTag("Tutorial"))
            {
                // Passa a animação de morte
                // Começa a Transição de tela escura
                // Começa a Cutscene de resgate
                // Há a troca de tela (Hub)
                // Toca a Cutscene do salvador morto
                // Frames do player pegando a espada
                // Personagem fica jogavel novamente
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
}
