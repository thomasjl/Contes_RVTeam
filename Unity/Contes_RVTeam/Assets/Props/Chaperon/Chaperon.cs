using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Chaperon : MonoBehaviour {

    public Transform PlayerHead { get { return Player.instance.headCollider.transform; } }
    bool GrabbableIsClose { get { return Vector3.Distance(grabbable.transform.position, PlayerHead.position - Vector3.up * .2f) < .4f; } }
    [SerializeField]
    ChaperonGrabbable grabbable;
    [SerializeField]
    ChaperonWearable wearable;

    [SerializeField]
    Transform fakeGround;
    bool fakeGroundMoves;
    [SerializeField]
    float clothHeight = 2;

    Interactable interactable;


    private void Awake()
    {
        interactable = grabbable.GetComponent<Interactable>();
        // React to grabbing the grabbable.
        interactable.onAttachedToHand += delegate { OnGrabbed(); };
        interactable.onDetachedFromHand += delegate { OnUngrabbed(); };
    }

    private void Start()
    {
        wearable.gameObject.SetActive(false);
        AttachToDwell();
    }


    public void AttachToDwell()
    {
        // Snap to the bucket and be ready to be grabbed.
        grabbable.transform.position = Dwell.instance.Bucket.position;
        grabbable.transform.parent = Dwell.instance.Bucket;
        grabbable.Rb.isKinematic = true;
        grabbable.Interactable.onAttachedToHand += DetachFromDwell;
        fakeGround.transform.position = grabbable.transform.position.SetY(Dwell.instance.Bottom);
    }
    void DetachFromDwell(Hand hand)
    {
        // Reset grabbable.
        grabbable.transform.parent = transform;
        grabbable.Rb.isKinematic = false;
        grabbable.Interactable.onAttachedToHand -= DetachFromDwell;
    }


    #region Equiping the grabbable ------------------
    void OnGrabbed()
    {
        StartCoroutine(TryEquiping());
    }

    void OnUngrabbed()
    {
        PlayGroundCorrection();
        if (GrabbableIsClose)
            // Validate equip.
            grabbable.gameObject.SetActive(false);
    }

    IEnumerator TryEquiping()
    {
        bool grabbableWasClose = GrabbableIsClose;
        while (grabbable.gameObject.activeSelf)
        {
            if (GrabbableIsClose && !grabbableWasClose)
            {
                // Temporary equip.
                grabbable.gameObject.Hide();
                wearable.gameObject.SetActive(true);
                wearable.Equip();
                grabbableWasClose = GrabbableIsClose;
                PlayGroundCorrection();
            }
            else if (!GrabbableIsClose && grabbableWasClose)
            {
                // Cancel temporary equip.
                wearable.gameObject.SetActive(false);
                grabbable.gameObject.Show();
                grabbableWasClose = GrabbableIsClose;
                PlayGroundCorrection();
            }
            yield return null;
        }
    }
    #endregion ------------------------------------


    #region Fake ground ----------
    void PlayGroundCorrection()
    {
        if (fakeGroundMoves)
            return;
        fakeGround.position = grabbable.transform.position;
        if (animatingFakeGround != null)
            StopCoroutine(animatingFakeGround);
        animatingFakeGround = AnimatingFakeGround();
        StartCoroutine(animatingFakeGround);
    }

    public void MoveFakeGroundOnPlayerHead()
    {
        if (fakeGroundMoves)
            return;
        fakeGround.position = PlayerHead.position.SetY(Player.instance.transform.position.y);
    }

    IEnumerator animatingFakeGround;
    IEnumerator AnimatingFakeGround()
    {
        fakeGroundMoves = true;
        float progression = 0;
        // Move the ground upwards slowly.
        while (progression < 1)
        {
            fakeGround.position = fakeGround.position.SetY(Player.instance.transform.position.y - clothHeight * (1 - progression));
            progression += Time.deltaTime;
            yield return null;
        }
        fakeGroundMoves = false;
        animatingFakeGround = null;
    }
    #endregion --------------------
}