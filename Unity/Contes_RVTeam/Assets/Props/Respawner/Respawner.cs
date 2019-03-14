using UnityEngine;
using Valve.VR.InteractionSystem;

public class Respawner : MonoBehaviour {

    bool canRespaw = false;
    private void Start()
    {
        this.Timer(5, delegate { canRespaw = true; });
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!canRespaw)
            return;
        Interactable interactable = other.GetComponent<Interactable>();
        if (!interactable)
            interactable = other.GetComponentInParent<Interactable>();
        if (interactable && !interactable.IsGrabbed())
            this.Timer(4, delegate { Respawn(interactable); });
    }

    void Respawn(Interactable interactable)
    {
        // Reset the velocity and place the interactable at the center of the room.
        interactable.GetComponent<Rigidbody>().velocity = Vector3.zero;
        interactable.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        interactable.transform.position = Vector3.up * .5f;
    }
}
