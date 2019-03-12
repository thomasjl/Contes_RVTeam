using UnityEngine;

public class SoldatDeCarte : MonoBehaviour {

    bool moved = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!moved && other.CompareTag("HeadCollider") && Crown.Instance.IsEquipped)
            MoveFromDoor();
    }

    void MoveFromDoor()
    {
        GetComponentInChildren<Animator>().SetTrigger("move");
        moved = true;
        this.Timer(2, delegate { SmallDoor.instance.Open(); });
    }
}
