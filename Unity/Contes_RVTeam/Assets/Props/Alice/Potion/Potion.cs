using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Comestible))]
public class Potion : MonoBehaviour {

    [SerializeField]
    float newPlayerSize = .3f;
    [SerializeField]
    float timer = 10;

    Vector3 ScaleCompensatedPosition { get { return (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * newPlayerSize)).SetY(0); } }

    public delegate void EventHandler();
    public static event EventHandler ScaledNormal;
    public static event EventHandler ScaledDown;

    private void Awake()
    {
        GetComponent<Comestible>().Consumed += PlayEffect;
    }

    public void PlayEffect()
    {
        Vector3 startPosition = Player.instance.transform.position;
        Player.instance.ProgressionAnim(2, delegate (float progression)
        {
            // Animate in.
            Player.instance.transform.position = Vector3.Lerp(startPosition, SmallDoor.instance.Spawnpoint.position, progression);
            Player.instance.transform.localScale = Mathf.Lerp(1, newPlayerSize, progression) * Vector3.one;

        }, delegate
        {
            // Respawn a bottle.
            Table.Instance.AddPotion();
            // Call event.
            if (ScaledDown != null)
                ScaledDown();
            Lanterne.instance.PlayColorAnim(timer, Color.white);
            Player.instance.Timer(timer, delegate
            {
                Player.instance.ProgressionAnim(2, delegate (float progression)
                {
                    Player.instance.transform.localScale = Mathf.Lerp(newPlayerSize, 1, progression) * Vector3.one;
                    Player.instance.transform.position = Vector3.Lerp(SmallDoor.instance.Spawnpoint.position, startPosition, progression);
                }, delegate
                {
                    // Call event.
                    if (ScaledNormal != null)
                        ScaledNormal();
                });
            });
        });




        StartCoroutine(SpawnNewPotion());
    }

    IEnumerator SpawnNewPotion()
    {
        yield return new WaitForSeconds(10);
        Table.Instance.AddPotion();
    }
}
