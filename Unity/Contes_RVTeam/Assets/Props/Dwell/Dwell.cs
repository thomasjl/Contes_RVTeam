using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwell : MonoBehaviour {

    DwellAxis axis;
    public Transform Bucket{ get{ return axis.Bucket; } }
    public float Bottom { get { return axis.Bottom; } }

    public static Dwell instance;
    private void Awake()
    {
        instance = this;
        axis = GetComponentInChildren<DwellAxis>(true);
    }
}
