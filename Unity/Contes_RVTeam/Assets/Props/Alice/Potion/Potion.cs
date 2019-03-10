using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Potion : MonoBehaviour {

    [SerializeField]
    float newPlayerScale = .3f;
    [SerializeField]
    float timer = 30;
    [SerializeField]
    AudioClip sfx;
    [SerializeField]
    float sfxVolume = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HeadCollider"))
        {
            Player.instance.transform.localScale = Vector3.one * newPlayerScale;
            if (sfx)
                AudioSource.PlayClipAtPoint(sfx, transform.position, sfxVolume);
            Player.instance.StartCoroutine(Timer());
            Destroy(gameObject);
        }
    }

    IEnumerator Timer(){
        yield return new WaitForSeconds(timer);
        Player.instance.transform.localScale = Vector3.one;
    }
}
