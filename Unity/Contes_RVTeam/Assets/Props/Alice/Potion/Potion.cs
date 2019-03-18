using UnityEngine;

[RequireComponent(typeof(Comestible))]
public class Potion : MonoBehaviour {

    [SerializeField]
    float newPlayerSize = .3f;
    [SerializeField]
    float duration = 15;
    float transitionTime = 2.5f;


    private void Awake()
    {
        GetComponent<Comestible>().Consumed += OnConsumed;
        if (!PlayerScaleManager.Instance)
            new GameObject().AddComponent<PlayerScaleManager>();
    }

    public void OnConsumed()
    {
        PlayerScaleManager.Instance.TryScaleDown(newPlayerSize, duration);

        // Create another potion.
        Table.Instance.AddPotion();
    }
}
