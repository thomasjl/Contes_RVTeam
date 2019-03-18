using UnityEngine;
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

    private bool consumed;


    private void Awake()
    {
        instance = this;
        GetComponent<Comestible>().Consumed += OnConsumed;
        SetAlpha(0);
        remedPresent = false;
        consumed = false;
    }

    void OnConsumed()
    {
        if (!consumed)
        {
            consumed = true;

            // Make the poison effect
            PlayerPostProcess.Instance.PlayPoison(8, colorFilter);

            //Wait for remede
            waitingForRemede = WaitForRemede();
            StartCoroutine(waitingForRemede);
        }
    }


    IEnumerator waitingForRemede;
    IEnumerator WaitForRemede()
    {
        //spawn remede
        FioleRemede.instance.SpawnFiole();

        float startime = Time.time;

        while (Time.time - startime < 11f)
            yield return null;


        // Kill the player.
        this.ProgressionAnim(2, delegate (float progression)
        {
            PlayerPostProcess.Instance.BlinkTime = Mathf.Lerp(0, blinkValue, progression);
        }, delegate
        {
            InterractionManagerBN.instance.LaunchBadOutro();
        });
    }

    public void RemedeFound()
    {
        StopCoroutine(waitingForRemede);
        PlayerPostProcess.Instance.PlayRemede(3);
    }

    private void Update()
    {
        // Update the alpha of the death symbol.
        float distance = Vector3.Distance(transform.position, Lanterne.instance.FlamePosition);
        if (distance < minLanterneDistance)
            SetAlpha(Mathf.Lerp(1, 0, distance / minLanterneDistance));
    }

    void SetAlpha(float alpha)
    {
        deathSymbol.material.SetColor("_Color", new Color(1, 1, 1, alpha));
    }
}
