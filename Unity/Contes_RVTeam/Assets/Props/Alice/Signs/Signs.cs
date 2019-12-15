using UnityEngine;

public class Signs : MonoBehaviour
{
    [SerializeField]
    private GameObject choice1;
    [SerializeField]
    private GameObject choice2;

    public enum ChoicesSigns { choice1, choice2 };

    public static Signs Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }


    public void SetChoicesSigns(ChoicesSigns choicesSigns)
    {
        switch (choicesSigns)
        {
            case ChoicesSigns.choice1:
                choice1.SetActive(true);
                choice2.SetActive(false);
                break;
            case ChoicesSigns.choice2:
                choice1.SetActive(false);
                choice2.SetActive(true);
                break;
            default:
                break;
        }
    }
}
