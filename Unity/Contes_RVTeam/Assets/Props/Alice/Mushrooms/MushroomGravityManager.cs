using UnityEngine;

public class MushroomGravityManager : MonoBehaviour {

    Vector3 startGrav;
    bool playing = false;

    public static MushroomGravityManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        startGrav = Physics.gravity;
        gameObject.name = GetType().ToString();
    }

    public void PlayGravityAnim(float duration)
    {
        if (playing)
            return;
        playing = true;
        this.ProgressionAnim(duration, delegate (float progression)
        {
            Physics.gravity = Vector3.Lerp(Vector3.up, -Vector3.up, progression);
        }, delegate
        {
            Physics.gravity = Vector3.zero;
            this.Timer(2, delegate
            {
                Physics.gravity = startGrav;
                playing = false;
            });
        });
    }
}
