using Valve.VR.InteractionSystem;

public static class SteamVRUtilities {

    public static bool IsGrabbed(this Interactable interactable)
    {
        return interactable.attachedToHand != null;
    }

    public static void SetGrabEnabled(this Interactable interactable, bool state)
    {
        if (state)
        {
            if (!interactable.GetComponent<GrabLock>())
                return;
            else
                interactable.GetComponent<GrabLock>().Unlock();
        }
        else
            interactable.GetOrAddComponent<GrabLock>().Lock();
    }
}
