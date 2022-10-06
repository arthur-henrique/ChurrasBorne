using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    PlayerController pc;

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
        
    }

    // Update is called once per frame
    void Update()
    {
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

        }
    }

    IEnumerator PullUp()
    {
        var selec = getChildGameObject(gameObject, "BalloonBox");
        if (selec.GetComponent<RectTransform>().anchoredPosition.y >= -173.99f)
        {
            StopCoroutine(PullUp());
        }
        for (int i = 0; i < 50; i++)
        {
            selec.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(selec.GetComponent<RectTransform>().anchoredPosition,
                                                               new Vector3(0, -174, 0), 0.08f);
            yield return null;
        }
    }

    IEnumerator PullDown()
    {
        var selec = getChildGameObject(gameObject, "BalloonBox");
        if (selec.GetComponent<RectTransform>().anchoredPosition.y < -330)
        {
            StopCoroutine(PullDown());
        }
        for (int i = 0; i < 50; i++)
        {
            selec.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(selec.GetComponent<RectTransform>().anchoredPosition,
                                                               new Vector3(0, -334, 0), 0.08f);
            yield return null;
        }
    }

    public void Habilitacao(CallbackContext context)
    {
        
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
