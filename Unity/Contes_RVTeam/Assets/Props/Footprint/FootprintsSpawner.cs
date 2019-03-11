using UnityEngine;

[RequireComponent(typeof(BezierSpline))]
[ExecuteInEditMode]
public class FootprintsSpawner : MonoBehaviour {

    [SerializeField]
    GameObject leftFoot, rightFoot;
    [SerializeField]
    int amount = 20;

    BezierSpline spline;

    private void Awake()
    {
        spline = GetComponent<BezierSpline>();
    }

    public void Spawn()
    {
        transform.DestroyChidren();
        bool left = false;
        for (int i = 0; i < amount; i++)
        {
            Transform newFootprint = Instantiate(left ? leftFoot : rightFoot, transform).transform;
            newFootprint.position = spline.GetPoint(i / (float)amount);
            newFootprint.rotation = Quaternion.LookRotation(spline.GetDirection(i / (float)amount), Vector3.up);
            left = !left;
        }
    }
}
