﻿using System.Collections;
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

    public bool isEquiped;
    public event System.Action Equipped;

    public enum AttachPoint { Dwell, Tree}
    public AttachPoint PointOfAttach { get; private set; }

    public static Chaperon Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // React to grabbing the grabbable.
        grabbable.Interactable.onAttachedToHand += delegate { OnGrabbed(); };
        grabbable.Interactable.onDetachedFromHand += delegate { OnUngrabbed(); };
        grabbable.Detached += PlayGroundCorrection;
        // Hide wearable.
        wearable.gameObject.SetActive(false);
        isEquiped = false;
    }


    /// <summary>
    /// 2: tree, else: dwell.
    /// </summary>
    /// <param name="choice"></param>
    public void SetFirstChoice(AttachPoint pointOfAttach)
    {
        PointOfAttach = pointOfAttach;
        if (PointOfAttach == AttachPoint.Tree)
            AttachToTree();
        else
            AttachToDwell();
    }

    public void AttachToDwell()
    {
        grabbable.AttachToDwell();
        fakeGround.transform.position = grabbable.transform.position.SetY(Dwell.instance.Bottom);
    }

    public void AttachToTree()
    {
        grabbable.AttachToTree();
    }
    

    #region Equiping the grabbable ------------------
    void OnGrabbed()
    {
        StartCoroutine(TryEquiping());
    }

    void OnUngrabbed()
    {
        if (grabbable.Attached)
            return;
        PlayGroundCorrection();
        if (GrabbableIsClose)
        {
            // Validate equip.
            grabbable.gameObject.SetActive(false);
            isEquiped = true;
            // Call event.
            if (Equipped != null)
                Equipped();
        }
    }

    IEnumerator TryEquiping()
    {
        bool grabbableWasClose = GrabbableIsClose;
        while (grabbable.gameObject.activeSelf)
        {
            if (GrabbableIsClose && !grabbableWasClose)
            {
                // Temporary equip.
                grabbable.gameObject.HideChildren();
                wearable.gameObject.SetActive(true);
                wearable.Equip();
                grabbableWasClose = GrabbableIsClose;
                PlayGroundCorrection();
            }
            else if (!GrabbableIsClose && grabbableWasClose)
            {
                // Cancel temporary equip.
                wearable.gameObject.SetActive(false);
                grabbable.gameObject.ShowChildren();
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
        if (animatingFakeGround != null)
            StopCoroutine(animatingFakeGround);
        animatingFakeGround = AnimatingFakeGround();
        StartCoroutine(animatingFakeGround);
    }

    IEnumerator animatingFakeGround;
    IEnumerator AnimatingFakeGround()
    {
        fakeGroundMoves = true;
        fakeGround.position = grabbable.transform.position;
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

    public void MoveFakeGroundOnPlayerHead()
    {
        if (fakeGroundMoves)
            return;
        fakeGround.position = PlayerHead.position.SetY(Player.instance.transform.position.y);
    }
    #endregion --------------------
}