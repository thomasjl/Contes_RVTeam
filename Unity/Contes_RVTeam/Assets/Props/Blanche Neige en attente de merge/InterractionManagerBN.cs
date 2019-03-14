using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerBN : InterractionManager
{
    public AudioSource ambianceSound;

    [SerializeField]
    string goodOutro = "GoodOutro";

    [SerializeField]
    string badOutro = "BadOutro";

    public void GoGoodOutro()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(goodOutro); }); });

    }

    public void GoBadOutro()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(badOutro); }); });

    }
}
