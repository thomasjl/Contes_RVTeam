using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerGoodOutro : InterractionManager
{

    private void Start()
    {
        Camera.main.backgroundColor = Color.white;
        if(GameObject.FindGameObjectWithTag("HeadCollider").transform.parent.GetChild(1)!=null)
        {
            GameObject.FindGameObjectWithTag("HeadCollider").transform.parent.GetChild(1).gameObject.SetActive(false);
        }
    }

    
}
