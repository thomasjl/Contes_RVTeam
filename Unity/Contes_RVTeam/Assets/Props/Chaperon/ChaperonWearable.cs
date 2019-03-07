using System.Collections;
using UnityEngine;

public class ChaperonWearable : MonoBehaviour {

    Chaperon chaperon;
    Cloth cloth;

    [SerializeField]
    float rotationStrength = .05f;

    private void Awake()
    {
        chaperon = GetComponentInParent<Chaperon>();
        cloth = GetComponentInChildren<Cloth>(true);
    }


    public void Equip()
    {
        // Show the correct cloth, snap to the player's head and start to follow the head.
        transform.position = chaperon.PlayerHead.position;
        transform.rotation = Quaternion.identity;
        StartCoroutine(UpdatingWearable());
    }

    IEnumerator UpdatingWearable()
    {
        // Reset the cloth.
        cloth.enabled = false;
        yield return null;
        cloth.enabled = true;
        while (gameObject.activeSelf)
        {
            // Set cloth transform.
            transform.position = chaperon.PlayerHead.position;
            Vector3 targetRotation = transform.eulerAngles.SetY(chaperon.PlayerHead.eulerAngles.y);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), rotationStrength);
            chaperon.MoveFakeGroundOnPlayerHead();
            yield return null;
        }
    }
}
