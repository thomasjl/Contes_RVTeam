using UnityEngine;
using Valve.VR.InteractionSystem;

public class GoodMushroom : Mushroom {

    [SerializeField]
    float sizeFactor = 3;

    protected override void OnConsumed()
    {
        Player.instance.transform.position = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * sizeFactor)).SetY(0);
        Player.instance.transform.localScale = Vector3.one * sizeFactor;
        Player.instance.Timer(duration, delegate
        {
            Player.instance.transform.localScale = Vector3.one;
            Player.instance.transform.position = Vector3.zero;
        });
        Lanterne.instance.PlayColorAnim(duration, Color.white);
    }
}
