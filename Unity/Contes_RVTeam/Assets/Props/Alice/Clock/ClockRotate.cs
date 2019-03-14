using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotate : MonoBehaviour {

    public bool inverser;
    private float rotationSpeed;
    private float x;
    // Use this for initialization
    void Start ()
    {
        x = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //x += Time.deltaTime * rotationSpeed;
        //hour.transform.localRotation = Quaternion.Euler(,0, 0);
        if (inverser)
        {
            transform.Rotate(Vector3.up * 5f, Space.Self);
        }
        else
        {
            transform.Rotate(-Vector3.up, Space.Self);
        }
        //transform.localEulerAngles += new Vector3(transform.localEulerAngles.x+0.1f,0,0);
    }
}
