using UnityEngine;

public class Table : MonoBehaviour {

    [SerializeField]
    Transform tableObjects;
    [SerializeField]
    Potion potionTemplate;
    [SerializeField]
    Transform potionSpawn;
    public bool AddedAPotionOnce{ get; private set; }


    public static Table Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddPotion()
    {
        AddedAPotionOnce = true;
        tableObjects.DestroyChidren();
        Instantiate(potionTemplate, potionSpawn);
    }
}
