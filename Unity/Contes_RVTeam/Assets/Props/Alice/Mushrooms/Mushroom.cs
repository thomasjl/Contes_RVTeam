using UnityEngine;

public class Mushroom : MonoBehaviour {

    [SerializeField]
    protected float duration = 10;

    protected virtual void Awake()
    {
        GetComponent<Comestible>().Consumed += OnConsumed;
    }

    protected virtual void OnConsumed()
    {
    }
}
