using UnityEngine;

public class Hache : MonoBehaviour {

    Rigidbody rb;
    public bool IsSlicing { get { return rb.velocity.magnitude > 5; } }
    public Vector3 SliceDirection { get { return rb.velocity.normalized; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
