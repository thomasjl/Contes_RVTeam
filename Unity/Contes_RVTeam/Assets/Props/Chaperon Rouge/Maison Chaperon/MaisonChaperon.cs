using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class MaisonChaperon : MonoBehaviour {

    [SerializeField]
    Animator doorAnimator, objectAnimator;
    ObjectDropper dropper;
    [SerializeField]
    float giveItemDelay = 3;

    [SerializeField]
    UnityEvent ObjectGrabbed;

    public static MaisonChaperon instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dropper = objectAnimator.GetComponent<ObjectDropper>();
        objectAnimator.gameObject.SetActive(false);
    }

    public void debugGive()
    {
        GiveItem(ObjectDropper.ObjectType.Medaillon);
    }

    public void GiveItem(ObjectDropper.ObjectType itemType)
    {
        doorAnimator.SetTrigger("open");
        StartCoroutine(WaitAndOpen(itemType));
    }

    IEnumerator WaitAndOpen(ObjectDropper.ObjectType itemType)
    {
        yield return new WaitForSeconds(giveItemDelay);
        objectAnimator.gameObject.SetActive(true);
        objectAnimator.SetTrigger("drop");
        dropper.DropItem(itemType);
        dropper.InteractableObject.onAttachedToHand += OnObjectGrabbed;
    }

    void OnObjectGrabbed(Hand hand)
    {
        ObjectGrabbed.Invoke();
        dropper.InteractableObject.onAttachedToHand -= OnObjectGrabbed;
    }
}
