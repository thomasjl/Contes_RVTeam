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
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(goodOutro); }); });

    }



    private void Start()
    {
        setChoicesRoom(new List<int> { 4, 8,12 });
    }

    public override void setChoicesRoom(List<int> choices)
    {
        choicesBN = choices;

        if (choices[0] == 2)
        {
            //table ecrivain
            TableScenery.Instance.SetScenery(TableScenery.SceneryType.Writer);

            //pomme sur table
            AppleInteraction.Instance.SetPoisonAppleInteraction(AppleInteraction.AppleInterraction.PoisonApple);


        }
        else if (choices[0] == 4)
        {
            //table sorciere
            TableScenery.Instance.SetScenery(TableScenery.SceneryType.Witch);

            //pomme dans boite
            AppleInteraction.Instance.SetPoisonAppleInteraction(AppleInteraction.AppleInterraction.BoxApple);

        }
        else
        {
            TableScenery.Instance.SetScenery(TableScenery.SceneryType.Writer);
            AppleInteraction.Instance.SetPoisonAppleInteraction(AppleInteraction.AppleInterraction.PoisonApple);

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
