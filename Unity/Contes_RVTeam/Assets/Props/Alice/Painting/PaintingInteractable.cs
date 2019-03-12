using UnityEngine;
using Valve.VR.InteractionSystem;

public class PaintingInteractable : MonoBehaviour {

    Interactable interactable;
    Throwable throwable;
    Hand.AttachmentFlags attachmentFlags;
    ReleaseStyle releaseStyle;
    Painting painting;
    Rigidbody rb;

    [SerializeField]
    Material rockMaterial;

    public bool isGrabbable = true;

    public delegate void PaintingInteractableEH();
    public event PaintingInteractableEH Detached;


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

    public void SetPainting(Painting _painting)
    {
        painting = _painting;
        painting.Shown += EnableGrab;
        painting.Hidden += DisableGrab;
    }

    private void OnGrabbed(Hand hand)
    {
        if (!interactable.highlightOnHover)
            return;
        transform.parent = null;
        ResetLayer(transform);
        rb.isKinematic = false;
        SetGrabEnabled(true);
        Destroy(this);
    }

    void ResetLayer(Transform transformToReset)
    {
        foreach (Transform tr in transformToReset)
        {
            tr.gameObject.layer = 0;
            ResetLayer(tr);
        }
    }

    public void SetGrabbableInPainting(bool state)
    {
        if (!GetComponent<PaintingInteractable>())
            return;
        GetComponent<PaintingInteractable>().isGrabbable = state;
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
        painting.Shown -= EnableGrab;
        painting.Hidden -= DisableGrab;
        // Call event.
        if (Detached != null)
            Detached();
    }
}
