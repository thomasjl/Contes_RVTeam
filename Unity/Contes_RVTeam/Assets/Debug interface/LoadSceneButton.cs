using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour {

    [SerializeField]
    string sceneToLoad;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            if (sceneToLoad != "")
                SceneManager.LoadSceneAsync(sceneToLoad);
        });
    }
}
