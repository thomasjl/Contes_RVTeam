using System.Collections;
using UnityEngine;

public class SignsEmissive : MonoBehaviour {

    public Material signClosed;
    public Material signFar;

    private GameObject lanterne;
    private float maxDist;
    private Renderer rend;

    private void Start()
    {
        lanterne = Lanterne.instance.gameObject;
        maxDist = lanterne.GetComponent<SphereCollider>().radius;
        rend = GetComponent<Renderer>();

        rend.material = signFar;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lanterne"))
        {
            StartCoroutine(SetFootprintMaterial());
            // rend.material.SetColor("_EmissionColor",signClosed.color);

            //rend.material = signClosed;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lanterne"))
        {
            rend.material = signFar;
            StopAllCoroutines();
        }
    }


    IEnumerator SetFootprintMaterial()
    {
        while (true)
        {
            //Debug.Log("lanterne");
            // set transparacy depending on distance from Lanterne (max distance = radius)
            float distance = Vector3.Distance(transform.position, lanterne.transform.position);
            // Debug.Log("distance " +distance);
            float ratio = distance / maxDist;
            //Debug.Log("ratio "+ratio);
            //lerp material

            rend.material.Lerp(signClosed, signFar, ratio);


            yield return null;
        }
    }

}
