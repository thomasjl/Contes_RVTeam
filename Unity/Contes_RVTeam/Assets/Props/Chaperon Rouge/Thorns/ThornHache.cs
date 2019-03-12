using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;


public class ThornHache : MonoBehaviour {


    Hache currenthache;
    int childrenToRemove = 0;
    public bool Clear { get; private set; }
    [SerializeField]
    AudioClip cut;
    
    private void Awake()
    {
        childrenToRemove = transform.childCount - 1;
    }

    private void Start()
    {
        //GetComponent<Throwable>().enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("hacccche");
        Hache hache = collider.GetComponentInParent<Hache>();
        if (hache && hache.IsSlicing)
        {
            currenthache = hache;
            Cut();
        }
    }
    void Cut()
    {
        Debug.Log("cut");
        if (childrenToRemove <= 0)
            return;
        RemoveChild(transform.childCount - 1 - childrenToRemove);
        childrenToRemove--;
        AudioSource.PlayClipAtPoint(cut, transform.position);
    }

    void RemoveChild(int index)
    {
        Rigidbody newSlice = transform.GetChild(index).gameObject.AddComponent<Rigidbody>();


        Destroy(transform.GetChild(index).gameObject, 1);


        //transform.GetChild(index).gameObject.AddComponent<Throwable>();
        newSlice.AddForce(currenthache.SliceDirection * 5);
        transform.GetChild(index).parent = null;
        if (childrenToRemove <= 0)
            Clear = true;
    }



}
