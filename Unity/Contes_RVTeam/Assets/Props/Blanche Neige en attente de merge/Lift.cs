using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Lift : MonoBehaviour {

[SerializeField]
    CircularDrive circularDrive;

    float previousDrive;

    private void Update()
    {
        previousDrive = circularDrive.outAngle;
    }
}
