using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractionManager : MonoBehaviour {

    public static InterractionManager instance;

    /*
    public delegate void acteurInScene();
    protected static event acteurInScene OnActeurInScene;
    */

    public bool actorIsInScene;

    private void Awake()
    {
        instance = this;
       
    }

    public virtual void setChoicesRoom(List<int> choices)
    {

    }

    public virtual void LaunchNextScene()
    {

    }

    public virtual GameObject GetLeaves()
    {
        return null;
    }

    public virtual List<int> GetChoices()
    {
        return null;
    }


}
