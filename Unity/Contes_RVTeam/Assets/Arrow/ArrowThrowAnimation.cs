using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ArrowThrowAnimation : MonoBehaviour
{
    [SerializeField]
    Transform mirror;
    [SerializeField]
    Throwable throwable;
    Quaternion startRotation;


    private void Start()
    {
        startRotation = transform.localRotation;

        // Animate towards the mirror on pickup.
        throwable.onPickUp.AddListener(delegate { StartCoroutine(AnimationRoutine()); });
        throwable.onDetachFromHand.AddListener(delegate
        {
            StopAllCoroutines();
            transform.localRotation = startRotation;
        });
    }


    IEnumerator AnimationRoutine()
    {
        float progression = 0;
        Vector3 startLocalPosition = transform.localPosition;
        while (true)
        {
            transform.LookAt(mirror.position);

            // Throw hint: move forward fast, then slowly come back.
            if (progression < .1f)
                transform.Translate(Vector3.forward * .01f);
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, startLocalPosition, progression.Remap(.3f, 1, 0, 1));
            progression += Time.deltaTime;
            if (progression > 1)
                progression = 0;
            yield return null;
        }
    }
}
