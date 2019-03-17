using UnityEngine;

public class Respawner : MonoBehaviour {

    float minYPosition = -5;

    public Bounds Bounds { get; private set; }
    public static Respawner Instance { get; private set; }

    private void Awake()
    {
        Bounds = GetComponent<Collider>().bounds;
        Instance = this;
    }

    private void Update()
    {
        if (Respawnable.Objects != null)
            // Respawn objects if they are out of bounds.
            foreach (Respawnable respawnable in Respawnable.Objects)
                if (respawnable.transform.position.y < minYPosition)
                    Respawn(respawnable, Vector3.up * .5f);
    }

    private void OnTriggerExit(Collider other)
    {
        // Respawn an object that comes in contact.
        Respawnable respawnable = other.GetComponent<Respawnable>();
        if (!respawnable)
            respawnable = other.GetComponentInParent<Respawnable>();

        if (respawnable && !respawnable.WaitingForFirstGrab)
        {
            Vector3 directionToCenter = other.transform.position.normalized;
            Vector3 respawnPosition = other.transform.position - directionToCenter * .5f;
            if (!respawnable.IsGrabbed)
                Respawn(respawnable, respawnPosition);
            else
                respawnable.plannedRespawnPosition = respawnPosition;
        }
    }

    public void Respawn(Respawnable respawnable, Vector3 respawnPosition)
    {
        // Reset the velocity and place the respawnable at the center of the room.
        respawnable.GetComponent<Rigidbody>().velocity = Vector3.zero;
        respawnable.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        respawnable.transform.position = respawnPosition;
    }
}
