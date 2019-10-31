using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManagerRRH : InterractionManager
{

    [SerializeField]
    GameObject plumeParticle;

    private List<int> choicesRRH;

    [SerializeField]
    GameObject footprintsToThorns, footprintsToTreeDwell;


    [SerializeField]
    string nextScene = "Attente1";

    [Header("Arrows")]
    [SerializeField]
    GameObject arrowChaperon;
    [SerializeField]
    GameObject arrowDoor, arrowThorns;


    private void Start()
    {
        actorIsInScene = false;
        if (ConteurManager.instance)
            SetChoicesRoom(ConteurManager.instance.choices);

        // Setup footprints.
        footprintsToThorns.SetActive(false);
        MaisonChaperon.instance.ObjectGiven += delegate
        {
            footprintsToThorns.SetActive(true);
            footprintsToTreeDwell.SetActive(false);
        };

        // Setup arrows.
        arrowChaperon.SetActive(true);
        arrowDoor.SetActive(false);
        arrowThorns.SetActive(false);
        // Got to the arrow of the door when the chaperon is equipped.
        Chaperon.instance.Equipped += delegate
        {
            arrowChaperon.SetActive(false);
            arrowDoor.SetActive(true);
        };
        // Got to the arrow of the thorns when the maison is complete.
        MaisonChaperon.instance.ObjectGiven += delegate
        {
            arrowDoor.SetActive(false);
            arrowThorns.SetActive(true);
        };
    }

    public override void SetChoicesRoom(List<int> choices)
    {
        choicesRRH = choices;

        if (choices[0] == 2)
            Chaperon.instance.SetFirstChoice(2);
        else if (choices[0] == 4)
            Chaperon.instance.SetFirstChoice(4);
        else
        {
            Chaperon.instance.SetFirstChoice(2);
        }

        if (choices[1] == 6)
        {
            MaisonChaperon.instance.SetSecondChoice(6);
        }
        else if (choices[1] == 8)
        {
            MaisonChaperon.instance.SetSecondChoice(8);
        }
        else
        {
            MaisonChaperon.instance.SetSecondChoice(6);
        }

        if (choices[2] == 10)
        {
            MailBox.instance.SetPaperMaterial(MailBox.PaperType.JabberWocky);
        }
        else if (choices[2] == 12)
        {
            MailBox.instance.SetPaperMaterial(MailBox.PaperType.Poison);
        }
        else
        {
            MailBox.instance.SetPaperMaterial(MailBox.PaperType.JabberWocky);
        }

        actorIsInScene = true;
    }

    public override GameObject GetLeaves()
    {
        return plumeParticle;
    }

    public override List<int> GetChoices()
    {
        Debug.Log("passe rrh");
        return choicesRRH;
    }

    public override void LaunchNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(nextScene); }); });
    }

}
