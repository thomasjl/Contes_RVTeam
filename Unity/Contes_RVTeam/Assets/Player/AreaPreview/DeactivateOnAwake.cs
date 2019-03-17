using UnityEngine;

public class DeactivateOnAwake : MonoBehaviour {

    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
