using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Respawnable : MonoBehaviour {

    public static List<Respawnable> Objects { get; private set; }

    [SerializeField]
    bool waitForFirstGrab = false;
    bool waitingForFirstGrab = false;
    public bool CanRespawn { get { return !waitingForFirstGrab && interactable.IsGrabbed(); } }
    [SerializeField]
    Interactable interactable;

    private void Awake()
    {
        if (Objects == null)
            Objects = new List<Respawnable>();
        Objects.Add(this);

        waitingForFirstGrab = waitForFirstGrab;
        if (!waitForFirstGrab)
            interactable.onAttachedToHand += delegate { waitingForFirstGrab = false; };
    }

    private void OnDestroy()
    {
        Objects.Remove(this);
    }
}
