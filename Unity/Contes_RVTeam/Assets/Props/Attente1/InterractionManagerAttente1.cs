using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerAttente1 : InterractionManager
{

    public AudioSource ambianceSound;
    [SerializeField]
    string nextScene = "Room2";

    private void Start()
    {
        StartCoroutine(AudioController.FadeIn(ambianceSound, 1f));

        ConteurManager.instance.LaunchChoicesRoom2();
    }

    public override void LaunchNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(nextScene); }); });
    }
    
}
