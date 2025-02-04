﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManagerAlice : InterractionManager
{
    public AudioSource ambianceSound;
    public string nextScene = "Attente2";
    [SerializeField]
    GameObject footprintsToDoor;

    [SerializeField]
    Color newColor = new Color(0, 0.4251068f, 1f);

    private List<int> choicesAlice;

    [Header("Arrows")]
    [SerializeField]
    GameObject arrowPainting;
    [SerializeField]
    GameObject  arrowDoor;


    private void Start()
    {
        //change lanternFlame color
        LanterneFlame.instance.SetColor(newColor);

        SetChoicesRoom(ConteurManager.instance.Choices);

        footprintsToDoor.SetActive(false);
        Crown.Instance.Equipped += ShowDoorHint;
        Scepter.Instance.Grabbed += ShowDoorHint;

        // Setting up the arrows.
        arrowPainting.SetActive(true);
        arrowDoor.SetActive(false);
    }


    public override void SetChoicesRoom(List<int> choices)
    {
        choicesAlice = choices;
        Debug.Log("choices0 : " + choices[0]);

        if (choices[0] == 2)
        {
            //scepter appear
            Crown.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(false);
            Scepter.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(true);

            SoldatDeCarte.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (choices[0] == 4)
        {
            //couronne appear
            Crown.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(true);
            Scepter.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(false);
        }
        else
        {
            Crown.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(false);
            Scepter.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(true);

            SoldatDeCarte.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (choices[1] == 6)
        {
            //time is altered
            BadMushroom.action = BadMushroom.Action.Levitation;
        }
        else if (choices[1] == 8)
        {
            //color change
            BadMushroom.action = BadMushroom.Action.Colors;
        }
        else
        {
            BadMushroom.action = BadMushroom.Action.Levitation;
        }

        if (choices[2] == 10)
        {
            //panneaux, useless
            Signs.Instance.SetChoicesSigns(Signs.ChoicesSigns.choice1);

        }
        else if (choices[2] == 12)
        {
            //panneaxu usefull

            Signs.Instance.SetChoicesSigns(Signs.ChoicesSigns.choice2);
        }
        else
        {
            Signs.Instance.SetChoicesSigns(Signs.ChoicesSigns.choice1);
        }
    }

    void ShowDoorHint()
    {
        footprintsToDoor.SetActive(true);
        arrowDoor.SetActive(true);
        arrowPainting.SetActive(false);
    }

    public override void LaunchNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate
        {
            this.Timer(2, delegate
            {
                Destroy(Crown.Instance.gameObject);
                SceneManager.LoadSceneAsync(nextScene);
            });
        });
    }
}
