using UnityEngine;
using UnityEngine.UI;

public class CamOccluder : MonoBehaviour {

    Image occluder;
    Color startColor;

    public static CamOccluder Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        occluder = GetComponentInChildren<Image>();
        // Setup occluder color.
        startColor = occluder.color;
        occluder.color = startColor.SetA(0);
    }
    
    internal void SetCamera(Camera cam)
    {
        transform.parent = cam.transform;
        GetComponent<Canvas>().worldCamera = cam;
    }

    public void FadeIn(float duration, System.Action endAction)
    {
        this.ProgressionAnim(duration, delegate (float progression)
        {
            occluder.color = startColor.SetA(progression);
        }, endAction);
    }
}
