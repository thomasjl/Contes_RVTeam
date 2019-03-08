using UnityEngine;

public class ChaperonOnTreePos : MonoBehaviour {

    public static ChaperonOnTreePos instance;
    private void Awake()
    {
        instance = this;
    }
}
