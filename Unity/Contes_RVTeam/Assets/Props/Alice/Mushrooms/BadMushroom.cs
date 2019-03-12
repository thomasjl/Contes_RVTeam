using UnityEngine;

[RequireComponent(typeof(Comestible))]
public class BadMushroom : Mushroom {

    [SerializeField]
    float minHueChange = 30;


    protected override void OnConsumed()
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
            PlayerPostProcess.Instance.Timer(duration, delegate
            {
                PlayerPostProcess.Instance.ProgressionAnim(5, delegate (float progression)
                {
                    PlayerPostProcess.Instance.HueShift = Mathf.Lerp(PlayerPostProcess.Instance.HueShift, PlayerPostProcess.Instance.StartHueshift, progression);
                });
            });
        });
    }
}