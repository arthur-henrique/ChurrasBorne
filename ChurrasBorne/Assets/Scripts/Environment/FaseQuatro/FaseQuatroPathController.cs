using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseQuatroPathController : MonoBehaviour
{
    public GameObject[] paths;
    private int selectedPath;
    private ManagerOfScenes manager;

    private void Start()
    {
        manager = FindObjectOfType<ManagerOfScenes>();
        StartCoroutine(PathSetter());
    }

    IEnumerator PathSetter()
    {
        yield return new WaitForSeconds(0.15f);
        selectedPath = manager.faseQuatroPath;
        paths[selectedPath].SetActive(true);
    }
}
