using UnityEngine;
using System.Collections;


public class TriangleExplosion : MonoBehaviour {

    [SerializeField]
    AudioClip sfx;
    [SerializeField]
    float sfxVolume = .8f;

    public void Explode(bool destroy)
    {
        StartCoroutine(SplittingMesh(destroy));
    }

    IEnumerator SplittingMesh(bool destroy)
    {

        Debug.Log("splitt");
      

        // Deactivate collider.
        if (GetComponent<Collider>())
            GetComponent<Collider>().enabled = false;

        // Get mesh.
        Mesh M = new Mesh();
        if (GetComponent<MeshFilter>())
            M = GetComponent<MeshFilter>().mesh;
        else if (GetComponent<SkinnedMeshRenderer>())
            M = GetComponent<SkinnedMeshRenderer>().sharedMesh;

        // Get materials.
        Material[] materials = new Material[0];
        if (GetComponent<MeshRenderer>())
            materials = GetComponent<MeshRenderer>().materials;
        else if (GetComponent<SkinnedMeshRenderer>())
            materials = GetComponent<SkinnedMeshRenderer>().materials;

        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;

        Debug.Log("nbr triangle "+ M.subMeshCount);

        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {
            int[] indices = M.GetTriangles(submesh);

            for (int i = 0; i < indices.Length; i += 3)
            {
                

                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                GO.transform.parent = transform.parent;
                //GO.layer = LayerMask.NameToLayer("Particle");
                //GO.transform.position = transform.position +  new Vector3(0,0,Random.Range(0.5f,1f));
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.transform.localScale = transform.localScale;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                // Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(10f, 20f), transform.position.y + Random.Range(10f, 20f), transform.position.z + Random.Range(10f, 20f));
                // GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(5f, 10f), explosionPos, 0.5f);

                GO.AddComponent<Rigidbody>();
                GO.GetComponent<Rigidbody>().AddForce(Vector3.forward * Random.Range(50f, 1000f));

                Destroy(GO, Random.Range(1f, 2f));
            }
        }

        GetComponent<Renderer>().enabled = false;
        if (sfx)
            AudioSource.PlayClipAtPoint(sfx, transform.position, sfxVolume);

        yield return new WaitForSeconds(1.0f);
        if (destroy)
            Destroy(gameObject);
    }
}