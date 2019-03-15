using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GoodMushroom : Mushroom {

    [SerializeField]
    float sizeFactor = 3;

    public static List<GoodMushroom> mushrooms = new List<GoodMushroom>();

    protected override void Awake()
    {
        base.Awake();
        mushrooms.Add(this);
    }

    protected override void OnConsumed()
    {
        Player.instance.transform.position = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * sizeFactor)).SetY(0);
        Player.instance.transform.localScale = Vector3.one * sizeFactor;
        Player.instance.Timer(duration, delegate
        {
            Player.instance.transform.localScale = Vector3.one;
            Player.instance.transform.position = Vector3.zero;

            if (Crown.Instance.IsEquipped || Scepter.Instance.IsEquipped)
                CheshireCat.Instance.Spawn();
        });
        Lanterne.instance.PlayColorAnim(duration, Color.white);
    }

    private void OnDestroy()
    {
        mushrooms.Remove(this);
#if  UNITY_EDITOR
        if (Application.isPlaying)
#endif
            if (mushrooms.Count < 1)
            {
                GameObject newMush = Instantiate(gameObject);
                newMush.transform.position = Vector3.up * .3f;
                newMush.SetActive(true);
            }
    }
}
