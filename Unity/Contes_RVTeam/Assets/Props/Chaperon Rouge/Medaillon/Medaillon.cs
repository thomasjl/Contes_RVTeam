using System;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Medaillon : MonoBehaviour {

    Interactable interactable;
    [SerializeField]
    float distanceFromThorns = 1;

    [SerializeField]
    float minLanterneDistance = .2f;
    [SerializeField]
    float maxLanterneDistance = 1;
    [SerializeField]
    float minAudioVolume = .2f;
    new AudioSource audio;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onAttachedToHand += OnGrab;
        interactable.onDetachedFromHand += OnUngrab;
        audio = GetComponent<AudioSource>();
    }

    private void OnGrab(Hand hand)
    {
        StartCoroutine(UpdateThorns());
        StartCoroutine(UpdateSound());
    }

    private void OnUngrab(Hand hand)
    {
        StopAllCoroutines();
        audio.Stop();
    }

    IEnumerator UpdateThorns()
    {
        while (Thorns.instance)
        {
            if (Vector3.Distance(transform.position, Thorns.instance.transform.position) < distanceFromThorns)
                Thorns.instance.Flatten();
            else
                Thorns.instance.Unflatten();
            yield return null;
        }
    }

    IEnumerator UpdateSound()
    {
        audio.Play();
        while (true)
        {
            float progression = 1 - (Vector3.Distance(transform.position, Lanterne.instance.transform.position) / (maxLanterneDistance - minLanterneDistance));
            audio.volume = Mathf.Lerp(minAudioVolume, 1, progression);
            yield return null;
        }
    }
}
