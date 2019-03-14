using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Handles the painting render texture and the grab on the painting objects.
/// </summary>
public class Painting : MonoBehaviour {

    Renderer rend;
    [SerializeField]
    Transform paintingObjects;
    float DistanceFromHeadset { get { return Vector3.Distance(transform.position, Player.instance.headCollider.transform.position); } }
    float AlphaFromDirection { get { return 1 - Vector3.Dot(transform.forward, Player.instance.headCollider.transform.forward).Remap(.8f, 1, 0, 1); } }
    float AlphaFromDirectionCompensated { get { return DistanceFromHeadset < .3f ? 0 : AlphaFromDirection; } }

    public delegate void PaintingEventHandler();
    public event PaintingEventHandler Shown;
    public event PaintingEventHandler Hidden;

    float PaintingAlpha {
        get {
            return rend.material.GetColor("_Color").a;
        }
        set {
            //rend.material.SetColor("_Color", new Color(1, 1, 1, value));
        }
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        SetChildrenHiddenFromCamera(paintingObjects, true);

        // Setup interactables.
        Interactable[] interactables = paintingObjects.GetComponentsInChildren<Interactable>();
        foreach (Interactable interactable in interactables)
            interactable.GetOrAddComponent<PaintingInteractable>().SetPainting(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HeadCollider"))
        {
            SetChildrenHiddenFromCamera(paintingObjects, false);
            // Fade in then update the painting.
            StopAllCoroutines();
            this.ProgressionAnim(1, delegate (float progression)
            {
                PaintingAlpha = Mathf.Lerp(PaintingAlpha, AlphaFromDirectionCompensated, progression);
            }, delegate
            {
                StartCoroutine(UpdatePainting());
            });
            // Call event.
            if (Shown != null)
                Shown();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HeadCollider"))
        {
            StopAllCoroutines();
            this.ProgressionAnim(.1f, delegate (float progression)
            {
                PaintingAlpha = Mathf.Lerp(AlphaFromDirectionCompensated, 1, progression);
            },
            delegate
            {
                SetChildrenHiddenFromCamera(paintingObjects, true);
                // Call event.
                if (Hidden != null)
                    Hidden();
            });
        }
    }

    IEnumerator UpdatePainting()
    {
        while (true)
        {
            PaintingAlpha = AlphaFromDirectionCompensated;
            yield return null;
        }
    }

    void SetChildrenHiddenFromCamera(Transform parent, bool state)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.layer = state ? LayerMask.NameToLayer("Hidden") : 0;
            SetChildrenHiddenFromCamera(parent.GetChild(i), state);
        }
    }
}
