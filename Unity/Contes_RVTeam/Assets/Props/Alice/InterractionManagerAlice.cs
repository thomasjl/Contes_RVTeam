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
    }

    public override void setChoicesRoom(List<int> choices)
    {
        choicesAlice = choices;

        if (choices[0] == 2)
        {
            //scepter appear
        }
        else if(choices[0] == 4)
        {
            //couronne appear
        }
        else
        {

        }

        if (choices[1] == 6)
        {
            //time is altered
        }
        else if (choices[1] == 8)
        {
            //color change
        }
        else
        {

        }

        if (choices[2] == 10)
        {
            //panneaux, useless
        }
        else if (choices[2] == 12)
        {
            //panneaxu usefull
        }
        else
        {

        }


    }
}
