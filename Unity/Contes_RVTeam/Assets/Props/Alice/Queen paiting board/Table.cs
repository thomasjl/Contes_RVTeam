using UnityEngine;

public class Table : MonoBehaviour {

    [SerializeField]
    Transform tableObjects;
    [SerializeField]
    Potion potionTemplate;
    [SerializeField]
    Transform potionSpawn;

    public static Table Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddPotion()
    {
        tableObjects.DestroyChidren();
        Instantiate(potionTemplate, potionSpawn);
    }
}
