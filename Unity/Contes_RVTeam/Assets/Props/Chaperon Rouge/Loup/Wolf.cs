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
        Invoke("LaunchGrognement1", 70f);
        Invoke("LaunchGrognement1", 120f);
    }

    private void LaunchGrognement1()
    {
        grognement1.Play();
    }

    private void LaunchHurlement()
    {
        hurlement.Play();
    }

    private void LaunchGrognement2()
    {
        grognement2.Play();
    }


}
