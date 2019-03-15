using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitHole : MonoBehaviour {

    [SerializeField]
    string nextScene = "Attente1";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HeadCollider") && (!ThornSelect.instance || ThornSelect.instance.Clear))
            ToNextScene();
    }

    void ToNextScene()
    {
        PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(nextScene); }); });
    }
}
