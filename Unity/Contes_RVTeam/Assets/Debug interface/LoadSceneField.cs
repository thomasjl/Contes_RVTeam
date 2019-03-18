using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneField : MonoBehaviour {

    InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<InputField>();
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(inputField.text);
    }
}
