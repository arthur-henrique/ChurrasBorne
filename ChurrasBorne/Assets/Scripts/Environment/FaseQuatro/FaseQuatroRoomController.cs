using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseQuatroRoomController : MonoBehaviour
{
    // Lista de inimigos da sala
    public List<GameObject> roomEnemies = new List<GameObject>();
    public GameObject[] portaoFrente, portaoLado; // Portão Frente = Colocar apenas o objeto principal - Portão Lado Colocar os quatro objetos
    public Collider2D roomTrigger;
    private void Start()
    {
        roomEnemies.ForEach(x => x.SetActive(false));
        for (int i = 0; i < portaoFrente.Length; i++)
        {
            portaoFrente[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
        for (int i = 0; i < portaoLado.Length; i++)
        {
            portaoLado[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            roomEnemies.ForEach(x => x.SetActive(true));
            for (int i = 0; i < portaoFrente.Length; i++)
            {
                portaoFrente[i].GetComponent<Animator>().SetTrigger("CLOSEIT");
            }
            for (int i = 0; i < portaoLado.Length; i++)
            {
                portaoLado[i].GetComponent<Animator>().SetTrigger("CLOSEIT");
            }
            roomTrigger.enabled = false;
        }
    }

    public void KilledEnemy(GameObject enemy)
    {
        if (roomEnemies.Contains(enemy))
        {
            roomEnemies.Remove(enemy);
            IsRoomCleared();
        }
    }

    public void IsRoomCleared()
    {
        if (roomEnemies.Count <= 0)
        {
            OpenRoom();
        }
    }

    public void OpenRoom()
    {
        for (int i = 0; i < portaoFrente.Length; i++)
        {
            portaoFrente[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
        for (int i = 0; i < portaoLado.Length; i++)
        {
            portaoLado[i].GetComponent<Animator>().SetTrigger("OPENIT");
        }
    }
}
