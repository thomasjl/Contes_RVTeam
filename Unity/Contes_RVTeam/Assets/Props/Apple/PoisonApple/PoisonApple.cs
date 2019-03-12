using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonApple : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Comestible>().Consumed += OnConsumed;
    }

    void OnConsumed(){

    }
}
