using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Handles the mirror render texture and the grab on the mirror objects.
/// </summary>
public class Mirror : MonoBehaviour {

    Renderer rend;
    [SerializeField]
    Transform mirrorObjects;
    float AlphaFromDirection { get { return 1 - Vector3.Dot(transform.forward, Player.instance.headCollider.transform.forward).Remap(.8f, 1, 0, 1); } }

    public delegate void MirrorEventHandler();
    public event MirrorEventHandler Shown;
    public event MirrorEventHandler Hidden;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        SetChildrenHiddenFromCamera(mirrorObjects, true);

        // Setup interactables.
        Interactable[] interactables = mirrorObjects.GetComponentsInChildren<Interactable>();
        foreach (Interactable interactable in interactables)
            interactable.GetOrAddComponent<MirrorInteractable>().SetMirror(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HeadCollider"))
        {
            SetChildrenHiddenFromCamera(mirrorObjects, false);
            // Fade in then update the mirror.
            StopAllCoroutines();
            this.ProgressionAnim(1, delegate (float progression)
            {
                SetAlpha(Mathf.Lerp(GetAlpha(), AlphaFromDirection, progression));
            }, delegate
            {
                StartCoroutine(UpdateMirror());
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
            this.ProgressionAnim(.2f, delegate (float progression)
            {
                SetAlpha(Mathf.Lerp(AlphaFromDirection, 1, progression));
            },
            delegate
            {
                SetChildrenHiddenFromCamera(mirrorObjects, true);
                // Call event.
                if (Hidden != null)
                    Hidden();
            });
        }
    }

    IEnumerator UpdateMirror()
    {
        while (true)
        {
            SetAlpha(AlphaFromDirection);
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

    void SetAlpha(float alpha)
    {
        rend.material.SetColor("_Color", new Color(1, 1, 1, alpha));
    }

    float GetAlpha()
    {
        return rend.material.GetColor("_Color").a;
    }
}
