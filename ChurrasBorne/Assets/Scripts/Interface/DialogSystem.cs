using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    PlayerController pc;
    private Vector3 velocity_box = Vector3.zero;
    Coroutine cr_up;
    Coroutine cr_down;

    [SerializeField]
    private TMP_Text _title;

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
        cr_up = StartCoroutine(voidTask());
        cr_down = StartCoroutine(voidTask());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (pc.Movimento.Attack.WasPressedThisFrame()) {
            var selec = getChildGameObject(gameObject, "BalloonBox");
            if (selec.GetComponent<RectTransform>().anchoredPosition.y < -190)
            {
                StopCoroutine(PullDown());
                StartCoroutine(PullUp());
            }
            else
            {
                StopCoroutine(PullUp());
                StartCoroutine(PullDown());
            }
            if (selec.GetComponent<RectTransform>().anchoredPosition.y > -200)
            {
                _title.text = "Your new text is here";
            }

        }*/
    }

    public IEnumerator PullUp()
    {
        var selec = getChildGameObject(gameObject, "BalloonBox");
        //if (selec.GetComponent<RectTransform>().anchoredPosition.y >= -173.99f)
        //{
        //    StopCoroutine(PullUp());
        //}
        for (int i = 0; i < 120; i++)
        {
            //selec.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(selec.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -174, 0), 25f * Time.fixedDeltaTime);

            selec.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(selec.GetComponent<RectTransform>().anchoredPosition,
                            new Vector3(0, -174, 0), ref velocity_box, 8 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);

            if (GameManager.isInDialog == false) { yield break; }

            yield return null;
        }
    }

    public IEnumerator PullDown()
    {
        var selec = getChildGameObject(gameObject, "BalloonBox");
        //if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
        //{
        //    StopCoroutine(PullDown());
        //}
        for (int i = 0; i < 120; i++)
        {
            //selec.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(selec.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -334, 0), 25f * Time.fixedDeltaTime);

            selec.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(selec.GetComponent<RectTransform>().anchoredPosition,
                            new Vector3(0, -334, 0), ref velocity_box, 8 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);

            if (GameManager.isInDialog == true) { yield break; }

            yield return null;
        }
    }

    public IEnumerator StopDialog()
    {
        yield return new WaitForSeconds(.1f);

        for (int i = 0; !pc.Movimento.Attack.WasPressedThisFrame(); i++)
        {
            yield return null;
        }

        GameManager.isInDialog = false;
        PlayerMovement.EnableControl();
        var selec = getChildGameObject(gameObject, "BalloonBox");
        if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
        {
            StopCoroutine(PullDown());
        }
        for (int i = 0; i < 120; i++)
        {
            //selec.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(selec.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -334, 0), 25f * Time.fixedDeltaTime);

            selec.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(selec.GetComponent<RectTransform>().anchoredPosition,
                            new Vector3(0, -334, 0), ref velocity_box, 8 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);

            if (GameManager.isInDialog == true) { yield break; }

            yield return null;
        }
        
    }

    public IEnumerator DialogComplex(int num)
    {
        for (int i = 0; DialogBank.portuguese_dialog_bank[num, i] != ""; i++)
        {
            _title.text = DialogBank.portuguese_dialog_bank[num, i];

            yield return new WaitForSeconds(.5f);

            for (int j = 0; !pc.Movimento.Attack.WasPressedThisFrame(); j++)
            {
                yield return null;
            }
        }
        
        GameManager.isInDialog = false;
        PlayerMovement.EnableControl();
        var selec = getChildGameObject(gameObject, "BalloonBox");
        if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
        {
            StopCoroutine(PullDown());
        }
        for (int i = 0; i < 120; i++)
        {
            //selec.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(selec.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -334, 0), 25f * Time.fixedDeltaTime);

            selec.GetComponent<RectTransform>().anchoredPosition =
                            Vector3.SmoothDamp(selec.GetComponent<RectTransform>().anchoredPosition,
                            new Vector3(0, -334, 0), ref velocity_box, 8 * Time.unscaledDeltaTime, 999, Time.unscaledDeltaTime);

            if (GameManager.isInDialog == true) { yield break; }

            yield return null;
        }

    }

    public IEnumerator voidTask()
    {
        yield return null;
    }


    public void db_PullUP()
    {
        GameManager.isInDialog = true;
        PlayerMovement.DisableControl();
        StopCoroutine(cr_down);
        cr_up = StartCoroutine(PullUp());
    }

    public void db_PullDOWN()
    {
        GameManager.isInDialog = false;
        PlayerMovement.EnableControl();
        StopCoroutine(cr_up);
        cr_down = StartCoroutine(PullDown());
    }
    public void db_SetSceneSimple(int scene_number)
    {
        _title.text = DialogBank.portuguese_bank[scene_number];
        StartCoroutine(StopDialog());
    }

    public void db_SetSceneComplex(int dialog_piece)
    {
        StartCoroutine(DialogComplex(dialog_piece));
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
