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


    private List<int> choicesBN;

    public override void LaunchGoodOutro()
    {

        SceneManager.LoadSceneAsync(goodOutro);
       // PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { }); });

    }

    private void Start()
    {
        SetChoicesRoom(ConteurManager.instance.choices);
    }

    public override void SetChoicesRoom(List<int> choices)
    {
        choicesBN = choices;

        if (choices[0] == 2)
        {
            TableScenery.Instance.writerScenery.SetActive(true);
            TableScenery.Instance.witchScenery.SetActive(false);

            /*
            //table ecrivain
            TableScenery.Instance.SetScenery(TableScenery.SceneryType.Writer);

            //pomme sur table
            AppleInteraction.Instance.SetPoisonAppleInteraction(AppleInteraction.AppleInterraction.PoisonApple);
            */

        }
        else if (choices[0] == 4)
        {
            TableScenery.Instance.writerScenery.SetActive(false);
            TableScenery.Instance.witchScenery.SetActive(true);
            /*
            //table sorciere
            TableScenery.Instance.SetScenery(TableScenery.SceneryType.Witch);

            //pomme dans boite
            AppleInteraction.Instance.SetPoisonAppleInteraction(AppleInteraction.AppleInterraction.BoxApple);
            */

        }
        else
        {
            /*
            TableScenery.Instance.SetScenery(TableScenery.SceneryType.Writer);
            AppleInteraction.Instance.SetPoisonAppleInteraction(AppleInteraction.AppleInterraction.PoisonApple);
            */
            TableScenery.Instance.writerScenery.SetActive(true);
            TableScenery.Instance.witchScenery.SetActive(false);

        }

        if (choices[1] == 6)
        {
            //reparer le mirroir
            MagicMirrorInteraction.instance.SetMirror(MagicMirrorInteraction.MirrorType.ToRepare);

        }
        else if (choices[1] == 8)
        {
            //Casser le mirroir
            MagicMirrorInteraction.instance.SetMirror(MagicMirrorInteraction.MirrorType.ToBrake);
            Debug.Log("to brake");
        }
        else
        {
            MagicMirrorInteraction.instance.SetMirror(MagicMirrorInteraction.MirrorType.ToRepare);
        }

        if (choices[2] == 10)
        {
            //anti-poison  
            PoisonApple.instance.remedPresent = true;
        }
        else if (choices[2] == 12)
        {
            //pas d'anti-poison
            PoisonApple.instance.remedPresent = false;
        }
        else
        {
            PoisonApple.instance.remedPresent = true;
        }
    }



    public override void LaunchBadOutro()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(badOutro); }); });

    }
}
