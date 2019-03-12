using UnityEngine;
using Valve.VR.InteractionSystem;

public class AppleFacePlayer : MonoBehaviour {
    Comestible comestible;
    private void Awake()
    {
        comestible = GetComponentInParent<Comestible>();
        comestible.Consumed += FacePlayer;
    }

    private void FacePlayer()
    {
        transform.rotation = Quaternion.LookRotation(transform.parent.up, Player.instance.headCollider.transform.position - transform.position);
    }
}
