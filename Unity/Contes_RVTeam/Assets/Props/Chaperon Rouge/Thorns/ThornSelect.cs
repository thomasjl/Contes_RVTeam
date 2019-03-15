using UnityEngine;

public class ThornSelect : MonoBehaviour {

    public static ThornSelect instance;
    public bool Clear {
        get {
            foreach (ThornHache thorn in GetComponentsInChildren<ThornHache>())
                if (!thorn.Clear)
                    return false;
            foreach (ThornMedaillon thorn in GetComponentsInChildren<ThornMedaillon>())
                if (!thorn.Clear)
                    return false;
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
