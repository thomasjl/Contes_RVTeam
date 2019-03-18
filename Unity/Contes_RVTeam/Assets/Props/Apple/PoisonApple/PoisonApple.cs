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

    private bool hasEatenApple;

    private IEnumerator coroutineTmp;

    private void Awake()
    {
        instance = this;
        GetComponent<Comestible>().Consumed += OnConsumed;
        SetAlpha(0);
        remedPresent = false;
        hasEatenApple = false;

        Debug.Log("poison apple"+gameObject.name);
    }



    void OnConsumed()
    {        
        if(!hasEatenApple)
        {
            hasEatenApple = true;

            Debug.Log("apple poison");

            // Make the poison effect
            PlayerPostProcess.Instance.PlayPoison(8, colorFilter);

            //Wait for remede
            coroutineTmp = WaitForRemede();
            StartCoroutine(coroutineTmp);

        }
    }

  
    
    IEnumerator WaitForRemede()
    {

        Debug.Log("start coroutine");
        //spawn remede
        FioleRemede.instance.SpawnFiole();

        float startime = Time.time;
       
        while (Time.time- startime < 11f)
        {
            yield return null;
            Debug.Log("tourne");
        }       

       
        Debug.Log("go to Outro");
        //remede not found
        this.ProgressionAnim(2, delegate (float progression) { PlayerPostProcess.Instance.BlinkTime = Mathf.Lerp(0, blinkValue, progression); }, delegate { InterractionManagerBN.instance.LaunchBadOutro(); });
             
    }
    

    public void RemedeFound()
    {
       
        Debug.Log("remde found");
        StopCoroutine(coroutineTmp);

        //found Remede
        
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
