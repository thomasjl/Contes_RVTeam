using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrabLock : MonoBehaviour {

    Interactable interactable;
    Throwable throwable;
    Hand.AttachmentFlags attachmentFlags;
    ReleaseStyle releaseStyle;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        throwable = GetComponent<Throwable>();
        attachmentFlags = throwable.attachmentFlags;
        releaseStyle = throwable.releaseVelocityStyle;
    }

    public void Lock()
    {
        throwable.attachmentFlags = 0;
        throwable.releaseVelocityStyle = ReleaseStyle.NoChange;
        interactable.highlightOnHover = false;
        MeshRenderer[] highlighters = interactable.highlightRenderers;
        for (int i = 0; i < highlighters.Length; i++)
            if (highlighters[i])
                Destroy(highlighters[i].gameObject);
    }

    public void Unlock()
    {
        throwable.attachmentFlags = attachmentFlags;
        throwable.releaseVelocityStyle = releaseStyle;
        interactable.highlightOnHover = true;
    }
}
