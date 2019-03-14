using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerBadOutro : InterractionManager
{

    [SerializeField]
    string credit = "Credit";

    public void GoCredit()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(credit); }); });

    }

}
