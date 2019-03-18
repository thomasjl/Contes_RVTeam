using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    public Material footPrintClosed;
    public Material footPrintFar;
    public Material outOfRangeFootprint;
    
    private float maxDist;
    private Renderer rend;


    private void Start()
    {
        maxDist = Lanterne.instance.GetComponent<SphereCollider>().radius;
        rend = GetComponent<Renderer>();

        rend.material = outOfRangeFootprint;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lanterne"))
        {
            StartCoroutine(SetFootprintMaterial());
        }
    }

   
    /*
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("trigger stay");
        if (other.gameObject.CompareTag("Lanterne"))
        {
            Debug.Log("lanterne");
            // set transparacy depending on distance from Lanterne (max distance = radius)
            float distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
           // Debug.Log("distance " +distance);
            float ratio = distance / maxDist;
            //Debug.Log("ratio "+ratio);
            //lerp material
            rend.material.Lerp(footPrintClosed,footPrintFar,ratio);


        }

    }
    */

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lanterne"))
        {
            rend.material = outOfRangeFootprint;
            StopAllCoroutines();
        }
    }

    IEnumerator SetFootprintMaterial()
    {
        while(true)
        {
            //Debug.Log("lanterne");
            // set transparacy depending on distance from Lanterne (max distance = radius)
            float distance = Vector3.Distance(transform.position, Lanterne.instance.transform.position);
            // Debug.Log("distance " +distance);
            float ratio = distance / maxDist;
            //Debug.Log("ratio "+ratio);
            //lerp material
            rend.material.Lerp(footPrintClosed, footPrintFar, ratio);
            yield return null;
        }
    }

   
}
