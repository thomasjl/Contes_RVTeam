using UnityEngine;

public class FioleRemede : MonoBehaviour {

    public static FioleRemede instance;
    public static bool Consumed { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.GetComponent<Comestible>().Consumed += delegate { Consumed = true; };
    }

    public void SpawnFiole()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
