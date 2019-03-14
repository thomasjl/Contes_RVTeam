using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleInteraction : MonoBehaviour {

    [SerializeField]
    GameObject boxApple, poisonApple;
    public enum AppleInterraction { BoxApple, PoisonApple }

    public static AppleInteraction Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void SetPoisonAppleInteraction(AppleInterraction appleInterraction)
    {
        switch (appleInterraction)
        {
            case AppleInterraction.BoxApple:
                poisonApple.SetActive(false);
                boxApple.SetActive(true);
                break;
            case AppleInterraction.PoisonApple:
                boxApple.SetActive(false);
                poisonApple.SetActive(true);
                break;
        }
    }
}
