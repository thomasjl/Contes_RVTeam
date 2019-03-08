using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

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
    List<Thorn> thorns = new List<Thorn>();


    public static Thorns instance;
    private void Awake()
    {
        instance = this;
        thorns = GetComponentsInChildren<Thorn>().ToList();
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
