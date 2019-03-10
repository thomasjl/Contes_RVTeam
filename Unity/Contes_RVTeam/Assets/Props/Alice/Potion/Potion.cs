using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Comestible))]
public class Potion : MonoBehaviour {

    [SerializeField]
    float newPlayerScale = .3f;
    [SerializeField]
    float timer = 30;
    [SerializeField]

    private void Awake()
    {
        GetComponent<Comestible>().Consumed += PlayEffect;
    }

    void PlayEffect()
    {
        Player.instance.transform.localScale = Vector3.one * newPlayerScale;
        Player.instance.StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Player.instance.transform.localScale = Vector3.one;
    }
}
