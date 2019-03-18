using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class InterractionManagerAttente2 : InterractionManager
{

    public AudioSource ambianceSound;
    [SerializeField]
    string nextScene = "Room3";

    private void Start()
    {
        Player.instance.StopAllCoroutines();

        StartCoroutine(AudioController.FadeIn(ambianceSound, 1f));

        ConteurManager.instance.LaunchChoices();

        Player.instance.transform.localScale = Vector3.one;
    }

    public override void LaunchNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(nextScene); }); });
    }
}
