using UnityEngine;

public class PlayAreaSizeDebug : MonoBehaviour
{

    bool activated;

    private void Awake()
    {
#if !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponentInParent<Valve.VR.InteractionSystem.Player>())
            return;
        OnScreenPrinter.Print("player out of bounds");
        activated = true;
    }

    private void LateUpdate()
    {
        if (!activated)
            OnScreenPrinter.Clear();
        activated = false;
    }
}
