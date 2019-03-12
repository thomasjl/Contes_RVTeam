using UnityEngine;

public class Thorns : MonoBehaviour {

    ThornHache[] thornsHache;
    ThornMedaillon[] thornsMedaillon;


    public bool Cleared {
        get {
            bool cleared = true;
            if(thornsHache!=null)
            {
                foreach (ThornHache thorn in thornsHache)
                {
                    if (!thorn.Clear)
                        cleared = false;
                }
                return cleared;
            }
            else if(thornsMedaillon!=null)
            {
                foreach (ThornMedaillon thorn in thornsMedaillon)
                {
                    if (!thorn.Clear)
                        cleared = false;
                }
                return cleared;
            }

            return false;
           
        }
    }


    public static Thorns instance;
    private void Awake()
    {
        instance = this;
        thornsHache = GetComponentsInChildren<ThornHache>();
        thornsMedaillon = GetComponentsInChildren<ThornMedaillon>();
    }

   

    

    public void Flatten()
    {
        foreach (ThornMedaillon thornMed in thornsMedaillon)
            thornMed.Flatten();
    }

    internal void Unflatten()
    {
        foreach (ThornMedaillon thornMed in thornsMedaillon)
            thornMed.Unflatten();
    }
}
