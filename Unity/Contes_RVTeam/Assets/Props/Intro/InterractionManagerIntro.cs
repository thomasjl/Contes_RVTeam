using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerIntro : InterractionManager {

    public AudioClip soundR1;
    public string nextScene = "Room1";

    private void Start()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;
        AudioManager.instance.audioSource.clip = soundR1;

        Invoke("PlaySound", 2f);
        Invoke("ToNextScene", 5f);
    }

    void PlaySound()
    {
        StartCoroutine(AudioController.FadeIn(AudioManager.instance.audioSource, 20f));

    }

    void ToNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(nextScene); }); });
    }

}
