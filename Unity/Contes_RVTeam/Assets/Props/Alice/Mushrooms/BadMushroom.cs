using UnityEngine;

[RequireComponent(typeof(Comestible))]
public class BadMushroom : Mushroom {

    [SerializeField]
    float minHueChange = 30;

    public enum Action { Colors, Levitation }
    public static Action action;


    protected override void OnConsumed()
    {
        
        switch (action)
        {
            case Action.Colors:
                Colors();
                break;
            case Action.Levitation:
                Levitation();
                break;
            default:
                break;
        }
    }

    void Levitation()
    {
        if (!MushroomGravityManager.Instance)
            new GameObject().AddComponent<MushroomGravityManager>();
        MushroomGravityManager.Instance.PlayGravityAnim(2);
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