using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerAttente1 : InterractionManager
{

    public AudioSource ambianceSound;
    [SerializeField]
    string nextScene = "Room2";

    public GameObject particle;

    private void Start()
    {
        StartCoroutine(AudioController.FadeOut(AudioManager.instance.audioSource, 1));
        StartCoroutine(AudioController.FadeIn(ambianceSound, 1f));

        ConteurManager.instance.LaunchChoices();
        
    }

    private void Update()
    {
        particle.transform.position = Lanterne.instance.gameObject.transform.position;
    }

    public override void LaunchNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(nextScene); }); });
    }
    
}
