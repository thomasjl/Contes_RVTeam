using UnityEngine;
using Valve.VR.InteractionSystem;

public class MirrorInteractable : MonoBehaviour {

    Interactable interactable;
    Throwable throwable;
    Hand.AttachmentFlags attachmentFlags;
    ReleaseStyle releaseStyle;
    Mirror mirror;
    Rigidbody rb;

    [SerializeField]
    Material rockMaterial;

    public bool isGrabbable = true;

    public delegate void MirrorInteractableEventHandler();
    public event MirrorInteractableEventHandler Detached;


    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onAttachedToHand += OnGrabbed;
        throwable = GetComponent<Throwable>();
        attachmentFlags = throwable.attachmentFlags;
        releaseStyle = throwable.releaseVelocityStyle;
        rb = GetComponent<Rigidbody>();
        SetGrabEnabled(false);
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

    public void SetGrabbableInMirror(bool state)
    {
        if (!GetComponent<MirrorInteractable>())
            return;
        GetComponent<MirrorInteractable>().isGrabbable = state;
        if (!state)
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                rend.material = rockMaterial;
    }

    void SetGrabEnabled(bool state)
    {
        if (!state)
        {
            throwable.attachmentFlags = 0;
            throwable.releaseVelocityStyle = ReleaseStyle.NoChange;
            interactable.highlightOnHover = false;
            MeshRenderer[] highlighters = interactable.highlightRenderers;
            for (int i = 0; i < highlighters.Length; i++)
                Destroy(highlighters[i].gameObject);
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
        if (isGrabbable)
            SetGrabEnabled(true);
    }
    void DisableGrab()
    {
        if (isGrabbable)
            SetGrabEnabled(false);
    }

    private void OnDestroy()
    {
        interactable.onAttachedToHand -= OnGrabbed;
        mirror.Shown -= EnableGrab;
        mirror.Hidden -= DisableGrab;
        // Call event.
        if (Detached != null)
            Detached();
    }
}
