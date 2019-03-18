using UnityEngine;
using Valve.VR.InteractionSystem;

public class AppleBox : MonoBehaviour {

    [SerializeField]
    PoisonApple appleTemplate;
    Interactable apple;
    [SerializeField]
    Transform appleSpawnPoint;
    BoxLid boxLid;
    Collider boxBounds;


    private void Awake()
    {
        apple = Instantiate(appleTemplate, appleSpawnPoint).GetComponent<Interactable>();
        apple.transform.localPosition = Vector3.zero;
        apple.transform.eulerAngles = Vector3.one * 10;
        boxLid = GetComponentInChildren<BoxLid>();
        if (boxLid)
        {
            apple.SetGrabEnabled(false);
            boxBounds = GetComponent<Collider>();
            boxLid.Opened += delegate { TrySetAppleGrab(true); };
            boxLid.Closed += delegate { TrySetAppleGrab(false); };
        }
    }

    public void TrySetAppleGrab(bool state)
    {
        if (!boxBounds.bounds.Contains(apple.transform.position))
            return;
        apple.SetGrabEnabled(state);
    }
}
