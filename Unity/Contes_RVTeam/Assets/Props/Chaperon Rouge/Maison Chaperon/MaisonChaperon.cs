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

    private int givenObject;
    private bool objectGiven;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dropper = objectAnimator.GetComponent<ObjectDropper>();
        objectAnimator.gameObject.SetActive(false);
        givenObject = 0;
        objectGiven = false;

        //GiveItem(ObjectDropper.ObjectType.Hache);
    }

    public void SetSecondChoice(int choice)
    {
        if(choice==6)
        {
            //medaillon
            givenObject= 6;
            ThornSelect.instance.ThornsUsed(0);
        }
        else
        {
            givenObject = 8;
            //hache
            ThornSelect.instance.ThornsUsed(1);

        }
    }


    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("HeadCollider") && givenObject != 0 && !objectGiven && Chaperon.instance.isEquiped)
        {           
            if (givenObject == 6)
            {
                GiveItem(ObjectDropper.ObjectType.Medaillon);
                Debug.Log("medaillon");
            }
            else
            {
                GiveItem(ObjectDropper.ObjectType.Hache);

                Debug.Log("hache");
            }
            objectGiven = true;
            
        }
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
