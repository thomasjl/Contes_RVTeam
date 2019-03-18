using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GoodMushroom : Mushroom {

    [SerializeField]
    float newPlayerSize = 3;

    public static List<GoodMushroom> mushrooms = new List<GoodMushroom>();

<<<<<<< HEAD
    Vector3 startPosition;
    Quaternion startRotation;
=======
>>>>>>> 7f2ed639bcef553ee88eace6d1728b9f247cbdef

    protected override void Awake()
    {
        base.Awake();
        mushrooms.Add(this);
<<<<<<< HEAD
        startPosition = transform.position;
        startRotation = transform.rotation;
        // Create player scale manager.
        if (!PlayerScaleManager.Instance)
            new GameObject().AddComponent<PlayerScaleManager>();
=======
>>>>>>> 7f2ed639bcef553ee88eace6d1728b9f247cbdef
    }

    protected override void OnConsumed()
    {
<<<<<<< HEAD
        PlayerScaleManager.Instance.TryScaleDown(newPlayerSize, duration);

=======
        Vector3 startPosition = Player.instance.trackingOriginTransform.position;
        Vector3 targetPosition = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * sizeFactor)).SetY(0);
        Player.instance.ProgressionAnim(2, delegate (float progression)
        {
            // Animate in.
            Player.instance.transform.position = Vector3.Lerp(startPosition, targetPosition, progression);
            Player.instance.transform.localScale = Mathf.Lerp(1, sizeFactor, progression) * Vector3.one;
        }, delegate
        {
            Lanterne.instance.PlayColorAnim(duration, Color.white);
            Player.instance.Timer(duration, delegate
            {
                Player.instance.ProgressionAnim(2, delegate (float progression)
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
>>>>>>> 7f2ed639bcef553ee88eace6d1728b9f247cbdef
        // Create another mushroom.
        if (mushrooms.Count <= 1)
        {
            mushrooms.Remove(this);
            GameObject newMush = Instantiate(gameObject);
<<<<<<< HEAD
            newMush.transform.position = startPosition;
            newMush.transform.rotation = startRotation;
=======
            newMush.transform.position = Vector3.up * .3f;
>>>>>>> 7f2ed639bcef553ee88eace6d1728b9f247cbdef
            newMush.SetActive(true);
        }
    }
}
