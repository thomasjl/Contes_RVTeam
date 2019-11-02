using UnityEngine;

public class Dwell : MonoBehaviour {

    public DwellAxis Axis { get; private set; }
    public Transform Bucket { get { return Axis.Bucket; } }
    public float Bottom { get { return Axis.Bottom; } }

    public static Dwell instance;
    private void Awake()
    {
        instance = this;
        Axis = GetComponentInChildren<DwellAxis>(true);
    }
}
