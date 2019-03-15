using UnityEngine;
using Valve.VR.InteractionSystem;

public class Respawner : MonoBehaviour {

    [SerializeField]
    Interactable[] interactablesToRespawn;

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
        if (interactablesToRespawn.Length < 1)
        {
            if (interactable)
                foreach (Interactable interactableToIgnore in interactablesToRespawn)
                    if (interactable == interactableToIgnore && !interactable.IsGrabbed())
                        this.Timer(4, delegate { Respawn(interactable); });
        }
        else if (interactable && !interactable.IsGrabbed())
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
