using UnityEngine;
using Valve.VR.InteractionSystem;

public class CheshireCat : MonoBehaviour {

    [SerializeField]
    Transform spawnPointsParent;
    int previousChoice = 0;
    [SerializeField]
    Transform cat;

    [Header("Eyes")]
    [SerializeField]
    Transform leftEye;
    [SerializeField]
    Transform rightEye;
    [SerializeField]
    BoxCollider leftLimits, rightLimits;
    float leftMinX;
    float leftMaxX;
    float rightMinX;
    float rightMaxX;

    [Space]
    [SerializeField]
    float playerDistance = 2;
    [SerializeField]
    new AudioSource audio;
    bool talked = false;

    Vector3 Target { get { return Player.instance.transform.position; } }

    private void Awake()
    {
        leftMinX = cat.InverseTransformPoint(leftLimits.bounds.min).x;
        leftMaxX = cat.InverseTransformPoint(leftLimits.bounds.max).x;
        rightMinX = cat.InverseTransformPoint(rightLimits.bounds.min).x;
        rightMaxX = cat.InverseTransformPoint(rightLimits.bounds.max).x;
    }

    private void Start()
    {
        Teleport();
    }

    public void Teleport()
    {
        previousChoice = Utilities.ExclusiveRange(0, spawnPointsParent.childCount, previousChoice);
        cat.SetParent(spawnPointsParent.GetChild(previousChoice), false);
    }

    private void Update()
    {
        LookAt(Player.instance.transform.position);
        if (!talked && Vector3.Distance(Target, cat.position) > playerDistance)
            Talk();
    }

    void Talk()
    {
        audio.Play();
        talked = true;
    }

    void LookAt(Vector3 target)
    {
        if (Vector3.Dot((target - cat.position).normalized, cat.forward) < 0)
            return;

        float dotLeft = Vector3.Dot((target - leftLimits.transform.position).normalized, cat.right);
        float dotRight = Vector3.Dot((target - rightLimits.transform.position).normalized, cat.right);

        float newLeftX = Mathf.Lerp(leftMinX, leftMaxX, (dotLeft + 1) * .5f);
        float newRightX = Mathf.Lerp(rightMinX, rightMaxX, (dotRight + 1) * .5f);

        leftEye.localPosition = leftEye.localPosition.SetX(newLeftX);
        rightEye.localPosition = rightEye.localPosition.SetX(newRightX);
    }
}
