using System;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Medaillon : MonoBehaviour {

    Interactable interactable;
    [SerializeField]
    float distanceFromThorns = 1;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onAttachedToHand += OnGrab;
        interactable.onDetachedFromHand += OnUngrab;
    }

    private void OnUngrab(Hand hand)
    {
        throw new NotImplementedException();
    }

    private void OnGrab(Hand hand)
    {
        throw new NotImplementedException();
    }

    IEnumerator UpdateThorns(){
        while (true)
        {
            if (Vector3.Distance(transform.position, Thorns.instance.transform.position) < distanceFromThorns)
                Thorns.instance.Flatten();
            else
                Thorns.instance.Unflatten();
            yield return null;
        }
    }
}
