using HTC.UnityPlugin.StereoRendering;
using System.Collections;
using UnityEngine;

public class PortalGetCamera : MonoBehaviour {

    StereoRenderer stereoRend;

    private void Awake()
    {
        stereoRend = GetComponent<StereoRenderer>();
        StartCoroutine(WaitForMainCamera());
    }

    IEnumerator WaitForMainCamera()
    {
        while (Camera.main == null)
        {
            stereoRend.enabled = false;
            yield return new WaitForSeconds(.5f);
        }
        stereoRend.enabled = true;
    }
}
