using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_Test : MonoBehaviour
{
    public GameObject canvas;
    public GameObject target;
    PlayerController pc;

    private void Awake()
    {
        pc = new PlayerController();
    }
    private void OnEnable()
    {
        pc.Enable();
    }
    private void OnDisable()
    {
        pc.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("TransitionCanvas");
        //canvas.GetComponent<Transition_Manager>().TransitionToScene("Transition_Test2");
    }

    // Update is called once per frame
    void Update()
    {


            if (pc.Movimento.Attack.WasPressedThisFrame())
            {
                canvas.GetComponent<Transition_Manager>().TransitionToScene("TransitionTest_2");
            }

    }
}
