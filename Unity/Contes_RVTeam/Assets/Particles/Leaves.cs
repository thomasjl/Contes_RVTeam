using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour {

    private ParticleSystem leaves;

    public bool showParticle=false;

    private void Start()
    {
        leaves = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (showParticle && other.gameObject.CompareTag("Lanterne"))
        {
            leaves.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lanterne"))
        {
            leaves.Stop();
        }
    }

   
}
