using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicMirrorInteraction : MonoBehaviour {

    public static MagicMirrorInteraction instance;
    [SerializeField]
    GameObject mirrorToRepare, mirrorToBrake;
    public enum MirrorType { ToRepare, ToBrake }

    public RawImage rawimage;

    private void Awake()
    {
        instance = this;
        SetWebcamMirror();
    }


    public void SetMirror(MirrorType mirrorType)
    {
        switch (mirrorType)
        {
            case MirrorType.ToRepare:
                mirrorToBrake.SetActive(false);
                mirrorToRepare.SetActive(true);
                break;
            case MirrorType.ToBrake:
                mirrorToRepare.SetActive(false);
                mirrorToBrake.SetActive(true);
                break;
        }
    }

    public void ShowWebcamMirror()
    {
        rawimage.enabled = true;
    }

    WebCamTexture webcamTexture;
    private void SetWebcamMirror()
    {
       
        webcamTexture = new WebCamTexture();

        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (WebCamDevice webcam in devices)
        {
            if (webcam.name != "HTC Vive")
            {
                webcamTexture.deviceName = webcam.name;
                rawimage.texture = webcamTexture;
                rawimage.material.mainTexture = webcamTexture;
                webcamTexture.Play();
            }
        }

    }

}
