using DigitalRuby.MagicMirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrakeMirror : MonoBehaviour {

    public int resWidth = 2048;
    public int resHeight = 2048;

    public Texture2D screenShot;
    public GameObject mirrorBroken;
    private Camera mirrorCamera;
    private GameObject mirror;

    private MagicMirrorScript mirrorScript;

    public RawImage rawimage;

    private void Start()
    {
        mirrorScript = GetComponent<MagicMirrorScript>();
        mirror = transform.parent.GetChild(1).gameObject;


    }


    public void TakeScreenShotCamera()
    {
        mirrorCamera = mirrorScript.currentReflectingCamera;


        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        mirrorCamera.targetTexture = rt;
        screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        mirrorCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        mirrorCamera = null;

        Destroy(rt);

        mirrorBroken.GetComponent<Renderer>().material.SetTexture("_MainTex", screenShot);
        mirror.GetComponent<Renderer>().enabled = false;
        mirrorScript.enabled = false;

        StartCoroutine(mirrorBroken.GetComponent<TriangleExplosion>().SplitMesh(true));

        Invoke("ShowWebcamMirror",0.5f);

    }


    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<BoxCollider>().enabled = false;
        Debug.Log("collision");
        TakeScreenShotCamera();

    }

    public void ShowWebcamMirror()
    {
        rawimage.enabled = true;
        WebCamTexture webcamTexture = new WebCamTexture();
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }



}
