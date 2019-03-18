using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerScaleManager : MonoBehaviour {

    float transitionTime = 2.5f;
    bool scaled;

    public delegate void EventHandler();
    public static event EventHandler ScaledNormal;
    public static event EventHandler ScaledDown;
    public static event EventHandler ScaledUp;

    public static PlayerScaleManager Instance { get; private set; }
    private void Awake()
    {
        gameObject.name = GetType().ToString();
        Instance = this;
    }

    public void TryScaleDown(float newPlayerSize, float duration)
    {
        if (scaled)
            return;
        scaled = true;
        Vector3 startPosition = Player.instance.transform.position;
        Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
        {
            // Scale down the player and move it in front of the small door.
            Player.instance.transform.position = Vector3.Lerp(startPosition, SmallDoor.instance.Spawnpoint.position, progression);
            Player.instance.transform.localScale = Mathf.Lerp(1, newPlayerSize, progression) * Vector3.one;

        }, delegate
        {
            Lanterne.instance.PlayColorAnim(duration, Color.white);

            // Reset the player's scale and position over time after a delay.
            StartCoroutine(WaitThenRescale(duration, delegate
            {
                Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
                {
                    Player.instance.transform.localScale = Mathf.Lerp(newPlayerSize, 1, progression) * Vector3.one;
                    Player.instance.transform.position = Vector3.Lerp(SmallDoor.instance.Spawnpoint.position, startPosition, progression);
                }, delegate
                {
                    scaled = false;
                    // Call event.
                    if (ScaledNormal != null)
                        ScaledNormal();
                });
            }));

            // Call event.
            if (ScaledDown != null)
                ScaledDown();
        });
    }


    public void TryScaleUp(float newPlayerSize, float duration)
    {
        if (scaled)
            return;
        scaled = true;
        Vector3 playerStartPosition = Player.instance.trackingOriginTransform.position;
        Vector3 targetPosition = (Player.instance.headCollider.transform.position - (Player.instance.headCollider.transform.position * newPlayerSize)).SetY(0);
        Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
        {
            // Scale up the Player while maintaining the camera world position.
            Player.instance.transform.position = Vector3.Lerp(playerStartPosition, targetPosition, progression);
            Player.instance.transform.localScale = Mathf.Lerp(1, newPlayerSize, progression) * Vector3.one;

        }, delegate
        {
            Lanterne.instance.PlayColorAnim(duration, Color.white);

            // Reset the player's scale and position over time after a delay.
            StartCoroutine(WaitThenRescale(duration, delegate
            {
                Player.instance.ProgressionAnim(transitionTime, delegate (float progression)
                {
                    Player.instance.transform.localScale = Mathf.Lerp(newPlayerSize, 1, progression) * Vector3.one;
                    Player.instance.headCollider.transform.localScale = Vector3.one / Player.instance.transform.localScale.x;
                    Player.instance.transform.position = Vector3.Lerp(targetPosition, playerStartPosition, progression);

                }, delegate
                {
                    scaled = false;
                    // Call event.
                    if (ScaledNormal != null)
                        ScaledNormal();
                });
            }));

            // Call event.
            if (ScaledUp != null)
                ScaledUp();
        });
    }


    #region Waiting for rescale ----------------------
    bool forceStopWaiting = false;

    IEnumerator WaitThenRescale(float duration, System.Action endAction)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration && !forceStopWaiting)
            yield return null;
        forceStopWaiting = false;
        endAction();
    }

    public void ForceStopWaiting()
    {
        forceStopWaiting = true;
    }
    #endregion --------------------------------------
}
