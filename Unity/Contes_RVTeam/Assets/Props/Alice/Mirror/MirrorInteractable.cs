using UnityEngine;
using Valve.VR.InteractionSystem;

public class MirrorInteractable : MonoBehaviour {

    Interactable interactable;
    Throwable throwable;
    Hand.AttachmentFlags attachmentFlags;
    ReleaseStyle releaseStyle;
    Mirror mirror;
    Rigidbody rb;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onAttachedToHand += OnGrabbed;
        throwable = GetComponent<Throwable>();
        attachmentFlags = throwable.attachmentFlags;
        releaseStyle = throwable.releaseVelocityStyle;
        rb = GetComponent<Rigidbody>();
    }

    public void SetMirror(Mirror _mirror)
    {
        mirror = _mirror;
        mirror.Shown += EnableGrab;
        mirror.Hidden += DisableGrab;
    }

    private void OnGrabbed(Hand hand)
    {
        transform.parent = null;
        rb.isKinematic = false;
        gameObject.layer = 0;
        SetGrabEnabled(true);
        Destroy(this);
    }

    public void SetGrabEnabled(bool state)
    {
        if (!state)
        {
            throwable.attachmentFlags = 0;
            throwable.releaseVelocityStyle = ReleaseStyle.NoChange;
            interactable.highlightOnHover = false;
            foreach (MeshRenderer highlighter in interactable.highlightRenderers)
                Destroy(highlighter.gameObject);
        }
        else
        {
            throwable.attachmentFlags = attachmentFlags;
            throwable.releaseVelocityStyle = releaseStyle;
            interactable.highlightOnHover = true;
        }
    }

    void EnableGrab()
    {
        SetGrabEnabled(true);
    }
    void DisableGrab()
    {
        SetGrabEnabled(false);
    }

    private void OnDestroy()
    {
        interactable.onAttachedToHand -= OnGrabbed;
        mirror.Shown -= EnableGrab;
        mirror.Hidden -= DisableGrab;
    }
}
