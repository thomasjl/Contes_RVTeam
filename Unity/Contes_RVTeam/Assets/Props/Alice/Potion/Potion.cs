using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Comestible))]
public class Potion : MonoBehaviour {

    [SerializeField]
    float newPlayerSize = .3f;
    [SerializeField]
    float timer = 10;
    float transitionTime = 2.5f;

    Vector3 ScaleCompensatedPosition { get { return (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * newPlayerSize)).SetY(0); } }

    public delegate void EventHandler();
    public static event EventHandler ScaledNormal;
    public static event EventHandler ScaledDown;
    public static bool scaled;

    private void Awake()
    {
        GetComponent<Comestible>().Consumed += OnConsumed;
    }

    public void OnConsumed()
    {
        if (!scaled && !GoodMushroom.scaled)
            AnimatePlayerScale();

        // Create another potion.
        Table.Instance.AddPotion();
    }

    void AnimatePlayerScale()
    {
        scaled = true;
        Vector3 startPosition = Player.instance.transform.position;
        Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
        {
            // Animate in.
            Player.instance.transform.position = Vector3.Lerp(startPosition, SmallDoor.instance.Spawnpoint.position, progression);
            Player.instance.transform.localScale = Mathf.Lerp(1, newPlayerSize, progression) * Vector3.one;

        }, delegate
        {
            // Call event.
            if (ScaledDown != null)
                ScaledDown();
            Lanterne.instance.PlayColorAnim(timer, Color.white);
            Player.instance.Timer(timer, delegate
            {
                Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
                {
                    Player.instance.transform.localScale = Mathf.Lerp(newPlayerSize, 1, progression) * Vector3.one;
                    Player.instance.transform.position = Vector3.Lerp(SmallDoor.instance.Spawnpoint.position, startPosition, progression);
                }, delegate
                {
                    scaled = false;
                    // Call event.
                    if (ScaledNormal != null)
                        ScaledNormal();
                });
            });
        });
    }
}
