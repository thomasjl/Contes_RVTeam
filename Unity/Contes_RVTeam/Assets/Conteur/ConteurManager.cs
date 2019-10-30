using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConteurManager : MonoBehaviour {

    public ComArduino arduino;
    public Slider timer;

    private List<int> defaultValueChoices = new List<int> { 2,6, 10 };

    public List<int> keyboardChoices1;
    public List<int> keyboardChoices2;
    public List<int> keyboardChoices3;


    public List<int> choices
    {
        get
        {
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
        DontDestroyOnLoad(this.gameObject);
        idRoom = 1;

        Debug.Log("conteur " + gameObject.name);

        ComArduino.onConteurHasChoose += FinishChoice;
    }


    private void FinishChoice()
    {
        Debug.Log("GetChoices "+idRoom);
        if(SceneInstance.Instance.currentSceneId == 0)
        {
            Debug.Log("scene 1");
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom1));
        }
        else if(SceneInstance.Instance.currentSceneId == 2)
        {
            Debug.Log("scene 2");
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom2));
        }
        else if(SceneInstance.Instance.currentSceneId == 4)
        {
            Debug.Log("scene 3");
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom3));
        }

        idRoom++;
    }

    void GetChoicesRoom1()
    {
       choices = new List<int>();

        if (arduino.arduinoEnable)
        {
            choices = arduino.GetChoices();

        }
        else
        {
            if(ConteurManager.instance.keyboardChoices1!=null && ConteurManager.instance.keyboardChoices1.Count>0)
            {
                choices = ConteurManager.instance.keyboardChoices1;
            }
            else
            {
                choices.Add(4);
                choices.Add(6);
                choices.Add(10);
            }
        }

        //InterractionManager.instance.setChoicesRoom(choices);
        InterractionManager.instance.LaunchNextScene();
        

    }

    
    
    void GetChoicesRoom2()
    {
        choices = arduino.GetChoices();

        if (arduino.arduinoEnable)
        {
            choices = arduino.GetChoices();

        }
        else
        {
            if(ConteurManager.instance.keyboardChoices2 != null && ConteurManager.instance.keyboardChoices2.Count>0)
            {
                Debug.Log("taille tableau " + ConteurManager.instance.keyboardChoices2.Count);
                choices = ConteurManager.instance.keyboardChoices2;
            }
            else
            {

                choices.Add(2);
                choices.Add(6);
                choices.Add(10);
            }
        }

        //change scene
        InterractionManager.instance.LaunchNextScene();
        

    }

    public void LaunchChoices()
    {
        Debug.Log("coroutine lance");
        StartCoroutine(arduino.ListenForMessages());
        if (arduino.arduinoEnable)
        {            
            arduino.EraseLed();
            //arduino.EnableLed();
        }
        else
        {
            arduino.ErraseLedKeyboard();
            arduino.choiceDone = false;
        }

       
    }

    void GetChoicesRoom3()
    {
        choices = arduino.GetChoices();

        if (arduino.arduinoEnable)
        {
            choices = arduino.GetChoices();

        }
        else
        {            
            if (ConteurManager.instance.keyboardChoices3 != null && ConteurManager.instance.keyboardChoices3.Count>0)
            {
                
                choices = ConteurManager.instance.keyboardChoices3;
            }
            else
            {
                choices.Add(2);
                choices.Add(6);
                choices.Add(10);
            }
        }

        //change scene
        InterractionManager.instance.LaunchNextScene();

        /*
        //set interactions room2
        InterractionManager.instance.setChoicesRoom(choices);
        */
    }
    

    IEnumerator LaunchTimer(float secondesRemaining, System.Action methode)
    {
        timer.gameObject.SetActive(true);
        timer.value = 1f;
        float startTimer = secondesRemaining;


        /*
        if(arduino.arduinoEnable)
        {

            arduino.ErraseLed();
            arduino.EnableLed();
        }
        */

        while (secondesRemaining>0)
        {
            secondesRemaining -= Time.deltaTime;
            float ratio = secondesRemaining / startTimer;

            timer.value = ratio;

            yield return null;
        }

        timer.gameObject.SetActive(false);

        if(arduino.arduinoEnable)
        {
            arduino.DisableLed();
        }

        methode.Invoke();
    }
}
