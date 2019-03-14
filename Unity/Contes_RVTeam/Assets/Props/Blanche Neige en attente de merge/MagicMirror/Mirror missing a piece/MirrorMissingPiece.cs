using UnityEngine;
using Valve.VR.InteractionSystem;

public class MirrorMissingPiece : MonoBehaviour {

    [SerializeField]
    GameObject[] objectsToHide;
    [SerializeField]
    GameObject missingPiece;

    private void Start()
    {
        foreach (GameObject objectToHide in objectsToHide)
            objectToHide.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == missingPiece && other.GetComponent<Interactable>().IsGrabbed())
        {
            // Attach the piece and show the real objects.
            missingPiece.GetComponent<Interactable>().attachedToHand = null;
            missingPiece.GetComponent<Rigidbody>().isKinematic = true;
            missingPiece.transform.localPosition = Vector3.zero;
            missingPiece.transform.localRotation = Quaternion.identity;

           // GetComponent<Renderer>().enabled = false;
            /*
            foreach (GameObject objectToHide in objectsToHide)
            {
                objectToHide.SetActive(true);
            }
            */

            MagicMirrorInteraction.instance.ShowWebcamMirror();

        }
    }


}
