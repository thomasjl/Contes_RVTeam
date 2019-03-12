using UnityEngine;
using Valve.VR.InteractionSystem;

public class MailBox : MonoBehaviour {

    [SerializeField]
    Transform lettersParent;
    [SerializeField]
    Renderer paperRenderer;
    [SerializeField]
    Material poison, jabberwocky;
    CircularDrive circularDrive;

    public enum PaperType { Poison, JabberWocky }

    private void Awake()
    {
        circularDrive = GetComponentInChildren<CircularDrive>();
    }

    public void SetPaperMaterial(PaperType type)
    {
        switch (type)
        {
            case PaperType.Poison:
                paperRenderer.material = poison;
                break;
            case PaperType.JabberWocky:
                paperRenderer.material = jabberwocky;
                break;
            default:
                break;
        }
    }
}
