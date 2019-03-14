using UnityEngine;

public class LanterneFlame : MonoBehaviour {

    [SerializeField]
    Color startColor = new Color(.18f, .9f, 1);
    ParticleSystem[] particleSystems;
    new Light light;
    public Color Color{ get; private set; }


    public static LanterneFlame instance;

    private void Awake()
    {
        instance = this;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        light = GetComponentInChildren<Light>();
        SetColor(startColor);
    }

    public void SetColor(Color newColor)
    {
        foreach (ParticleSystem psys in particleSystems)
        {
            ParticleSystem.MainModule main = psys.main;
            main.startColor = newColor;
        }
        light.color = newColor;
        Color = newColor;
    }
}
