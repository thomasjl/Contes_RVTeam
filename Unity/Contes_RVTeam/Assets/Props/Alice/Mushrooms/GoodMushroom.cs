using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GoodMushroom : Mushroom {

    [SerializeField]
    float sizeFactor = 3;

    public static List<GoodMushroom> mushrooms = new List<GoodMushroom>();

    private bool FirstEat;

    protected override void Awake()
    {
        base.Awake();
        mushrooms.Add(this);
    }

    private void Start()
    {
        FirstEat = true;
    }

    protected override void OnConsumed()
    {
        Vector3 startPosition = Player.instance.trackingOriginTransform.position;
        Vector3 targetPosition = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * sizeFactor)).SetY(0);
        Player.instance.ProgressionAnim(2, delegate (float progression)
        {
        // Animate in.
            Player.instance.transform.position = Vector3.Lerp(startPosition, targetPosition, progression);
            Player.instance.transform.localScale = Mathf.Lerp(1, sizeFactor, progression) * Vector3.one;
        }, delegate
        {
            Player.instance.Timer(duration, delegate
            {
                Player.instance.ProgressionAnim(2, delegate (float progression)
                {
                // Animate out.
                    Player.instance.transform.localScale = Vector3.one;
                    Player.instance.transform.position = startPosition;

                }, delegate
             {
                 if (Crown.Instance.IsEquipped || Scepter.Instance.IsEquipped)
                     CheshireCat.Instance.Spawn();
                 Lanterne.instance.PlayColorAnim(duration, Color.white);
                 Table.Instance.AddPotion();
             });

            });
        });
    }

    IEnumerator SpawnFirstPotion()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("passe");
        Table.Instance.AddPotion();
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
