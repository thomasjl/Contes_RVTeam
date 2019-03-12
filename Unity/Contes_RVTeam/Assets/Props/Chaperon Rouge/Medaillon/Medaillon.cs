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

    [Space]
    [SerializeField]
    Transform cache;
    [SerializeField]
    Vector3 openRotation;
    bool open = false;
    bool canOpen = true;

    Cloth cloth;
    Rigidbody rb;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onAttachedToHand += OnGrab;
        interactable.onDetachedFromHand += OnUngrab;
        audio = GetComponent<AudioSource>();
        cloth = GetComponentInChildren<Cloth>();
        cloth.enabled = false;
    }

    private void OnGrab(Hand hand)
    {
        StartCoroutine(UpdateOnGrab());
        // Enable cloth.
        if (!cloth.enabled)
            cloth.enabled = true;
    }

    private void OnUngrab(Hand hand)
    {
        StopAllCoroutines();
        audio.Stop();
        canOpen = true;
    }

    IEnumerator UpdateOnGrab()
    {
        while (true)
        {
            UpdateThorns();
            UpdateSound();
            if (Mathf.Abs(GetComponent<Rigidbody>().angularVelocity.x) > 6)
                ToggleOpen();
            yield return null;
        }
    }

    void UpdateThorns()
    {
        if (!Thorns.instance)
            return;
        if (Vector3.Distance(transform.position, Thorns.instance.transform.position) < distanceFromThorns)
        {
            Debug.Log("proche medaillon");
            Thorns.instance.Flatten();
        }
        else
        {
            Debug.Log("loin medaillon");
            Thorns.instance.Unflatten();
        }
    }

    void UpdateSound()
    {
        if (!audio.isPlaying)
            audio.Play();
        float progression = 1 - (Vector3.Distance(transform.position, Lanterne.instance.FlamePosition) / (maxLanterneDistance - minLanterneDistance));
        audio.volume = Mathf.Lerp(minAudioVolume, 1, progression);
    }

    void ToggleOpen()
    {
        // Open or close over time.
        if (!canOpen)
            return;
        open = !open;
        this.ProgressionAnim(.2f, delegate (float progression)
        {
            if (!open)
                cache.localEulerAngles = Vector3.Lerp(openRotation, Vector3.zero, progression);
            else
                cache.localEulerAngles = Vector3.Lerp(Vector3.zero, openRotation, progression);

        }, delegate
        {
            this.Timer(.5f, delegate { canOpen = true; });
        });
        canOpen = false;
    }
}
