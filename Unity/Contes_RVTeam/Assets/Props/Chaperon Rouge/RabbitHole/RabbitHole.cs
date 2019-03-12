using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitHole : MonoBehaviour {

    [SerializeField]
    SceneAsset nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HeadCollider") && (!Thorns.instance || Thorns.instance.Cleared))
            ToNextScene();  
    }

    void ToNextScene()
    {
        this.ProgressionAnim(3, delegate (float progression)
        {
            PlayerPostProcess.Instance.VignetteStrength = PlayerPostProcess.Instance.StartVignetteStrength + progression * 2;
        }, delegate
        {
            SceneManager.LoadSceneAsync(nextScene.name);
        });
    }
}
