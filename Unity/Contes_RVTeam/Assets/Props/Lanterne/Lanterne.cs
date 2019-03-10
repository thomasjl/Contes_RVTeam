using UnityEngine;

public class Lanterne : MonoBehaviour {

    LanterneFlame flame;

    public static Lanterne instance;
    private void Awake()
    {
        instance = this;
        flame = GetComponentInChildren<LanterneFlame>();
    }

    public void SetFlameColor(Color color)
    {
        flame.SetColor(color);
    }
}
