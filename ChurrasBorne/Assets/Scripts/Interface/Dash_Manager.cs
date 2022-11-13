using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Manager : MonoBehaviour
{

    GameObject DASH_Base;
    GameObject DASH_Bar_1;
    GameObject DASH_Bar_2;
    GameObject DASH_Bar_3;

    float hp_amount_lerp = 0;
    float convertHealth = 0;

    public static float dash_fill_global = 60*3;
    public static float dash_light_global = 0.6f;

    // Start is called before the first frame update
    void Start()
    {

        DASH_Base = DialogSystem.getChildGameObject(gameObject, "DASH_Base");

        DASH_Bar_1 = DialogSystem.getChildGameObject(gameObject, "DASH_Bar_1");
        DASH_Bar_2 = DialogSystem.getChildGameObject(gameObject, "DASH_Bar_2");
        DASH_Bar_3 = DialogSystem.getChildGameObject(gameObject, "DASH_Bar_3");

    }

    // Update is called once per frame
    void Update()
    {
        dash_fill_global = dash_fill_global += 14f * Time.deltaTime;
        dash_fill_global = Mathf.Clamp(dash_fill_global, 0, 60 * 3);
        //Debug.Log(dash_fill_global);

        dash_light_global = Mathf.Lerp(dash_light_global, 0.6f, Time.deltaTime * 2f);
        var dlg = dash_light_global;

        var dbase_col = DASH_Base.GetComponent<Image>().color;
        dbase_col.a = Mathf.Lerp(dbase_col.a, 1f - dlg, Time.deltaTime * 4f);
        DASH_Base.GetComponent<Image>().color = dbase_col;

        // DASH BAR 1
        if (dash_fill_global <= 60)
        {
            DASH_Bar_1.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_1.GetComponent<Image>().fillAmount, dash_fill_global / 60, Time.deltaTime * 4f);

            var dbar_1_col = DASH_Bar_1.GetComponent<Image>().color; dbar_1_col.a = Mathf.Lerp(dbar_1_col.a, 0.6f - dlg/2, Time.deltaTime * 4f); DASH_Bar_1.GetComponent<Image>().color = dbar_1_col;

        } else
        {
            DASH_Bar_1.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_1.GetComponent<Image>().fillAmount, 1, Time.deltaTime * 4f);

            var dbar_1_col = DASH_Bar_1.GetComponent<Image>().color; dbar_1_col.a = Mathf.Lerp(dbar_1_col.a, 1f - dlg, Time.deltaTime * 4f); DASH_Bar_1.GetComponent<Image>().color = dbar_1_col;
        }

        // DASH BAR 2
        if (dash_fill_global <= 60*2 && dash_fill_global > 60)
        {
            DASH_Bar_2.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_2.GetComponent<Image>().fillAmount, (dash_fill_global - 60) / 60, Time.deltaTime * 4f);

            var dbar_2_col = DASH_Bar_2.GetComponent<Image>().color; dbar_2_col.a = Mathf.Lerp(dbar_2_col.a, 0.6f - dlg/2, Time.deltaTime * 4f); DASH_Bar_2.GetComponent<Image>().color = dbar_2_col;
        }
        else if (dash_fill_global > 60*2)
        {
            DASH_Bar_2.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_2.GetComponent<Image>().fillAmount, 1, Time.deltaTime * 4f);

            var dbar_2_col = DASH_Bar_2.GetComponent<Image>().color; dbar_2_col.a = Mathf.Lerp(dbar_2_col.a, 1f - dlg, Time.deltaTime * 4f); DASH_Bar_2.GetComponent<Image>().color = dbar_2_col;
        } 
        else
        {
            DASH_Bar_2.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_2.GetComponent<Image>().fillAmount, 0, Time.deltaTime * 4f);

            var dbar_2_col = DASH_Bar_2.GetComponent<Image>().color; dbar_2_col.a = Mathf.Lerp(dbar_2_col.a, 0.6f - dlg/2, Time.deltaTime * 4f); DASH_Bar_2.GetComponent<Image>().color = dbar_2_col;
        }

        // DASH BAR 3
        if (dash_fill_global < 60 * 3 && dash_fill_global > 60*2)
        {
            //DASH_Bar_3.GetComponent<Image>().fillAmount = (dash_fill_global - 60*2) / 60;
            DASH_Bar_3.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_3.GetComponent<Image>().fillAmount, (dash_fill_global - 60 * 2) / 60, Time.deltaTime * 4f);

            var dbar_3_col = DASH_Bar_3.GetComponent<Image>().color; dbar_3_col.a = Mathf.Lerp(dbar_3_col.a, 0.6f - dlg/2, Time.deltaTime * 4f); DASH_Bar_3.GetComponent<Image>().color = dbar_3_col;
        }
        else if (dash_fill_global >= 60 * 3)
        {
            DASH_Bar_3.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_3.GetComponent<Image>().fillAmount, 1, Time.deltaTime * 4f);

            var dbar_3_col = DASH_Bar_3.GetComponent<Image>().color; dbar_3_col.a = Mathf.Lerp(dbar_3_col.a, 1f - dlg, Time.deltaTime * 4f); DASH_Bar_3.GetComponent<Image>().color = dbar_3_col;
        }
        else
        {
            DASH_Bar_3.GetComponent<Image>().fillAmount = Mathf.Lerp(DASH_Bar_3.GetComponent<Image>().fillAmount, 0, Time.deltaTime * 4f);

            var dbar_3_col = DASH_Bar_3.GetComponent<Image>().color; dbar_3_col.a = Mathf.Lerp(dbar_3_col.a, 0.6f - dlg/2, Time.deltaTime * 4f); DASH_Bar_3.GetComponent<Image>().color = dbar_3_col;
        }

    }
}
