using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    public Material footPrintClosed;
    public Material footPrintFar;
    public Material outOfRangeFootprint;

    private GameObject lanterne;
    private float maxDist;
    private Renderer rend;

    private void Start()
    {
        lanterne = GameObject.FindGameObjectWithTag("Lanterne");
        maxDist = lanterne.GetComponent<SphereCollider>().radius;
        rend = GetComponent<Renderer>();

        rend.material = outOfRangeFootprint;
    }
        

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag =="Lanterne")
        {
            // set transparacy depending on distance from Lanterne (max distance = radius)
            float distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
           // Debug.Log("distance " +distance);
            float ratio = distance / maxDist;
            //Debug.Log("ratio "+ratio);
            //lerp material
            rend.material.Lerp(footPrintClosed,footPrintFar,ratio);


        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lanterne")
        {

            rend.material = outOfRangeFootprint;
        }
    }

   
}
