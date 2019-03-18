using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerGoodOutro : InterractionManager
{

    [SerializeField]
    string credit = "Credit";

    private void Start()
    {
        Invoke("GoCredit",13f);
        Camera.main.backgroundColor = Color.white;

    }

    public void GoCredit()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(credit); }); });

    }
}
