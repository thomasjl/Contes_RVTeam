using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Crank : MonoBehaviour {

    public Interactable Interactable { get; private set; }
    Throwable throwable;
    Rigidbody rb;
    CircularDrive circularDrive;

    private GameObject leaf;

    public bool IsGrabbed { get { return Interactable.attachedToHand != null; } }
    public float LinearMapping {
        get {
            if (!circularDrive || !circularDrive.linearMapping)
                return 0;
            return circularDrive.linearMapping.value;
        }
    }

    private void Awake()
    {
        Interactable = GetComponent<Interactable>();
        throwable = GetComponent<Throwable>();
        rb = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        StartCoroutine("IsActorInScene");
    }

    IEnumerator IsActorInScene()
    {
        bool actorIsInscene = false;
        while(!actorIsInscene)
        {
            if(InterractionManager.instance.actorIsInScene)
            {
                actorIsInscene = true;
            }
            yield return null;
        }
        InstantiateParticles();
    }

    private void InstantiateParticles()
    {
        if (InterractionManager.instance.GetChoices()[0] == 4)
        {
            leaf = Instantiate(InterractionManager.instance.GetLeaves(), transform.position, Quaternion.identity);
            //leaf.transform.parent = transform;
            Debug.Log("true");
            leaf.GetComponent<Leaves>().showParticle = true;
        }
    }

    public void UseAsCrank(CircularDrive.Axis_t axis)
    {
        leaf.GetComponent<Leaves>().showParticle = false;
        Destroy(leaf,1.5f);
        // Remove the throwable behaviour and setup the crank behaviour.
        if (IsGrabbed)
            Interactable.attachedToHand.DetachObject(gameObject);
        Destroy(throwable);
        Destroy(rb);
        circularDrive = gameObject.AddComponent<CircularDrive>();
        circularDrive.axisOfRotation = axis;
        circularDrive.hoverLock = true;
    }
}
