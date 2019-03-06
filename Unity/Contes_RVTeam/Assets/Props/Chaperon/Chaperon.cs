using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Chaperon : MonoBehaviour {

    Transform PlayerHead { get { return Player.instance.headCollider.transform; } }
    [SerializeField]
    Transform grabbable;
    bool GrabbableCloseToHead { get { return Vector3.Distance(grabbable.position, PlayerHead.position) < .4f; } }
    [SerializeField]
    Transform wearable;
    Cloth wearableCloth;

    [SerializeField]
    Transform fakeGround;
    bool fakeGroundMoves;
    [SerializeField]
    float clothHeight = 2;

    [SerializeField]
    float rotationStrength = .05f;

    public enum State { Attached, Grabbable, Wearable }
    State state = State.Attached;

    Interactable interactable;


    private void Awake()
    {
        wearableCloth = wearable.GetComponentInChildren<Cloth>(true);
        interactable = grabbable.GetComponent<Interactable>();
        // React to grabbing the grabbable.
        interactable.onAttachedToHand += delegate { OnGrabbed(); };
        interactable.onDetachedFromHand += delegate { OnUngrabbed(); };
    }

    void Start()
    {
        SetState(State.Grabbable);
    }


    public void SetState(State newState)
    {
        if (newState == state)
            return;
        state = newState;
        switch (state)
        {
            case State.Attached:
                StopAllCoroutines();
                break;
            case State.Grabbable:
                grabbable.gameObject.SetActive(true);
                wearable.gameObject.SetActive(false);
                StopAllCoroutines();
                break;
            case State.Wearable:
                grabbable.gameObject.SetActive(false);
                wearable.gameObject.SetActive(true);
                EquipWearable();
                break;
            default:
                break;
        }
        PlayGroundCorrection();
    }


    void OnGrabbed()
    {
        StartCoroutine(CheckForWearingGrabbable());
    }

    void OnUngrabbed()
    {
        PlayGroundCorrection();
        if (GrabbableCloseToHead)
            grabbable.gameObject.SetActive(false);
    }

    IEnumerator CheckForWearingGrabbable()
    {
        bool wasCloseToHead = GrabbableCloseToHead;
        while (grabbable.gameObject.activeSelf)
        {
            if (GrabbableCloseToHead)
            {
                if (!wasCloseToHead)
                {
                    grabbable.gameObject.Hide();
                    wearable.gameObject.SetActive(true);
                    EquipWearable();
                    wasCloseToHead = GrabbableCloseToHead;
                }
            }
            else
            {
                wearable.gameObject.SetActive(false);
                grabbable.gameObject.Show();
                wasCloseToHead = GrabbableCloseToHead;
            }
            yield return null;
        }
    }

    public void EquipWearable()
    {
        // Show the correct cloth, snap to the player's head and start to follow the head.
        wearable.position = PlayerHead.position;
        wearable.rotation = Quaternion.identity;
        StartCoroutine(UpdatingWearable());
    }

    IEnumerator UpdatingWearable()
    {
        // Reset the cloth.
        wearableCloth.enabled = false;
        yield return null;
        wearableCloth.enabled = true;
        while (wearable.gameObject.activeSelf)
        {
            // Set cloth transform.
            wearable.position = PlayerHead.position;
            Vector3 targetRotation = transform.eulerAngles.SetY(PlayerHead.eulerAngles.y);
            wearable.rotation = Quaternion.Lerp(wearable.rotation, Quaternion.Euler(targetRotation), rotationStrength);
            // Update the fake ground.
            if (!fakeGroundMoves)
                fakeGround.position = PlayerHead.position.SetY(Player.instance.transform.position.y);
            yield return null;
        }
    }

    void PlayGroundCorrection()
    {
        // Move the ground upwards slowly.
        fakeGroundMoves = true;
        fakeGround.position = grabbable.position;
        this.MakeProgressionAnim(1, delegate (float progression)
        {
            fakeGround.position = fakeGround.position.SetY(Player.instance.transform.position.y - clothHeight * (1 - progression));
        }, delegate
        {
            fakeGroundMoves = false;
        });
    }
}