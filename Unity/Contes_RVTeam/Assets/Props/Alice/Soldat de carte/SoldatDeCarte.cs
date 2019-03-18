using UnityEngine;

public class SoldatDeCarte : MonoBehaviour {

    bool triggeredOnce = false;

    public static SoldatDeCarte instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggeredOnce && other.CompareTag("HeadCollider") && Crown.Instance.IsEquipped)
        {
            triggeredOnce = true;
            this.Timer(1, MoveFromDoor);
        }
    }

    void MoveFromDoor()
    {
        GetComponentInChildren<Animator>().SetTrigger("move");
        this.Timer(2, delegate { SmallDoor.instance.Open(); });
    }
}
