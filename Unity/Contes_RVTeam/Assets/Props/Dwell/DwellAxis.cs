using System.Collections;
using UnityEngine;

public class DwellAxis : MonoBehaviour {

    [SerializeField]
    SphereCollider attachCollider;
    [SerializeField]
    Transform bucket;
    public Transform Bucket { get { return bucket; } }
    [SerializeField]
    BoxCollider bucketLimit;
    Vector2 bucketUpDownLimits;
    public float Progression { get { return (bucket.position.y - bucketUpDownLimits.y) / (bucketUpDownLimits.x - bucketUpDownLimits.y); } }
    Crank crank;

    public delegate void DwellEventHandler();
    public event DwellEventHandler HasCrank;

    private void Awake()
    {
        bucketUpDownLimits = new Vector2(bucketLimit.bounds.max.y, bucketLimit.bounds.min.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        Crank otherCrank = other.GetComponentInParent<Crank>();
        if (otherCrank != null)// && otherCrank.IsGrabbed)
        {
            // Attach the crank and start updating the progression.
            crank = other.GetComponentInParent<Crank>();
            crank.UseAsCrank(Valve.VR.InteractionSystem.CircularDrive.Axis_t.ZAxis);
            crank.transform.position = transform.TransformPoint(attachCollider.center);
            StartCoroutine(UpdateProgression());
            // Call event.
            if (HasCrank != null)
                HasCrank();
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
                bucket.Translate(Vector3.up * (previousDrive - crank.LinearMapping), Space.World);
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
