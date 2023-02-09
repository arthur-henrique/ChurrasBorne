using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu_VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MasterVolumeParam", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelBGM(float sliderValue)
    {
        mixer.SetFloat("BGMVolumeParam", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFXVolumeParam", Mathf.Log10(sliderValue) * 20);
    }
}
