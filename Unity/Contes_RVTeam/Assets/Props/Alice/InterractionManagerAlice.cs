using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractionManagerAlice : InterractionManager
{
    public AudioSource ambianceSound;

    [SerializeField]
    Color newColor = new Color(0, 0.4251068f, 1f);

    private List<int> choicesAlice;

    private void Start()
    {
        //change lanternFlame color
        LanterneFlame.instance.SetColor(newColor);

        setChoicesRoom(new List<int> { 4, 6, 10 });
    }

    public override void setChoicesRoom(List<int> choices)
    {
        choicesAlice = choices;

        if (choices[0] == 2)
        {
            //scepter appear
            Crown.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(true);
            Scepter.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(false);

        }
        else if(choices[0] == 4)
        {
            //couronne appear
            Crown.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(false);
            Scepter.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(true);
        }
        else
        {
            Crown.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(true);
            Scepter.Instance.gameObject.GetComponent<PaintingInteractable>().SetGrabbableInPainting(false);
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
            Signs.instance.SetChoicesSigns(Signs.ChoicesSigns.choice1);

        }
        else if (choices[2] == 12)
        {
            //panneaxu usefull

            Signs.instance.SetChoicesSigns(Signs.ChoicesSigns.choice2);
        }
        else
        {
            Signs.instance.SetChoicesSigns(Signs.ChoicesSigns.choice1);
        }


    }
}
