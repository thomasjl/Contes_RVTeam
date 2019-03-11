using UnityEngine;
using Valve.VR.InteractionSystem;

public class CheshireCat : MonoBehaviour {

    [SerializeField]
    Transform spawnPointsParent;
    [SerializeField]
    Transform cat;

    [Space]
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

    [SerializeField]
    Transform testtarget;
    int previousChoice = 0;

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
