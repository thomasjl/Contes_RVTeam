using UnityEngine;

public class BadMushroomsManager : MonoBehaviour {

    public enum Action { Colors, Levitation }
    public Action action;

    public static BadMushroomsManager instance;

    private void Awake()
    {
        instance = this;
    }
}
