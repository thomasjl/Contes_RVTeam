using UnityEngine;

public class Comestible : MonoBehaviour {

    [Header("On first bite")]
    [SerializeField]
    GameObject destroy;
    [SerializeField]
    GameObject activate;

    [Space]
    [SerializeField]
    float delayBetweenBites = .5f;
    [SerializeField]
    AudioClip sfx;
    [SerializeField]
    float volume = 1;
    float lastConsume = 0;
    public bool destroyInTheEnd = true;


    public delegate void ComestibleEventHandler();
    public event ComestibleEventHandler Consumed;

    private void Start()
    {
        if (activate)
            activate.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HeadCollider") && Time.time - lastConsume > delayBetweenBites)
        {
            if (Consumed != null)
                Consumed();
            // Show the new mesh and play a sound.
            if (sfx)
                AudioSource.PlayClipAtPoint(sfx, transform.position, volume);
            if (destroy)
            {
                Destroy(destroy);
                if (activate)
                    activate.SetActive(true);
            }
            else
            {
                if (destroyInTheEnd)
                    Destroy(gameObject);
                else
                    activate.SetActive(false);
            }
            lastConsume = Time.time;
        }
    }
}
