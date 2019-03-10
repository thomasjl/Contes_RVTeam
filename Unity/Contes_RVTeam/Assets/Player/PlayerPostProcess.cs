using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerPostProcess : MonoBehaviour {

    PostProcessProfile profile;
    ColorGrading colorGrading;
    Vignette vignette;

    public float HueShift { get { return colorGrading.hueShift.value; } set { colorGrading.hueShift.value = value; } }
    public float VignetteStrength { get { return vignette.intensity.value; } set { vignette.intensity.value = value; } }

    public static PlayerPostProcess Instance{ get; private set; }

    private void Awake()
    {
        profile = GetComponent<PostProcessVolume>().profile;
        profile.TryGetSettings(out colorGrading);
        profile.TryGetSettings(out vignette);
        Instance = this;
    }
}
