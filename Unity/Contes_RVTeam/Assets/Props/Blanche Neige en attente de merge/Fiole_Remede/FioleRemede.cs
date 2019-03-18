using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FioleRemede : MonoBehaviour {

    public static FioleRemede instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);

        transform.GetChild(0).gameObject.GetComponent<Comestible>().Consumed += FioleUsed;
    }

    public void SpawnFiole()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void FioleUsed()
    {
        PoisonApple.instance.RemedeFound();
    }
}
