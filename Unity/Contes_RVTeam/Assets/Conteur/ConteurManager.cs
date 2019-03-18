using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConteurManager : MonoBehaviour {

    public ComArduino arduino;
    public Slider timer;

    private List<int> defaultValueChoices = new List<int> { 2,6, 10 };

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
        //LaunchChoicesRoom1();

        DontDestroyOnLoad(this.gameObject);
        idRoom = 1;

        Debug.Log("conteur " + gameObject.name);

        ComArduino.onConteurHasChoose += FinishChoice;

        //LaunchChoices();
    }

    private void FinishChoice()
    {
        Debug.Log("GetChoices "+idRoom);
        if(idRoom==1)
        {
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom1));
        }
        else if(idRoom==2)
        {
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom2));
        }
        else if(idRoom==3)
        {
            StartCoroutine(LaunchTimer(5f, GetChoicesRoom3));
        }

        idRoom++;
    }

    /*
    private void LaunchChoicesRoom1()
    {
        StartCoroutine(LaunchTimer( 1f, GetChoicesRoom1));
    }
    public void LaunchChoicesRoom2()
    {
        StartCoroutine(LaunchTimer(30f, GetChoicesRoom2));
    }

    public void LaunchChoicesRoom3()
    {
        StartCoroutine(LaunchTimer(30f, GetChoicesRoom3));
    }
    */
    void GetChoicesRoom1()
    {
       choices = new List<int>();

        if (arduino.arduinoEnable)
        {
            choices = arduino.GetChoices();

        }
        else
        {
            choices.Add(4);
            choices.Add(6);
            choices.Add(10);
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
            choices.Add(2);
            choices.Add(6);
            choices.Add(10);
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
            arduino.ErraseLed();
            //arduino.EnableLed();
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
            choices.Add(2);
            choices.Add(6);
            choices.Add(10);
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
