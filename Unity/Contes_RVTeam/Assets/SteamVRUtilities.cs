using UnityEngine;
using Valve.VR.InteractionSystem;

public static class SteamVRUtilities {
    public static bool IsGrabbed(this Interactable interactable)
    {
        return interactable.attachedToHand != null;
    }
}
