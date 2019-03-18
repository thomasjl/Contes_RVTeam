using UnityEngine;
using System.Collections;

public class PoisonApple : MonoBehaviour {

    [SerializeField]
    float minLanterneDistance = 1f;
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

    private Color startColor;

    public Color off;
    public Color on;


    private void Awake()
    {
        instance = this;
        GetComponent<Comestible>().Consumed += OnConsumed;
        remedPresent = false;
        consumed = false;
    }


    private void Start()
    {
        startColor = deathSymbol.material.color;
        deathSymbol.material.SetColor("_EmissionColor", off);

        deathSymbol.material.EnableKeyword("_EMISSION");
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

        while (Time.time - startime < 11)
        {
            if (FioleRemede.Consumed)
            {
                // Stop the poison and play victory if we use the fiole.
                PlayerPostProcess.Instance.PlayRemede(3);
                yield break;
            }
            yield return null;
        }

        // Kill the player.
        this.ProgressionAnim(2, delegate (float progression)
        {
            PlayerPostProcess.Instance.BlinkTime = Mathf.Lerp(0, blinkValue, progression);
        }, delegate
        {
            InterractionManagerBN.instance.LaunchBadOutro();
        });
    }
    
    private void Update()
    {
        // Update the alpha of the death symbol.
        float distance = Vector3.Distance(transform.position, Lanterne.instance.FlamePosition);
        if (distance < minLanterneDistance)
        {
            SetAlpha(Mathf.Lerp(1, 0, distance / minLanterneDistance));
        }
        else
        {
            deathSymbol.material.SetColor("_EmissionColor", off);
        }

    }

    void SetAlpha(float alpha)
    {
        deathSymbol.material.SetColor("_EmissionColor", Color.Lerp(off, on, alpha));
    }
}
