using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;

public class PoisonApple : MonoBehaviour {

    [SerializeField]
    float minLanterneDistance = .4f;
    [SerializeField]
    Renderer deathSymbol;

    [Space]
    [SerializeField]
    Color colorFilter = new Color(.5f, 1, .5f);
    [SerializeField]
    float blinkValue = .5f;

    public static PoisonApple instance;

    public bool remedPresent;


    private void Awake()
    {
        instance = this;
        GetComponent<Comestible>().Consumed += OnConsumed;
        SetAlpha(0);
        remedPresent = false;
    }

    
    void OnConsumed()
    {        
        // Make the poison effect
        PlayerPostProcess.Instance.PlayPoison(8, colorFilter);        

        //Wait for remede
        StartCoroutine(WaitForRemede());
        //this.Timer(5, delegate { InterractionManagerBN.instance.LaunchBadOutro(); });
    }

    IEnumerator WaitForRemede()
    {
        int timer = 9;
        if(remedPresent)
        {
            while (timer > 0)
            {
                yield return new WaitForSeconds(1);
                timer--;
            }
        }
       

        //remede not found
        this.ProgressionAnim(2, delegate (float progression) { PlayerPostProcess.Instance.BlinkTime = Mathf.Lerp(0, blinkValue, progression); });
        InterractionManagerBN.instance.LaunchBadOutro();

    }

    public void RemedeFound()
    {
        //found Remede
        StopAllCoroutines();
        
        //reset normal vignette
        PlayerPostProcess.Instance.PlayRemede(3);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Lanterne.instance.FlamePosition);
        if (distance < minLanterneDistance)
            SetAlpha(Mathf.Lerp(1, 0, distance / minLanterneDistance));
    }

    void SetAlpha(float alpha)
    {
        deathSymbol.material.SetColor("_Color", new Color(1, 1, 1, alpha));
    }
}
