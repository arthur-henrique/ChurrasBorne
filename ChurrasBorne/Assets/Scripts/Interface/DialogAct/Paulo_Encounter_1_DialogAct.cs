using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paulo_Encounter_1_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public Material sprite_lit;
    public Material sprite_unlit;

    public GameObject notif_balloon;
    public Sprite notif_exclamation;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
