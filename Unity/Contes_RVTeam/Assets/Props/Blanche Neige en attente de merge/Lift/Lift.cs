using UnityEngine;
using Valve.VR.InteractionSystem;

public class Lift : MonoBehaviour {

    CircularDrive circularDrive;

    float previousDrive;
    float maxVelocity = .5f;
    float velocity = 0;
    float accelerationSpeed = 1;
    [SerializeField]
    float targetY = 2.5f;

    new AudioSource audio;

    private void Awake()
    {
        circularDrive = GetComponentInChildren<CircularDrive>();
        audio = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        velocity += ((circularDrive.outAngle - previousDrive > 3) ? 1 : -1) * Time.deltaTime * accelerationSpeed;
        velocity = Mathf.Clamp(velocity, 0, maxVelocity);
        previousDrive = circularDrive.outAngle;
        if (velocity > .1f)
        {
            if (!audio.isPlaying)
                audio.Play();
        }
        else
            audio.Pause();

        transform.Translate(Vector3.up * velocity * Time.deltaTime);
        Player.instance.transform.position = Player.instance.transform.position.SetY(transform.position.y);

        if (transform.position.y >= targetY)
            this.enabled = false;
    }
}
