using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Void_Skip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Ferreiro_Encounter_1_DialogAct.ferreiro_encounter_1_occurred = false;
        Ferreiro_Encounter_1_DialogAct.ferreiro_encounter_2_occurred = false;
        Ferreiro_Encounter_2_DialogAct.ferreiro_encounter_3_occurred = false;
        Ferreiro_Encounter_2_DialogAct.ferreiro_encounter_4_occurred = false;
        Ferreiro_Encounter_2_DialogAct.ferreiro_encounter_5_occurred = false;
        Ferreiro_Encounter_2_DialogAct.ferreiro_encounter_6_occurred = false;
        Ferreiro_Encounter_2_DialogAct.ferreiro_encounter_7_occurred = false;
        Bruxinha_Encounter_1_DialogAct.bruxinha_encounter_1_occurred = false;
        Bruxinha_Encounter_1_DialogAct.bruxinha_encounter_2_occurred = false;
        Bruxinha_Encounter_2_DialogAct.bruxinha_encounter_3_occurred = false;
        Bruxinha_Encounter_2_DialogAct.bruxinha_encounter_4_occurred = false;
        Bruxinha_Encounter_2_DialogAct.bruxinha_encounter_5_occurred = false;
        Bruxinha_Encounter_2_DialogAct.bruxinha_encounter_6_occurred = false;
        Bruxinha_Encounter_2_DialogAct.bruxinha_encounter_7_occurred = false;

        SceneManager.LoadScene("Tutorial");
        if (GameManager.instance)
        {
            GameManager.instance.NextLevelSetter(Vector2.zero);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
