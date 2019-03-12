using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornSelect : MonoBehaviour {

    public static ThornSelect instance;

    private void Awake()
    {
        instance = this;
    }

    public void ThornsUsed(int choice)
    {
        if (choice == 0)
        {
            //medaillon
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            //hache
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
