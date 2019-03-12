using UnityEngine;

public class TableScenery : MonoBehaviour {

    [SerializeField]
    GameObject writerScenery, witchScenery;
    public enum SceneryType { Writer, Witch }

    public static TableScenery Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void SetScenery(SceneryType type)
    {
        switch (type)
        {
            case SceneryType.Writer:
                witchScenery.SetActive(false);
                writerScenery.SetActive(true);
                break;
            case SceneryType.Witch:
                writerScenery.SetActive(false);
                witchScenery.SetActive(true);
                break;
        }
    }
}
