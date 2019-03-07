using UnityEngine;

public class BucketAnimation : MonoBehaviour {

    [SerializeField]
    float handleStrength = 5;
    [SerializeField]
    float bucketStrength = 5;

    void Update()
    {
        transform.eulerAngles = Vector3.one.SetY(handleStrength * Mathf.Sin(Time.time));
        transform.GetChild(0).localEulerAngles = Vector3.zero.SetX(bucketStrength * Mathf.Sin(Time.time + 1));
    }
}
