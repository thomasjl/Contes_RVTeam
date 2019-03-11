using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConteurManager : MonoBehaviour {

    public ComArduino arduino;
    public Slider timer;

    private void Start()
    {
        LaunchChoicesRoom1();

        DontDestroyOnLoad(this.gameObject);
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
       List<int> choices = arduino.GetChoices();     
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

        arduino.ErraseLed();
        arduino.EnableLed();

        while (secondesRemaining>0)
        {
            secondesRemaining -= Time.deltaTime;
            float ratio = secondesRemaining / startTimer;

            timer.value = ratio;

            yield return null;
        }

        timer.gameObject.SetActive(false);

        arduino.DisableLed();

        methode.Invoke();
    }
}
