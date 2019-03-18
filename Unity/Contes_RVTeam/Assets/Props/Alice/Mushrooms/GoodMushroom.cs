using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GoodMushroom : Mushroom {

    [SerializeField]
    float sizeFactor = 3;

    public static List<GoodMushroom> mushrooms = new List<GoodMushroom>();
    float transitionTime = .5f;

    protected override void Awake()
    {
        base.Awake();
        mushrooms.Add(this);
    }


    protected override void OnConsumed()
    {
        Vector3 startPosition = Player.instance.trackingOriginTransform.position;
        Vector3 targetPosition = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * sizeFactor)).SetY(0);
        Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
        {
            // Animate in.
            Player.instance.transform.position = Vector3.Lerp(startPosition, targetPosition, progression);
            Player.instance.transform.localScale = Mathf.Lerp(1, sizeFactor, progression) * Vector3.one;
        }, delegate
        {
            Lanterne.instance.PlayColorAnim(duration, Color.white);
            Player.instance.Timer(duration, delegate
            {
                Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
                {
                    // Animate out.
                    Player.instance.transform.localScale = Mathf.Lerp(sizeFactor, 1, progression) * Vector3.one;
                    Player.instance.headCollider.transform.localScale = Vector3.one / Player.instance.transform.localScale.x;
                    Player.instance.transform.position = Vector3.Lerp(targetPosition, startPosition, progression);

                }, delegate
                {/*
                    if (Crown.Instance.IsEquipped || Scepter.Instance.IsEquipped)
                        CheshireCat.Instance.Spawn();*/
                    Table.Instance.AddPotion();
                });
            });
        });
        // Create another mushroom.
        if (mushrooms.Count <= 1)
        {
            mushrooms.Remove(this);
            GameObject newMush = Instantiate(gameObject);
            newMush.transform.position = Vector3.up * .3f;
            newMush.SetActive(true);
        }
    }
}
