using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Comestible))]
public class Potion : MonoBehaviour {

    [SerializeField]
    float sizeFactor = .3f;
    [SerializeField]
    float timer = 10;

    public delegate void PotionEH();
    public static event PotionEH ScaledNormal;
    public static event PotionEH ScaledDown;

    private void Awake()
    {
        GetComponent<Comestible>().Consumed += PlayEffect;
    }
    
    
    public void PlayEffect()
    {
        // Play a timed scale effect on the player.
        // Player.instance.transform.position = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * sizeFactor)).SetY(0);
        Player.instance.transform.position = SmallDoor.instance.Spawnpoint.position;

        Player.instance.transform.localScale = Vector3.one * sizeFactor;
        // Call event.
        if (ScaledDown != null)
            ScaledDown();
        Player.instance.Timer(timer, delegate
        {
            Player.instance.transform.localScale = Vector3.one;
            Player.instance.transform.position = Vector3.zero;
            // Call event.
            if (ScaledNormal != null)
                ScaledNormal();
        });
        Lanterne.instance.PlayColorAnim(timer, Color.white);


        StartCoroutine(SpawnNewPotion());
    }

    IEnumerator SpawnNewPotion()
    {
        yield return new WaitForSeconds(10);
        Table.Instance.AddPotion();
    }
}
