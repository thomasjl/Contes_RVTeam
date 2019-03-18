using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GoodMushroom : Mushroom {

    [SerializeField]
    float sizeFactor = 3;

    public static List<GoodMushroom> mushrooms = new List<GoodMushroom>();
    float transitionTime = 2.5f;

    Vector3 startPosition;
    Quaternion startRotation;

    public delegate void EventHandler();
    public static event EventHandler ScaledNormal;
    public static event EventHandler ScaledUp;
    public static bool scaled;

    protected override void Awake()
    {
        base.Awake();
        mushrooms.Add(this);
        startPosition = transform.position;
        startRotation = transform.rotation;
    }


    protected override void OnConsumed()
    {
        if (!scaled && !Potion.scaled)
            AnimatePlayerScale();

        // Create another mushroom.
        if (mushrooms.Count <= 1)
        {
            mushrooms.Remove(this);
            GameObject newMush = Instantiate(gameObject);
            newMush.transform.position = startPosition;
            newMush.transform.rotation = startRotation;
            newMush.SetActive(true);
        }
    }

    void AnimatePlayerScale()
    {
        scaled = true;
        Vector3 playerStartPosition = Player.instance.trackingOriginTransform.position;
        Vector3 targetPosition = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * sizeFactor)).SetY(0);
        Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
        {
            // Animate in.
            Player.instance.transform.position = Vector3.Lerp(playerStartPosition, targetPosition, progression);
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
                    Player.instance.transform.position = Vector3.Lerp(targetPosition, playerStartPosition, progression);

                }, delegate
                {
                    Table.Instance.AddPotion();
                    scaled = false;
                    // Call event.
                    if (ScaledNormal != null)
                        ScaledNormal();
                });
            });
            // Call event.
            if (ScaledUp != null)
                ScaledUp();
        });
    }
}
