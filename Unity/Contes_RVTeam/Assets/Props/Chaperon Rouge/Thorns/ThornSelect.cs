using UnityEngine;

public class ThornSelect : MonoBehaviour {

    public static ThornSelect instance;
    public bool Clear {
        get {
            if(MaisonChaperon.instance.givenObject==8)
            {
                foreach (ThornHache thorn in GetComponentsInChildren<ThornHache>())
                {
                    if (!thorn.Clear)
                    {
                        Debug.Log("thorn hache pas clear " + thorn.name);
                        return false;
                    }
                }                   
            }
            else if(MaisonChaperon.instance.givenObject == 6)
            {
                foreach (ThornMedaillon thorn in GetComponentsInChildren<ThornMedaillon>())
                {
                    if (!thorn.Clear)
                    {
                        Debug.Log("thorn medaillon pas clear " + thorn.name);

                        return false;
                    }
                }                    
            }           
           
            return true;
        }
    }


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
