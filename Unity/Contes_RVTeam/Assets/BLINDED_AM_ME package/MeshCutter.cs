using UnityEngine;

public class MeshCutter : MonoBehaviour {

    [SerializeField]
    Material capMaterial;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                GameObject victim = hit.collider.gameObject;
                GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);
            }
        }
    }
}
