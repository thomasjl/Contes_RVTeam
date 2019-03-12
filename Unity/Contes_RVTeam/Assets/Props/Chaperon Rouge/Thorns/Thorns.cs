using UnityEngine;

public class Thorns : MonoBehaviour {

    public bool Cleared {
        get {
            bool cleared = true;
            foreach (Thorn thorn in thorns)
            {
                if (!thorn.Clear)
                    cleared = false;
            }
            return cleared;
        }
    }
    Thorn[] thorns;


    public static Thorns instance;
    private void Awake()
    {
        instance = this;
        thorns = GetComponentsInChildren<Thorn>();
    }

    public void Flatten()
    {
        foreach (Thorn thorn in thorns)
            thorn.Flatten();
    }

    internal void Unflatten()
    {
        foreach (Thorn thorn in thorns)
            thorn.Unflatten();
    }
}
