using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class InteractionManagerBN : InterractionManager
{
    [SerializeField]
    AudioClip ambianceSound;

    [SerializeField]
    string goodOutro = "GoodOutro";

    [SerializeField]
    string badOutro = "BadOutro";

    private List<int> choicesBN;

    [SerializeField]
    CamOccluder occluderTemplate;

    [Header("Arrows")]
    [SerializeField]
    GameObject arrowElevator;
    [SerializeField]
    GameObject arrowFragment, arrowThrow;


    private void Start()
    {
        SetChoicesRoom(ConteurManager.instance.Choices);

        AudioManager.instance.audioSource.clip = ambianceSound;

        // Setting up the arrows.
        arrowFragment.SetActive(false);
        arrowThrow.SetActive(false);
        Lift.Instance.Moves.AddListener(delegate
        {
            arrowElevator.SetActive(false);
        });
        Lift.Instance.StopsMoving.AddListener(delegate
        {
            if (MagicMirrorInteraction.instance.Type == MagicMirrorInteraction.MirrorType.ToBrake)
                arrowThrow.SetActive(true);
            else
                arrowFragment.SetActive(true);
        });
    }

    
    public override void LaunchGoodOutro()
    {
        if (!CamOccluder.Instance)
            Instantiate(occluderTemplate);
        CamOccluder.Instance.SetCamera(Player.instance.headCollider.GetComponentInParent<Camera>());

        // Fade to white and load the next scene.
        CamOccluder.Instance.FadeIn(5, delegate { SceneManager.LoadSceneAsync(goodOutro); });
    }

    public override void LaunchBadOutro()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(badOutro); }); });
    }

    public override void SetChoicesRoom(List<int> choices)
    {
        choicesBN = choices;

        if (choices[0] == 2)
        {
            TableScenery.Instance.writerScenery.SetActive(true);
            TableScenery.Instance.witchScenery.SetActive(false);
        }
        else if (choices[0] == 4)
        {
            TableScenery.Instance.writerScenery.SetActive(false);
            TableScenery.Instance.witchScenery.SetActive(true);
        }
        else
        {
            TableScenery.Instance.writerScenery.SetActive(true);
            TableScenery.Instance.witchScenery.SetActive(false);
        }

        if (choices[1] == 6)
            //reparer le mirroir
            MagicMirrorInteraction.instance.SetMirror(MagicMirrorInteraction.MirrorType.ToRepare);
        else if (choices[1] == 8)
        {
            //Casser le mirroir
            MagicMirrorInteraction.instance.SetMirror(MagicMirrorInteraction.MirrorType.ToBrake);
            Debug.Log("to brake");
        }
        else
            MagicMirrorInteraction.instance.SetMirror(MagicMirrorInteraction.MirrorType.ToRepare);

        if (choices[2] == 10)
            //anti-poison  
            PoisonApple.instance.remedPresent = true;
        else if (choices[2] == 12)
            //pas d'anti-poison
            FioleRemede.instance.gameObject.HideChildren();
        else
            PoisonApple.instance.remedPresent = true;
    }
}
