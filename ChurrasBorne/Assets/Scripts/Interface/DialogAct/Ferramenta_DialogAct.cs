using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferramenta_DialogAct : MonoBehaviour
{
    public GameObject target;
    public GameObject dbox;
    PlayerController pc;

    public Material sprite_lit;
    public Material sprite_unlit;

    public AudioSource audioSource;
    public AudioClip item_get;
    public GateChecker gc;

    private void Awake()
    {
        pc = new PlayerController();
        dbox = GameObject.Find("DialogBox");
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
        target = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float dist = Vector2.Distance(target.transform.position, transform.position);
            if (dist <= 3)
            {
                GetComponent<SpriteRenderer>().material = sprite_unlit;
            }
            else
            {
                GetComponent<SpriteRenderer>().material = sprite_lit;
            }
            if (pc.Movimento.Attack.WasPressedThisFrame() && dist <= 3)
            {
                audioSource.PlayOneShot(item_get, audioSource.volume);
                GetComponent<SpriteRenderer>().material = sprite_lit;
                Inventory_Manager.instance.itemStorage.Add(10);
                GameManager.instance.HasCollectedItemUm();
                gc.FaseUmOpenRoutine();
                HealthBar_Manager.newItem = true;
                Destroy(gameObject);
            }

        }
    }
}
