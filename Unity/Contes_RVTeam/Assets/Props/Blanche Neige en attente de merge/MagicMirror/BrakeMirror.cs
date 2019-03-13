using DigitalRuby.MagicMirror;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class BrakeMirror : MonoBehaviour {

    public int resWidth = 2048;
    public int resHeight = 2048;

    public Texture2D screenShot;
    public GameObject mirrorBroken;
    private Camera mirrorCamera;
    private GameObject mirror;

    private MagicMirrorScript mirrorScript;

    public RawImage rawimage;

    [SerializeField]
    bool canBreak = true;
    bool broken;

    float startHoverTime;
    [SerializeField]
    float timeUntilBreakable = 2;
    Renderer rend;
    float startSpecArea;
    float startSpecIntensity;

    private void Awake()
    {
        mirrorScript = GetComponent<MagicMirrorScript>();
        mirror = transform.parent.GetChild(1).gameObject;
        rend = GetComponent<Renderer>();
        startSpecArea = rend.material.GetFloat("_SpecularArea");
        startSpecIntensity = rend.material.GetFloat("_SpecularIntensity");
    }
    
    public void FreezeAndBreak()
    {
        if (broken)
            return;

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

        mirrorBroken.GetComponent<TriangleExplosion>().Explode(true);
        GetComponent<Renderer>().enabled = false;

        ShowWebcamMirror();
        //Invoke("ShowWebcamMirror", 0.5f);

        broken = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!canBreak)
            return;
        GetComponent<BoxCollider>().enabled = false;
        FreezeAndBreak();
    }

    private void OnHandHoverBegin(Hand hand)
    {
        startHoverTime = Time.time;
    }

    private void HandHoverUpdate(Hand hand)
    {
        SetGlowIntensity((Time.time - startHoverTime) / (timeUntilBreakable + 2));
        if (Time.time - startHoverTime > timeUntilBreakable && hand.GetGrabStarting() == GrabTypes.Pinch)
            FreezeAndBreak();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        SetGlowIntensity(0);
    }

    void SetGlowIntensity(float intensity)
    {
        rend.material.SetFloat("_SpecularArea", Mathf.Lerp(startSpecArea, 1, intensity));
        rend.material.SetFloat("_SpecularIntensity", Mathf.Lerp(startSpecIntensity, 2, intensity));
    }

    WebCamTexture webcamTexture;
    public void ShowWebcamMirror()
    {
        rawimage.enabled = true;
        webcamTexture = new WebCamTexture();
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
