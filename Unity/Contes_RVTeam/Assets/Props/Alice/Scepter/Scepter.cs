using UnityEngine;
using Valve.VR.InteractionSystem;

public class Scepter : MonoBehaviour {

    Interactable interactable;
    public bool ScaledDown { get; private set; }

    private void Awake()
    {
        GetComponent<PaintingInteractable>().Detached += ListenToPotions;
        interactable = GetComponent<Interactable>();
    }

    void ListenToPotions()
    {
        Potion.ScaledDown += ScaleDown;
        Potion.ScaledNormal += ResetScale;
    }

    void ScaleDown()
    {
        // Change the scale after a delay if we are note grabbed.
        if (interactable.IsGrabbed())
            transform.localScale = Player.instance.transform.localScale;
        else
            this.Timer(1, delegate
            {
                transform.localScale = Player.instance.transform.localScale;
                ScaledDown = true;
            });
    }

    void ResetScale()
    {
        // Change the scale after a delay if we are note grabbed.
        if (interactable.IsGrabbed())
            transform.localScale = Vector3.one;
        else
            this.Timer(1, delegate
            {
                transform.localScale = Vector3.one;
                ScaledDown = false;
            });
    }

    public void UseAsKey()
    {
        Destroy(GetComponent<Throwable>());
        Destroy(interactable);
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
