using System.Collections;
using UnityEngine;

public class DwellAxis : MonoBehaviour {

    [SerializeField]
    SphereCollider attachCollider;
    [SerializeField]
    Transform bucket;
    [SerializeField]
    BoxCollider bucketLimit;
    Vector2 bucketUpDownLimits;
    DwellCrank crank;

    private void Awake()
    {
        bucketUpDownLimits = new Vector2(bucketLimit.bounds.max.y, bucketLimit.bounds.min.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        DwellCrank otherCrank = other.GetComponentInParent<DwellCrank>();
        if (otherCrank != null && otherCrank.IsGrabbed)
        {
            // Attach the crank and start updating the progression.
            crank = other.GetComponentInParent<DwellCrank>();
            crank.UseAsCrank(Valve.VR.InteractionSystem.CircularDrive.Axis_t.ZAxis);
            crank.transform.position = transform.TransformPoint(attachCollider.center);
            StartCoroutine(UpdateProgression());
        }
    }

    IEnumerator UpdateProgression()
    {
        float previousDrive = crank.LinearMapping;
        while (true)
        {
            transform.localEulerAngles = transform.localEulerAngles.SetZ(Mathf.Lerp(-180, 180, crank.LinearMapping));
            if (Mathf.Abs(previousDrive - crank.LinearMapping) < .8f)
            {
                bucket.Translate(Vector3.up * (previousDrive - crank.LinearMapping));
                // Clamp position.
                if (bucket.position.y > bucketUpDownLimits.x)
                    bucket.position = bucket.position.SetY(bucketUpDownLimits.x);
                else if (bucket.position.y < bucketUpDownLimits.y)
                    bucket.position = bucket.position.SetY(bucketUpDownLimits.y);
            }
            previousDrive = crank.LinearMapping;
            yield return null;
        }
    }
}
