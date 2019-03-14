using System.Collections;
using UnityEngine;

public class DwellRope : MonoBehaviour {

    [SerializeField]
    Transform rope, ropeWinding;
    Quaternion ropeStartRot;
    float ropeStartTiling;
    float windingRopeStartScale;
    float windingRopeMinScale;
    Renderer ropeRend;
    DwellAxis dwellAxis;

    private void Awake()
    {
        // Setup rope.
        ropeRend = rope.GetComponent<Renderer>();
        ropeStartTiling = ropeRend.material.GetTextureScale("_MainTex").x;
        ropeStartRot = rope.rotation;
        // Setup winding rope.
        windingRopeStartScale = ropeWinding.localScale.y;
        windingRopeMinScale = windingRopeStartScale * .1f;
        // Get the dwell and update ourselves when it has the crank.
        dwellAxis = GetComponentInParent<DwellAxis>();
        dwellAxis.HasCrank += delegate { StartCoroutine(UpdateRopeLoop()); };
    }

    private void Start()
    {
        UpdateRope();
    }

    IEnumerator UpdateRopeLoop()
    {
        while (true)
        {
            UpdateRope();
            yield return null;
        }
    }

    void UpdateRope()
    {
        // Animate the rope.
        rope.position = dwellAxis.Bucket.position;
        rope.rotation = ropeStartRot;
        rope.localScale = Vector3.one.SetZ(Vector3.Distance(rope.position, transform.position) / rope.lossyScale.x);
        ropeRend.material.SetTextureScale("_MainTex", new Vector2(ropeStartTiling, ropeStartTiling * rope.localScale.z));
        // Animate the winding rope.
        ropeWinding.localScale = ropeWinding.localScale.SetY(Mathf.Lerp(windingRopeMinScale, windingRopeStartScale, dwellAxis.Progression));
    }
}
