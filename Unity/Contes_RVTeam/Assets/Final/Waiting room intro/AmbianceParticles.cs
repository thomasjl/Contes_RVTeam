using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceParticles : MonoBehaviour {

    ParticleSystem particles;
    public static AmbianceParticles Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        particles = GetComponent<ParticleSystem>();
    }

    public void SetColor(Color newColor)
    {
        ParticleSystem.MainModule main = particles.main;
        main.startColor = newColor;
    }
}
