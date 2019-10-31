using UnityEngine;

public class Arrow : MonoBehaviour
{
    Renderer rend;
    float delay = 10;
    float startTime;


    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime > delay)
            rend.material.color = rend.material.color.SetA(Mathf.Sin(Time.time));
    }
}
