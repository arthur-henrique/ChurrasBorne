using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSpikeSummoner : MonoBehaviour
{
    public bool firstSpikePattern = false, secondSpikePattern = false, thirdSpikePattern = false;

    GameObject[] firstSpikes, secondSpikes, thirdSpikes;
    
    void Start()
    {
        firstSpikes = GameObject.FindGameObjectsWithTag("BullSpikes1");
        secondSpikes = GameObject.FindGameObjectsWithTag("BullSpikes2");
        thirdSpikes = GameObject.FindGameObjectsWithTag("BullSpikes3");

        foreach (GameObject firstSpike in firstSpikes)
        {
            firstSpike.SetActive(false);
        }
        foreach (GameObject secondSpike in secondSpikes)
        {
            secondSpike.SetActive(false);
        }
        foreach (GameObject thirdSpike in thirdSpikes)
        {
            thirdSpike.SetActive(false);
        }
    }

    void Update()
    {
        if(firstSpikePattern == true)
        {
            foreach(GameObject firstSpike in firstSpikes)
            {
                firstSpike.SetActive(true);
            }
        }
        else if(secondSpikePattern == true)
        {
            foreach(GameObject secondSpike in secondSpikes)
            {
                secondSpike.SetActive(true);
            }
        }
        else if(thirdSpikePattern == true)
        {
            foreach (GameObject thirdSpike in thirdSpikes)
            {
                thirdSpike.SetActive(true);
            }
        }
    }
}
