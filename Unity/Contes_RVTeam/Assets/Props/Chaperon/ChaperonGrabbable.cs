using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ChaperonGrabbable : MonoBehaviour {

    public Rigidbody Rb { get; private set; }
    public Interactable Interactable { get; private set; }
    Throwable throwable;
    Hand.AttachmentFlags attachmentFlags;
    ReleaseStyle releaseVelocityStyle;
    Transform OnTreeTransform { get { return ChaperonOnTreePos.instance.transform; } }
    ChaperonStick stick;
    public bool Attached { get; private set; }

    Cloth cloth;
    [SerializeField]
    GameObject hitbox;
    Transform startParent;

    List<Collider> colliders = new List<Collider>();

    public delegate void ChaperonEventHandler();
    public event ChaperonEventHandler Detached;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Interactable = GetComponent<Interactable>();
        throwable = GetComponent<Throwable>();
        cloth = GetComponentInChildren<Cloth>();
        attachmentFlags = throwable.attachmentFlags;
        releaseVelocityStyle = throwable.releaseVelocityStyle;
        startParent = transform.parent;
        foreach (Collider col in GetComponentsInChildren<Collider>(true))
        {
            if (!col.isTrigger)
                colliders.Add(col);
        }
    }

    public void SetGrabEnabled(bool state)
    {
        if (!state)
        {
            throwable.attachmentFlags = 0;
            throwable.releaseVelocityStyle = ReleaseStyle.NoChange;
            Interactable.highlightOnHover = false;
        }
        else
        {
            throwable.attachmentFlags = attachmentFlags;
            throwable.releaseVelocityStyle = releaseVelocityStyle;
            Interactable.highlightOnHover = true;
        }
    }


    public void AttachToDwell()
    {
        // Snap to the bucket and be ready to be grabbed.
        AttachTo(Dwell.instance.Bucket);
        Interactable.onAttachedToHand += DetachFromSceneElement;
    }

    public void AttachToTree()
    {
        // Snap to the tree and be ready to be grabbed.
        AttachTo(OnTreeTransform);
        SetGrabEnabled(false);
    }

    void AttachTo(Transform attachPoint)
    {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.parent = attachPoint;
        Rb.isKinematic = true;
        ResetCloth();
        Attached = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stick == null && other.GetComponent<ChaperonStick>() != null)
        {
            stick = other.GetComponent<ChaperonStick>();
            AttachToStick();
        }
    }

    void AttachToStick()
    {
        transform.parent = startParent;
        StartCoroutine(FollowStick());
        SetGrabEnabled(true);
        Interactable.onAttachedToHand += DetachFromSceneElement;
        ResetCloth();
        // Call event.
        if (Detached != null)
            Detached();
    }

    void DetachFromSceneElement(Hand hand)
    {
        // Reset grabbable.
        transform.parent = startParent;
        Rb.isKinematic = false;
        Interactable.onAttachedToHand -= DetachFromSceneElement;
        StopAllCoroutines();
        SetCollisionActive(true);
        Attached = false;
        // Call event.
        if (Detached != null)
            Detached();
    }

    IEnumerator FollowStick()
    {
        SetCollisionActive(false);
        while (true)
        {
            // Follow the position of the stick's attach point.
            transform.position = stick.AttachPoint.position;
            yield return null;
        }
    }

    void SetCollisionActive(bool state)
    {
        foreach (Collider col in colliders)
            col.enabled = state;
    }

    void ResetCloth()
    {
        StartCoroutine(ResettingCloth());
    }
    IEnumerator ResettingCloth()
    {
        cloth.enabled = false;
        yield return null;
        cloth.enabled = true;
    }
}