using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Tester"))
            {
                GameManager.instance.NextLevelSetter(Vector2.zero);
                SceneManager.LoadScene("Tutorial");
            }

            if (gameObject.CompareTag("ParaHub"))
            {
                GameManager.instance.NextLevelSetter(Vector2.zero);
                SceneManager.LoadScene("Hub");
            }

            if (gameObject.CompareTag("PortaUm"))
            {
                GameManager.instance.NextLevelSetter(Vector2.zero);
                SceneManager.LoadScene("FaseUm");
                //if (!GameManager.instance.hasCleared[0])
                //{
                //    GameManager.instance.NextLevelSetter(Vector2.zero);
                //    SceneManager.LoadScene("FaseUm");
                //}
                //if (GameManager.instance.hasCleared[0] && !GameManager.instance.hasCleared[1])
                //{
                //    Vector2 halfPos = new Vector2(1000f, 0f);
                //    GameManager.instance.NextLevelSetter(halfPos);
                //    SceneManager.LoadScene("FaseUm");
                //}
                //if (GameManager.instance.hasCleared[0] && GameManager.instance.hasCleared[1])
                //{
                //    int randomTimeline;
                //    randomTimeline = Random.Range(1, 3);
                //    print(randomTimeline);
                //    if (randomTimeline == 1)
                //    {
                //        GameManager.instance.NextLevelSetter(Vector2.zero);
                //        SceneManager.LoadScene("FaseUm");
                //    }
                //    else if (randomTimeline == 2)
                //    {
                //        Vector2 halfPos = new Vector2(1000f, 0f);
                //        GameManager.instance.NextLevelSetter(halfPos);
                //        SceneManager.LoadScene("FaseUm");
                //    }
                //}
            }
        }
    }
}
