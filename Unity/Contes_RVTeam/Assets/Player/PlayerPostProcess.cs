using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using PostProcess;

public class PlayerPostProcess : MonoBehaviour {

    PostProcessProfile profile;
    ColorGrading colorGrading;
    Vignette vignette;
    BlinkEffect blink;

    public float StartHueshift { get; private set; }
    public float HueShift { get { return colorGrading.hueShift.value; } set { colorGrading.hueShift.value = value; } }
    public Color StartColorFilter { get; private set; }
    public Color ColorFilter { get { return colorGrading.colorFilter.value; } set { colorGrading.colorFilter.value = value; } }
    public float StartVignetteStrength { get; private set; }
    public float VignetteStrength { get { return vignette.intensity.value; } set { vignette.intensity.value = value; } }
    public float Blink { get { return blink.time; } set { blink.time = value; } }

    public static PlayerPostProcess Instance { get; private set; }

    private void Awake()
    {
        profile = GetComponent<PostProcessVolume>().profile;
        profile.TryGetSettings(out colorGrading);
        profile.TryGetSettings(out vignette);
        StartHueshift = HueShift;
        StartColorFilter = colorGrading.colorFilter.value;
        StartVignetteStrength = vignette.intensity;
        blink = GetComponent<BlinkEffect>();
        Instance = this;
    }

    public void PlayBlinkFadeOut(float time, System.Action endAction)
    {
        this.ProgressionAnim(time, delegate (float progression)
        {
            blink.time = progression;
        }, endAction);
    }

    public void PlayPoison(float transitionDuration, Color targetColor)
    {
        this.ProgressionAnim(transitionDuration, delegate (float progression)
        {
            ColorFilter = Color.Lerp(StartColorFilter, targetColor, progression);
        });
    }
}
