using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Chaperon : MonoBehaviour {

    Transform PlayerHead { get { return Player.instance.headCollider.transform; } }
    [SerializeField]
    GameObject grabbable;
    Cloth grabbableCloth;
    [SerializeField]
    GameObject wearable;
    Cloth wearableCloth;
    [SerializeField]
    Transform fakeGround;

    [SerializeField]
    float rotationStrength = .05f;
    [SerializeField]
    float clothHeight = 2;
    bool fakeGroundMoves;

    private void Awake()
    {
        // Get cloths.
        grabbableCloth = grabbable.GetComponentInChildren<Cloth>(true);
        wearableCloth = wearable.GetComponentInChildren<Cloth>(true);
    }

    private void Start()
    {
        Equip();
    }

    public void Equip()
    {
        // Show the correct cloth, snap to the player's head and start to follow the head.
        grabbable.SetActive(false);
        wearable.SetActive(true);
        transform.position = PlayerHead.position;
        transform.rotation = Quaternion.identity;
        StartCoroutine(FollowHead());
        // Move the ground upwards slowly to prevent the cloth from clipping.
        StartGroundCorrection();
    }

    IEnumerator FollowHead()
    {
        // Reset the cloth.
        wearableCloth.enabled = false;
        yield return null;
        wearableCloth.enabled = true;
        while (true)
        {
            // Set cloth transform.
            transform.position = PlayerHead.position;
            Vector3 targetRotation = transform.eulerAngles.SetY(PlayerHead.eulerAngles.y);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), rotationStrength);
            // Set fake ground transform.
            if (!fakeGroundMoves)
                fakeGround.position = PlayerHead.position.SetY(Player.instance.transform.position.y);
            yield return null;
        }
    }

    void StartGroundCorrection()
    {
        // Move the ground upwards slowly.
        fakeGroundMoves = true;
        this.MakeProgressionAnim(1, delegate (float progression)
        {
            fakeGround.position = PlayerHead.position.SetY(Player.instance.transform.position.y - clothHeight * (1 - progression));
        }, delegate
        {
            fakeGroundMoves = false;
        });
    }
}
