using UnityEngine;

public class Thorn : MonoBehaviour {

    [SerializeField]
    bool startFromFirstChild = true;
    Hache currenthache;
    int childrenToRemove = 0;
    public bool Clear { get { return childrenToRemove == 0; } }

    private void Awake()
    {
        childrenToRemove = transform.childCount - 1;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Hache hache = collider.GetComponentInParent<Hache>();
        if (hache && hache.IsSlicing)
        {
            currenthache = hache;
            Cut();
        }
    }

    void Cut()
    {
        if (childrenToRemove <= 0)
            return;
        RemoveChild(startFromFirstChild ? 0 : transform.childCount - 1);
        childrenToRemove--;
    }

    void RemoveChild(int index)
    {
        Rigidbody newSlice = transform.GetChild(index).gameObject.AddComponent<Rigidbody>();
        newSlice.AddForce(currenthache.SliceDirection * 5);
        transform.GetChild(index).parent = null;
    }
}
