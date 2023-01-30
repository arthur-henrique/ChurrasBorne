using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPortal : MonoBehaviour
{
    public GameObject canvas; // TransitionCanvas NEEDS to be in scene

    private void Start()
    {
        canvas = GameObject.Find("TransitionCanvas"); // TransitionCanvas NEEDS to be in scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Tester"))
            {
                canvas.GetComponent<Transition_Manager>().TransitionToScene("Tutorial");
                PlayerMovement.DisableControl();
            }

            if (gameObject.CompareTag("ParaHub"))
            {
                canvas.GetComponent<Transition_Manager>().TransitionToScene("Hub");
                PlayerMovement.DisableControl();
            }

            if (gameObject.CompareTag("PortaUm"))
            {
                canvas.GetComponent<Transition_Manager>().TransitionToScene("FaseUm");
                PlayerMovement.DisableControl();
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

            if (gameObject.CompareTag("PortaDois"))
            {
                canvas.GetComponent<Transition_Manager>().TransitionToScene("FaseDois");
                PlayerMovement.DisableControl();
            }
        }
    }
}
