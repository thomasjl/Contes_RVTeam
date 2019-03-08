using System.Collections;
using UnityEngine;

public class Thorn : MonoBehaviour {

    Hache currenthache;
    int childrenToRemove = 0;
    public bool Clear { get; private set; }
    [SerializeField]
    AudioClip cut, flatten;

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
        RemoveChild(transform.childCount - 1 - childrenToRemove);
        childrenToRemove--;
        AudioSource.PlayClipAtPoint(cut, transform.position);
    }

    public void Flatten()
    {
        if (!GetComponent<Animator>())
            return;
        GetComponent<Animator>().SetTrigger("flatten");
        AudioSource.PlayClipAtPoint(flatten, transform.position);
        StartCoroutine(WaitThenSetCleared());
    }

    IEnumerator WaitThenSetCleared()
    {
        yield return new WaitForSeconds(2);
        Clear = true;
    }

    void RemoveChild(int index)
    {
        Rigidbody newSlice = transform.GetChild(index).gameObject.AddComponent<Rigidbody>();
        newSlice.AddForce(currenthache.SliceDirection * 5);
        transform.GetChild(index).parent = null;
        if (childrenToRemove <= 0)
            Clear = true;
    }
}
