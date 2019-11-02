using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInstance : MonoBehaviour
{
    public int currentSceneId;
    List<string> listScene = new List<string> { "Intro", "Room1", "Attente1", "Room2", "Attente2", "Room3", "GoodOutro" };

    public static SceneInstance Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
            string currentScene = SceneManager.GetActiveScene().name;
            currentSceneId = listScene.IndexOf(currentScene);
        }
        else
            gameObject.HideChildren();
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Return))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            int currentId = listScene.IndexOf(currentScene);
            Debug.Log("current scene id: " + currentId);
            if (currentId < listScene.Count)
            {
                string sceneToLaunch = listScene[currentId + 1];
                Debug.Log("force loading scene " + sceneToLaunch);
                PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(sceneToLaunch); }); });
            }
        }
    }
}
