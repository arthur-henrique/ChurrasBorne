using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu_VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MasterVolumeParam", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MASTER_VOLUME", sliderValue);
    }
    public void SetLevelBGM(float sliderValue)
    {
        mixer.SetFloat("BGMVolumeParam", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGM_VOLUME", sliderValue);
    }
    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFXVolumeParam", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFX_VOLUME", sliderValue);
    }
}
