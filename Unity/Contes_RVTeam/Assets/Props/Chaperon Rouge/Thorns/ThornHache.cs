﻿using UnityEngine;

public class ThornHache : MonoBehaviour
{
    Hache currenthache;
    int childrenToRemove = 0;
    public bool Clear { get; private set; }
    [SerializeField]
    AudioClip cut;


    private void Awake()
    {
        childrenToRemove = transform.childCount - 1;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Hache hache = collider.GetComponentInParent<Hache>();
        if (hache && hache.IsSlicing)
        {
            currenthache = hache;
            Cut();
        }
    }


    void Cut()
    {
        if (childrenToRemove <= 0)
            return;
        RemoveChild(transform.childCount - 1 - childrenToRemove);
        childrenToRemove--;
        AudioSource.PlayClipAtPoint(cut, transform.position);
        Cut();
    }

    void RemoveChild(int index)
    {
        Rigidbody newSlice = transform.GetChild(index).gameObject.AddComponent<Rigidbody>();

        Destroy(transform.GetChild(index).gameObject, 1);

        newSlice.AddForce(currenthache.SliceDirection * 5);
        transform.GetChild(index).parent = null;
        if (childrenToRemove <= 0 || transform.childCount <= 1)
            Clear = true;
    }
}
