using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using PostProcess;

public class PlayerPostProcess : MonoBehaviour {

    PostProcessProfile profile;
    ColorGrading colorGrading;
    Vignette vignette;
    BlinkEffect Blink { get { return BlinkEffect.Instance; } }

    public float StartHueshift { get; private set; }
    public float HueShift { get { return colorGrading.hueShift.value; } set { colorGrading.hueShift.value = value; } }
    public Color StartColorFilter { get; private set; }
    public Color ColorFilter { get { return colorGrading.colorFilter.value; } set { colorGrading.colorFilter.value = value; } }
    public float StartVignetteStrength { get; private set; }
    public float VignetteStrength { get { return vignette.intensity.value; } set { vignette.intensity.value = value; } }
    public float BlinkTime { get { return Blink.time; } set { Blink.time = value; } }

    public static PlayerPostProcess Instance { get; private set; }


    private void Awake()
    {
        profile = GetComponent<PostProcessVolume>().profile;
        profile.TryGetSettings(out colorGrading);
        profile.TryGetSettings(out vignette);
        StartHueshift = HueShift;
        StartColorFilter = colorGrading.colorFilter.value;
        StartVignetteStrength = vignette.intensity;
        Instance = this;
    }

    public void PlayBlinkFadeOut(float time, System.Action endAction)
    {
        (this).ProgressionAnim(time, (System.Action<float>)delegate (float progression)
        {
            this.Blink.time = progression;
        }, endAction);
    }

    public void PlayPoison(float transitionDuration, Color targetColor)
    {
        
        this.ProgressionAnim(transitionDuration, delegate (float progression)
        {
            //Debug.Log("progression "+ progression);
            ColorFilter = Color.Lerp(StartColorFilter, targetColor, progression);
        });
    }

    public void PlayRemede(float transitionDuration)
    {
        Debug.Log("remede");
        StopAllCoroutines();
        Color currentColorVignette = ColorFilter;
        this.ProgressionAnim(transitionDuration, delegate (float progression)
        {
            ColorFilter = Color.Lerp(currentColorVignette, StartColorFilter, progression);
        });
    }
}
