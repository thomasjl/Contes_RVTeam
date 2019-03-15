using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {

    public AudioSource grognement1;
    public AudioSource hurlement;
    public AudioSource grognement2;

    public void Start()
    {
        Invoke("LaunchGrognement1", 30f);
        Invoke("LaunchHurlement", 70f);
        Invoke("LaunchGrognement2", 120f);
    }

    private void LaunchGrognement1()
    {
        StartCoroutine(AudioController.FadeIn(grognement1, 0.5f));
    }

    private void LaunchHurlement()
    {
        StartCoroutine(AudioController.FadeIn(hurlement, 2f));
    }

    private void LaunchGrognement2()
    {
        StartCoroutine(AudioController.FadeIn(grognement2, 0.5f));
    }
}