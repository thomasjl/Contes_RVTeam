using UnityEngine;

public class ParentOnAwake : MonoBehaviour
{
    [SerializeField]
    Transform parent;


    private void Awake()
    {
        transform.parent = parent;
    }
}
