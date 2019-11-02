using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConteurManager : MonoBehaviour
{
    public ComArduino arduino;
    public Slider timer;

    private List<int> defaultValueChoices = new List<int> { 2, 6, 10 };

    public List<int> keyboardChoices1;
    public List<int> keyboardChoices2;
    public List<int> keyboardChoices3;

    public List<int> Choices {
        get {
            return defaultValueChoices;
        }
        set { defaultValueChoices = value; }
    }

    public static ConteurManager instance;

    public int idRoom;


    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        idRoom = 1;

        Debug.Log("conteur " + gameObject.name);

        ComArduino.ConteurChose += FinishChoice;
    }


    private void FinishChoice()
    {
        Debug.Log("getting the choices for the room " + idRoom);
        if (SceneInstance.Instance.currentSceneId == 0)
        {
            Debug.Log("loading choices for scene 1");
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom1));
        }
        else if (SceneInstance.Instance.currentSceneId == 2)
        {
            Debug.Log("loading choices for scene 2");
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom2));
        }
        else if (SceneInstance.Instance.currentSceneId == 4)
        {
            Debug.Log("loading choices for scene 3");
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom3));
        }

        idRoom++;
    }

    void GetChoicesRoom1()
    {
        Choices = new List<int>();

        if (arduino.arduinoEnable)
        {
            Choices = arduino.GetChoices();

        }
        else
        {
            if (ConteurManager.instance.keyboardChoices1 != null && ConteurManager.instance.keyboardChoices1.Count > 0)
            {
                Choices = ConteurManager.instance.keyboardChoices1;
            }
            else
            {
                Choices.Add(4);
                Choices.Add(6);
                Choices.Add(10);
            }
        }

        InterractionManager.instance.LaunchNextScene();
    }



    void GetChoicesRoom2()
    {
        Choices = arduino.GetChoices();

        if (arduino.arduinoEnable)
            Choices = arduino.GetChoices();
        else
        {
            if (instance.keyboardChoices2 != null && instance.keyboardChoices2.Count > 0)
            {
                Debug.Log("nombre de choix : " + instance.keyboardChoices2.Count);
                Choices = instance.keyboardChoices2;
            }
            else
            {
                Choices.Add(2);
                Choices.Add(6);
                Choices.Add(10);
            }
        }

        //change scene
        InterractionManager.instance.LaunchNextScene();
    }

    public void LaunchChoices()
    {
        Debug.Log("starting to listen to the Arduino");
        StartCoroutine(arduino.ListenForMessages());
        if (arduino.arduinoEnable)
            arduino.EraseLed();
        else
        {
            arduino.EraseLedKeyboard();
            arduino.choiceDone = false;
        }
    }

    void GetChoicesRoom3()
    {
        Choices = arduino.GetChoices();

        if (arduino.arduinoEnable)
            Choices = arduino.GetChoices();
        else
        {
            if (instance.keyboardChoices3 != null && instance.keyboardChoices3.Count > 0)
                Choices = instance.keyboardChoices3;
            else
            {
                Choices.Add(2);
                Choices.Add(6);
                Choices.Add(10);
            }
        }

        //change scene
        InterractionManager.instance.LaunchNextScene();
    }


    IEnumerator LaunchTimer(float timerDuration, System.Action endAction)
    {
        timer.gameObject.SetActive(true);
        timer.value = 1f;
        float startTimer = timerDuration;

        while (timerDuration > 0)
        {
            timerDuration -= Time.deltaTime;
            float ratio = timerDuration / startTimer;

            timer.value = ratio;

            yield return null;
        }

        timer.gameObject.SetActive(false);

        if (arduino.arduinoEnable)
            arduino.DisableLed();

        endAction.Invoke();
    }
}
