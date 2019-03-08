using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitHole : MonoBehaviour {

    [SerializeField]
    SceneAsset nextScene;
    private void OnTriggerEnter(Collider other)
    {
        OnScreenPrinter.Print("enter", this);
        if (other.CompareTag("HeadCollider") && Thorns.instance.Cleared)
            ToNextScene();
        else
            OnScreenPrinter.Print("no", this);
    }

    void ToNextScene()
    {
        SceneManager.LoadSceneAsync(nextScene.name);
    }
}
