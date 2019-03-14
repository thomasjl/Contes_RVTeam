﻿using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitHole : MonoBehaviour {

    [SerializeField]
    string nextScene= "Attente1";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HeadCollider") && (!Thorns.instance || Thorns.instance.Cleared))
            ToNextScene();
    }

    void ToNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(nextScene); }); });
    }
}
