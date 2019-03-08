using UnityEngine;

public class Buche : MonoBehaviour {

    [SerializeField]
    Material capMaterial;

    private void OnTriggerEnter(Collider other)
    {
        Hache hache = other.GetComponent<Hache>();
        if (hache && hache.IsSlicing)
            Cut(hache);
    }

    void Cut(Hache hache)
    {
        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(transform.GetChild(0).gameObject, transform.position, hache.SliceDirection, capMaterial);
    }
}
