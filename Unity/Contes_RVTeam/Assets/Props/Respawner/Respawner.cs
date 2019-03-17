using UnityEngine;

public class Respawner : MonoBehaviour {

    float minYPosition = -5;

    bool canRespaw = false;
    private void Start()
    {
        this.Timer(5, delegate { canRespaw = true; });
    }


    private void Update()
    {
        // Respawn an object if it's out of bounds.
        foreach (Respawnable respawnable in Respawnable.Objects)
            if (respawnable.transform.position.y < minYPosition)
                Respawn(respawnable, Vector3.up * .5f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canRespaw)
            return;

        // Respawn an object that comes in contact.
        Respawnable respawnable = other.GetComponent<Respawnable>();
        if (!respawnable)
            respawnable = other.GetComponentInParent<Respawnable>();

        if (respawnable && respawnable.CanRespawn)
        {
            Vector3 directionToCenter = other.transform.position.normalized;
            Vector3 respawnPosition = other.transform.position + directionToCenter;
            Respawn(respawnable, respawnPosition);
        }
    }

    void Respawn(Respawnable respawnable, Vector3 respawnPosition)
    {
        // Reset the velocity and place the respawnable at the center of the room.
        respawnable.GetComponent<Rigidbody>().velocity = Vector3.zero;
        respawnable.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        respawnable.transform.position = respawnPosition;
    }
}
