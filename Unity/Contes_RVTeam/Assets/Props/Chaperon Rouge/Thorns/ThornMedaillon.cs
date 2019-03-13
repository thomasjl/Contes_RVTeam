using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMedaillon : MonoBehaviour {

    public bool Clear { get; private set; }
    [SerializeField]
    AudioClip flatten;
    [SerializeField]
    AudioClip unflatten;
    public bool flattening = false;

    public void Flatten()
    {
        if (!GetComponent<Animator>() || flattening)
            return;
        GetComponent<Animator>().SetTrigger("flatten");
        Debug.Log("flattent");
        AudioSource.PlayClipAtPoint(flatten, transform.position);
        //StartCoroutine(WaitThenSetCleared());
        flattening = true;
    }

    public void Unflatten()
    {
        Debug.Log("unflattent avant");
        if (!GetComponent<Animator>() || !flattening)
            return;
        //StopAllCoroutines();
        Debug.Log("unflattent");

        AudioSource.PlayClipAtPoint(unflatten, transform.position);

        GetComponent<Animator>().SetTrigger("unflatten");
       
        flattening = false;
        Clear = false;
    }

    /*
    IEnumerator WaitThenSetCleared()
    {
        flattening = false;
        yield return new WaitForSeconds(2);
        flattening = true;
        Clear = true;
    }
    */

}
