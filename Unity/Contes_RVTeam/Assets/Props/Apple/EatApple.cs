using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatApple : MonoBehaviour
{

    public GameObject eatenApple;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HeadCollider"))
        {
            GetComponent<MeshFilter>().mesh = eatenApple.GetComponent<MeshFilter>().sharedMesh;
            GetComponent<MeshRenderer>().material = eatenApple.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }

    
}
