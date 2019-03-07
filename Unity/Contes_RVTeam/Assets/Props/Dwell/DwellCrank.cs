using UnityEngine;
using Valve.VR.InteractionSystem;

public class DwellCrank : MonoBehaviour {

    Interactable interactable;
    Throwable throwable;
    Rigidbody rb;
    CircularDrive circularDrive;

    public bool IsGrabbed { get { return interactable.attachedToHand != null; } }
    public float LinearMapping {
        get {
            if (!circularDrive || !circularDrive.linearMapping)
                return 0;
            return circularDrive.linearMapping.value;
        }
    }

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        throwable = GetComponent<Throwable>();
        rb = GetComponent<Rigidbody>();
    }

    public void UseAsCrank(CircularDrive.Axis_t axis)
    {
        // Remove the throwable behaviour and setup the crank behaviour.
        interactable.attachedToHand.DetachObject(gameObject);
        Destroy(throwable);
        Destroy(rb);
        circularDrive = gameObject.AddComponent<CircularDrive>();
        circularDrive.axisOfRotation = axis;
        circularDrive.hoverLock = true;
    }
}
