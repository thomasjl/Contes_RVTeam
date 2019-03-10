using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Comestible))]
public class BadMushroom : MonoBehaviour {

    [SerializeField]
    float minHueChange = 30;
    [SerializeField]
    float duration = 10;

    public void Awake()
    {
        GetComponent<Comestible>().Consumed += OnConsumed;
    }

    private void OnConsumed()
    {
        if (BadMushroomsManager.instance)
            switch (BadMushroomsManager.instance.action)
            {
                case BadMushroomsManager.Action.Colors:
                    Colors();
                    break;
                case BadMushroomsManager.Action.Levitation:
                    Levitation();
                    break;
                default:
                    break;
            }
        else
            Colors();
    }

    void Levitation()
    {

    }

    void Colors()
    {
        // Use color grading to make a color effect.
        float targetHueShift = Random.value > 0.5f ? Random.Range(-180, -minHueChange) : Random.Range(minHueChange, 180);
        PlayerPostProcess.Instance.ProgressionAnim(3, delegate (float progression)
        {
            PlayerPostProcess.Instance.HueShift = Mathf.Lerp(PlayerPostProcess.Instance.HueShift, targetHueShift, progression);
        }, delegate
        {
            PlayerPostProcess.Instance.StartCoroutine(Timer());
        });
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        PlayerPostProcess.Instance.HueShift = 0;
    }

    private void Reset()
    {
        GetComponent<Comestible>().destroyInTheEnd = false;
    }
}