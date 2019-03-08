using UnityEngine;
using Valve.VR.InteractionSystem;

public class ObjectDropper : MonoBehaviour {

    [SerializeField]
    GameObject hacheTemplate, medaillonTemplate;
    [SerializeField]
    Transform hacheTransform, medaillonTransform;
    public Interactable InteractableObject { get; private set; }

    public enum ObjectType { Hache, Medaillon }

    public void DropItem(ObjectType typeToShow)
    {
        switch (typeToShow)
        {
            case ObjectType.Hache:
                InteractableObject = Instantiate(hacheTemplate, hacheTransform).GetComponent<Interactable>();
                break;
            case ObjectType.Medaillon:
                InteractableObject = Instantiate(medaillonTemplate, medaillonTransform).GetComponent<Interactable>();
                break;
            default:
                break;
        }
        InteractableObject.GetComponent<Rigidbody>().isKinematic = true;
        InteractableObject.onAttachedToHand += delegate { InteractableObject.GetComponent<Rigidbody>().isKinematic = false; };
    }
}
