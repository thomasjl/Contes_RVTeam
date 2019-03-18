using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GoodMushroom : Mushroom {

    [SerializeField]
    float newPlayerSize = 3;

    public static List<GoodMushroom> mushrooms = new List<GoodMushroom>();
    float transitionTime = 2.5f;

    Vector3 startPosition;
    Quaternion startRotation;

    protected override void Awake()
    {
        base.Awake();
        mushrooms.Add(this);
        startPosition = transform.position;
        startRotation = transform.rotation;
        // Create player scale manager.
        if (!PlayerScaleManager.Instance)
            new GameObject().AddComponent<PlayerScaleManager>();
    }

    protected override void OnConsumed()
    {
        PlayerScaleManager.Instance.TryScaleDown(newPlayerSize, duration);

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
}
