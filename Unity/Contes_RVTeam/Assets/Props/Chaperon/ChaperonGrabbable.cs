using UnityEngine;
using Valve.VR.InteractionSystem;

public class ChaperonGrabbable : MonoBehaviour {

    public Rigidbody Rb { get; private set; }
    public Interactable Interactable{ get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Interactable = GetComponent<Interactable>();
    }
}
