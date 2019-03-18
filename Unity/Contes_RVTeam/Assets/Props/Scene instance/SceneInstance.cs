using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInstance : MonoBehaviour {

    private static SceneInstance instance;

    private List<string> listScene=new List<string> {"Intro","Room1","Attente1","Room2","Attente2","Room3","GoodOutro"};

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

   

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Return))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            int currentId = listScene.IndexOf(currentScene);
            Debug.Log("currentId " + currentId);
            if(currentId< listScene.Count)
            {
                string sceneToLaunch =listScene[currentId+1];
                Debug.Log("scenetoLaunch "+ sceneToLaunch);
                PlayerPostProcess.Instance.PlayBlinkFadeOut(1, delegate { this.Timer(2, delegate { SceneManager.LoadSceneAsync(sceneToLaunch); }); });

            }
        }
    }
}
