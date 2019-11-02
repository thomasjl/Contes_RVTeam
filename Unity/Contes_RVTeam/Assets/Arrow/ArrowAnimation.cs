using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    Renderer rend;
    float wakeUpDelay = 10;


    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        rend.material.color = rend.material.color.SetA(0);
        this.Timer(wakeUpDelay, AnimationLoop);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


    void AnimationLoop()
    {
        this.ProgressionAnim(2, delegate (float progression)
        {
            rend.material.color = rend.material.color.SetA(Mathf.Lerp(.2f, 1, AniMath.Bell(progression)));
        }, AnimationLoop);
    }
}
