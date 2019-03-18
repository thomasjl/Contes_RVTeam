using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInstance : MonoBehaviour {

    private static SceneInstance instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
