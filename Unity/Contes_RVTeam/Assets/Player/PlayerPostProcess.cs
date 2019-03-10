using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerPostProcess : MonoBehaviour {

    PostProcessProfile profile;
    ColorGrading colorGrading;

    public float HueShift { get { return colorGrading.hueShift.value; } set { colorGrading.hueShift.value = value; } }

    public static PlayerPostProcess Instance{ get; private set; }

    private void Awake()
    {
        profile = GetComponent<PostProcessVolume>().profile;
        profile.TryGetSettings(out colorGrading);
        Instance = this;
    }
}
