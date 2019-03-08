using UnityEngine;

public class Lanterne : MonoBehaviour {

    public static Lanterne instance;
    private void Awake()
    {
        instance = this;
    }
}
