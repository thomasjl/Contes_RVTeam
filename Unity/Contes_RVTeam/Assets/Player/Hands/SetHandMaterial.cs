using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SetHandMaterial : MonoBehaviour {

    [SerializeField]
    Material mat;
    private void Start()
    {
        GetComponent<RenderModel>().SetHandMaterial(mat);
    }
}
