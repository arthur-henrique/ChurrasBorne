using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ManagerOfScenes : MonoBehaviour
{
    public GameObject passado, eclipse;
    private bool clearedUm, clearedHalf;
    public static int randomTimeline;

    private void Start()
    {
        clearedUm = GameManager.instance.GetHasCleared(0);
        clearedHalf = GameManager.instance.GetHasCleared(1);


        if (gameObject.CompareTag("Tutorial"))
        {
            GameManager.instance.SetHeals(-1f, true, false);
        }
        else
        {
            GameManager.instance.SetHeals(3f, false, true);
        }

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
}
