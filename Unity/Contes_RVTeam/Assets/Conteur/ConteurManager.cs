using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConteurManager : MonoBehaviour {

    public ComArduino arduino;
    public Slider timer;

    public Chaperon chaperon;
    private MaisonChaperon maisonChaperon;
    public static ConteurManager instance;


    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LaunchChoicesRoom1();

        DontDestroyOnLoad(this.gameObject);

        chaperon = GameObject.Find("Chaperon").GetComponent<Chaperon>();
        maisonChaperon = GameObject.Find("MaisonChaperon").GetComponent<MaisonChaperon>();

    }

    private void LaunchChoicesRoom1()
    {
        StartCoroutine(LaunchTimer( 3f, GetChoicesRoom1));
    }
    private void LaunchChoicesRoom2()
    {
        StartCoroutine(LaunchTimer(30f, GetChoicesRoom2));
    }

    private void LaunchChoicesRoom3()
    {
        StartCoroutine(LaunchTimer(30f, GetChoicesRoom3));
    }

    void GetChoicesRoom1()
    {
        List<int> choices = new List<int>();

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

        if (choices[0] == 2)
        {
            chaperon.SetFirstChoice(2);
        }
        else if (choices[0] == 4)
        {
            chaperon.SetFirstChoice(4);
        }
        else
        {
            chaperon.SetFirstChoice(2);
        }

        if (choices[1] == 6)
        {
            maisonChaperon.SetSecondChoice(6);
        }
        else if (choices[1] == 8)
        {
            maisonChaperon.SetSecondChoice(8);
        }
        else
        {
            maisonChaperon.SetSecondChoice(6);
        }

        if (choices[1] == 10)
        {

        }
        else if(choices[1] == 12)
        {

        }
        else
        {

        }

    }

    void GetChoicesRoom2()
    {
        List<int> choices = arduino.GetChoices();
        
    }

    void GetChoicesRoom3()
    {
        List<int> choices = arduino.GetChoices();
    }

    IEnumerator LaunchTimer(float secondesRemaining, System.Action methode)
    {
        timer.gameObject.SetActive(true);
        timer.value = 1f;
        float startTimer = secondesRemaining;

        if(arduino.arduinoEnable)
        {

            arduino.ErraseLed();
            arduino.EnableLed();
        }

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
