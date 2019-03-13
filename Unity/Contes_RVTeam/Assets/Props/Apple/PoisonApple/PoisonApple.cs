using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    SceneAsset sceneToLoad;


    private void Awake()
    {
        GetComponent<Comestible>().Consumed += OnConsumed;
        SetAlpha(0);
    }

    void Start()
    {
        this.Timer(2, delegate
        {
            OnConsumed();
        });
    }

    void OnConsumed()
    {
        // Make the poison effect and load the next scene.
        PlayerPostProcess.Instance.PlayPoison(3, colorFilter);
        this.ProgressionAnim(2, delegate (float progression) { PlayerPostProcess.Instance.Blink = progression * blinkValue; });
        this.Timer(5, delegate { SceneManager.LoadSceneAsync(sceneToLoad.name); });
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
