using UnityEngine;

public class ClockRotate : MonoBehaviour
{
    [SerializeField]
    bool inverser;


    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * (inverser ? -1 : 1), Space.Self);
    }
}
