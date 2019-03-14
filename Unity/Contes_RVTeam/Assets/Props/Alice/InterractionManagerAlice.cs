using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractionManagerAlice : InterractionManager
{
    public AudioSource ambianceSound;

    [SerializeField]
    Color newColor = new Color(0, 0.4251068f, 1f);



    private void Start()
    {
        //change lanternFlame color
        LanterneFlame.instance.SetColor(newColor);
    }

}
