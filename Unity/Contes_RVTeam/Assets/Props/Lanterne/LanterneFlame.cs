using UnityEngine;

public class LanterneFlame : MonoBehaviour {

    ParticleSystem[] particleSystems;
    new Light light;

    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        light = GetComponentInChildren<Light>();
    }

    public void SetColor(Color color)
    {
        foreach (ParticleSystem psys in particleSystems)
        {
            ParticleSystem.MainModule main = psys.main;
            main.startColor = color;
        }
        light.color = color;
    }
}
