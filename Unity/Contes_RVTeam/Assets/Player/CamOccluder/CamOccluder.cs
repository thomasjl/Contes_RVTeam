using UnityEngine;

public class CamOccluder : MonoBehaviour {

    Renderer occluder;
    Color startColor;

    public static CamOccluder Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        occluder = GetComponentInChildren<Renderer>();
        // Setup occluder color.
        startColor = occluder.material.color;
        occluder.material.SetColor("_Color", startColor.SetA(0));
    }
    
    internal void SetCamera(Camera cam)
    {
        transform.parent = cam.transform;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
    public void FadeIn(float duration, System.Action endAction)
    {
        this.ProgressionAnim(duration, delegate (float progression)
        {
            occluder.material.SetColor("_Color", startColor.SetA(progression));
        }, endAction);
    }
}
