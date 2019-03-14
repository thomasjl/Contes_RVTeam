using UnityEngine;

public class Narration : MonoBehaviour {

    [SerializeField]
    GameObject introEng, introFR, badOutroEng, goodOutroEng, badOutroFr, goodOutroFr;
    GameObject[] narrations;

    public enum NarrationType { introEng, introFR, badOutroEng, goodOutroEng, badOutroFr, goodOutroFr }

    [Space]
    [SerializeField]
    bool playNarrationOnStart = true;
    [SerializeField]
    NarrationType startType;

    public static Narration Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        narrations = new GameObject[] { introEng, introFR, badOutroEng, goodOutroEng, badOutroFr, goodOutroFr };
    }

    private void Start()
    {
        if (playNarrationOnStart)
            PlayNarration(startType);
    }

    /// <summary>
    /// Start a type of narration.
    /// </summary>
    /// <param name="type"></param>
    public void PlayNarration(NarrationType type)
    {
        foreach (GameObject narration in narrations)
            narration.SetActive(false);
        switch (type)
        {
            case NarrationType.introEng:
                introEng.SetActive(true);
                break;
            case NarrationType.introFR:
                introFR.SetActive(true);
                break;
            case NarrationType.badOutroEng:
                badOutroEng.SetActive(true);
                break;
            case NarrationType.goodOutroEng:
                goodOutroEng.SetActive(true);
                break;
            case NarrationType.badOutroFr:
                badOutroFr.SetActive(true);
                break;
            case NarrationType.goodOutroFr:
                goodOutroFr.SetActive(true);
                break;
            default:
                break;
        }
    }
}
