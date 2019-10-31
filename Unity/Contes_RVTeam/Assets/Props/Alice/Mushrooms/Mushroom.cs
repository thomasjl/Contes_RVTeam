using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField]
    protected float duration = 10;
    Comestible comestible;
    public event Comestible.ComestibleEventHandler Consumed {
        add { comestible.Consumed += value; }
        remove { comestible.Consumed -= value; }
    }

    protected virtual void Awake()
    {
        comestible = GetComponent<Comestible>();
        comestible.Consumed += OnConsumed;
    }

    protected virtual void OnConsumed()
    {
    }
}
