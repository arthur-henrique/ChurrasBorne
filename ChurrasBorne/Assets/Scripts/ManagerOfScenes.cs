using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfScenes : MonoBehaviour
{
    private void Start()
    {
        if (gameObject.CompareTag("Tutorial"))
        {
            GameManager.instance.SetHeals(-1f, true);
        }
        else if (gameObject.CompareTag("Untagged"))
        {
            GameManager.instance.SetHeals(3f, false);
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.GetAlive())
        {
            if (gameObject.CompareTag("Tutorial"))
            {
                // Passa a anima��o de morte
                // Come�a a Transi��o de tela escura
                // Come�a a Cutscene de resgate
                // H� a troca de tela (Hub)
                // Toca a Cutscene do salvador morto
                // Frames do player pegando a espada
                // Personagem fica jogavel novamente
            }
        }
    }
}
