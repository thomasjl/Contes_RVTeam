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

    public GameObject plane;

    private void Awake()
    {
        instance = this;
        SetWebcamMirror();

        Color tmpColor = plane.GetComponent<Renderer>().material.color;
        tmpColor.a = 0f;
        
        plane.GetComponent<Renderer>().material.color = tmpColor;
    }

    private void Start()
    {
        //LaunchGoodOutro();
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

        StartCoroutine(WaitUntilNextScene());
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

    IEnumerator WaitUntilNextScene()
    {
        yield return new WaitForSeconds(7);
        LaunchGoodOutro();
    }

    public void LaunchGoodOutro()
    {

        float tmpLightSettings = RenderSettings.ambientIntensity;

        this.ProgressionAnim(5f, delegate (float progression)
        {

            Color tmpColor = plane.GetComponent<Renderer>().material.color;
            tmpColor.a = progression;

            RenderSettings.ambientIntensity = Mathf.Lerp(tmpLightSettings, 8f, progression);

            plane.GetComponent<Renderer>().material.color = tmpColor;
        }, delegate
        {
            InterractionManagerBN.instance.LaunchGoodOutro();
        });
         
    }

}
