using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footpath : MonoBehaviour {


    private List<GameObject> footprints;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            footprints.Add(child.gameObject);
        }
    }
}
