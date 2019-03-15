using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterractionManagerRRH : InterractionManager
{

    public GameObject footprintArbre;
    public GameObject footprintPuit;
    public GameObject plumeParticle;
    
    private List<int> choicesRRH;


    public string nextScene = "Attente1";

    private void Start()
    {
        actorIsInScene = false;
        setChoicesRoom(ConteurManager.instance.choices);

    }

    public override void setChoicesRoom(List<int> choices)
    {
        choicesRRH = choices;

        if (choices[0] == 2)
        {
            Chaperon.instance.SetFirstChoice(2);

            footprintArbre.SetActive(true);
            footprintPuit.SetActive(false);
        }
        else if (choices[0] == 4)
        {
            Chaperon.instance.SetFirstChoice(4);
            footprintArbre.SetActive(false);
            footprintPuit.SetActive(true);
        }
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
