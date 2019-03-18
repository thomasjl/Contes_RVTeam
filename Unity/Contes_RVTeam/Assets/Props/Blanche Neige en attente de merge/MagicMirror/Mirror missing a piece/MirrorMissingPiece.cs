using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MirrorMissingPiece : MonoBehaviour {

    [SerializeField]
    GameObject missingPiece;

    public GameObject hider;

    public GameObject notBrokenMirror;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == missingPiece && other.GetComponent<Interactable>().IsGrabbed())
        {            
            other.GetComponent<Interactable>().attachedToHand = null;
            Destroy(missingPiece);

            hider.SetActive(false);

            /*
            // Attach the piece and show the real objects.
            missingPiece.GetComponent<Interactable>().attachedToHand = null;
           
            missingPiece.transform.localPosition = Vector3.zero;
            missingPiece.transform.localRotation = Quaternion.identity;
            */

            StartCoroutine(LaunchFadeMirror());

        }
    }

    IEnumerator LaunchFadeMirror()
    {       
        yield return new WaitForSeconds(2);
        notBrokenMirror.SetActive(false);
        MagicMirrorInteraction.instance.ShowWebcamMirror();
    }


}
