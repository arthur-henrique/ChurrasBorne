using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class PostProcessingControl : MonoBehaviour
{
    public static PostProcessingControl Instance;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Volume volume = GetComponent<Volume>();
        Vignette tempVig;
        ChromaticAberration tempCA;
        LensDistortion tempLensDistortion;
        if (volume.profile.TryGet<Vignette>(out tempVig))
        {
            vignette = tempVig;
        }
        if (volume.profile.TryGet<ChromaticAberration>(out tempCA))
        {
            chromaticAberration = tempCA;
        }
        if (volume.profile.TryGet<LensDistortion>(out tempLensDistortion))
        {
            lensDistortion = tempLensDistortion;
        }
    }

    public void TurnOnVignette()
    {
        vignette.active = true;
    }
    public void TurnOffVignette()
    {
        vignette.active = false;
    }

    public void TurnOnCA()
    {
        float caIntensity = Random.Range(0.4f, 0.8f);
        chromaticAberration.intensity.value = caIntensity;
        chromaticAberration.active = true;
        StartCoroutine(ChromaticCurve());
    }

    public IEnumerator ChromaticCurve()
    {
        float timeToWait = Random.Range(0.5f, 1.25f);
        yield return new WaitForSeconds(timeToWait);
        chromaticAberration.active = false;
    }

    public void TurnOnLens()
    {
        float distortionIntensity = Random.Range(-0.2f, -0.05f);
        lensDistortion.intensity.value = distortionIntensity;
        lensDistortion.active = true;
    }
    public void TurnOffLens()
    {
        lensDistortion.active = false;
    }
}
