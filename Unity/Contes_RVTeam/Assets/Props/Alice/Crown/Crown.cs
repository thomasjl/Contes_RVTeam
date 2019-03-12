﻿using UnityEngine;
using Valve.VR.InteractionSystem;

public class Crown : MonoBehaviour {

    [SerializeField]
    float headYOffset = .05f;
    public bool IsEquipped { get; private set; }

    public static Crown Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HeadCollider"))
        {
            // Detach from the hand and attach to the head.
            GetComponent<Interactable>().attachedToHand = null;
            GetComponent<Interactable>().SetGrabEnabled(false);
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.transform;
            transform.localPosition = Vector3.up * headYOffset;
            transform.localRotation = Quaternion.identity;
            IsEquipped = true;
        }
    }
}
