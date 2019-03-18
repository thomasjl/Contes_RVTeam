using UnityEngine;
using Valve.VR.InteractionSystem;

public class BoxLid : MonoBehaviour {

    Interactable interactable;
    CircularDrive circularDrive;
    [SerializeField]
    Vector2 minMaxOpened = new Vector2(-90, -180);

    public delegate void BoxLidEH();
    public event BoxLidEH Opened;
    public event BoxLidEH Closed;

    private void Awake()
    {
        interactable = GetComponentInChildren<Interactable>();
        circularDrive = GetComponent<CircularDrive>();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        if (circularDrive.outAngle.IsBetween(minMaxOpened.x, minMaxOpened.y))
        {
            if (Opened != null)
                Opened();
        }
        else
        {
            if (Closed != null)
                Closed();
        }
    }
}
