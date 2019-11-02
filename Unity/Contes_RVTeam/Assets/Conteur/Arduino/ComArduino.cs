using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComArduino : MonoBehaviour
{
    public bool button2;
    public bool button4;
    public bool button6;
    public bool button8;
    public bool button10;
    public bool button12;

    public bool arduinoEnable = true;
    public bool listenToInput;

    public bool choiceDone;

    public static event Action ConteurChose;



    private void Awake()
    {
        listenToInput = false;
    }

    private void Start()
    {
        Debug.Log("ComArduino is running");
        choiceDone = false;
    }

    private void Update()
    {
        if (listenToInput)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                button2 = true;
                button4 = false;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                button2 = false;
                button4 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                button6 = true;
                button8 = false;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                button6 = false;
                button8 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                button10 = true;
                button12 = false;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                button10 = false;
                button12 = true;
            }

            if (!choiceDone && (button2 || button4) && (button6 || button8) && (button10 || button12))
            {
                Debug.Log("le conteur a choisi");
                if (ConteurChose != null)
                    ConteurChose();
                choiceDone = true;
                listenToInput = false;

                List<int> tempKeyboard = new List<int>();
                tempKeyboard.Add(button2 ? 2 : 4);
                tempKeyboard.Add(button6 ? 6 : 8);
                tempKeyboard.Add(button10 ? 10 : 12);

                if (SceneInstance.Instance.currentSceneId == 0)
                    ConteurManager.instance.keyboardChoices1 = tempKeyboard;
                else if (SceneInstance.Instance.currentSceneId == 2)
                    ConteurManager.instance.keyboardChoices2 = tempKeyboard;
                else if (SceneInstance.Instance.currentSceneId == 4)
                    ConteurManager.instance.keyboardChoices3 = tempKeyboard;
            }
        }
    }


    public IEnumerator ListenForMessages()
    {
        while (true)
        {
            if (arduinoEnable)
            {
                CheckComingMessages();
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                Debug.Log("listen for message");
                listenToInput = true;
                yield break;
            }
        }
    }

    private void CheckComingMessages()
    {
        string msg = ArduinoThread.instance.ReadFromArduino();

        if (msg != null)
        {
            Debug.Log("message reçu d'arduino : " + msg);
            switch (msg)
            {
                case ("2"):
                    {
                        button2 = true;
                        button4 = false;
                        Debug.Log("2");
                        break;
                    }
                case ("4"):
                    {
                        button4 = true;
                        button2 = false;
                        Debug.Log("4");
                        break;
                    }
                case ("6"):
                    {
                        button6 = true;
                        button8 = false;
                        Debug.Log("6");
                        break;
                    }
                case ("8"):
                    {
                        button8 = true;
                        button6 = false;
                        Debug.Log("8");
                        break;
                    }
                case ("3"):
                    {
                        button10 = true;
                        button12 = false;
                        Debug.Log("10");
                        break;
                    }
                case ("5"):
                    {
                        button12 = true;
                        button10 = false;
                        Debug.Log("12");
                        break;
                    }
            }

            if (!choiceDone && (button2 || button4) && (button6 || button8) && (button10 || button12))
            {
                Debug.Log("conteur a choisi");
                if (ConteurChose != null)
                    ConteurChose();
                choiceDone = true;
            }
        }
    }


    public void EraseLed()
    {
        Debug.Log("erasing a led");

        //Send "R" to Arduino
        ArduinoThread.instance.SendToArduino("R");
        button2 = false;
        button4 = false;
        button6 = false;
        button8 = false;
        button10 = false;
        button12 = false;

        EnableLed();
    }

    public void EraseLedKeyboard()
    {
        button2 = false;
        button4 = false;
        button6 = false;
        button8 = false;
        button10 = false;
        button12 = false;
    }

    public void EnableLed()
    {
        Debug.Log("enableChoices");
        ArduinoThread.instance.SendToArduino("E");

        choiceDone = false;
    }

    public void DisableLed()
    {
        Debug.Log("disabling a led");
        ArduinoThread.instance.SendToArduino("D");
    }

    public List<int> GetChoices()
    {
        StopAllCoroutines();

        Debug.Log("get choices");
        List<int> result = new List<int>();

        if (button2)
            result.Add(2);
        else if (button4)
            result.Add(4);
        else
        {
            //default value
            result.Add(2);
            ArduinoThread.instance.SendToArduino("2");
        }

        if (button6)
            result.Add(6);
        else if (button8)
            result.Add(8);
        else
        {
            //default value
            result.Add(6);
            ArduinoThread.instance.SendToArduino("6");
        }

        if (button10)
            result.Add(10);
        else if (button12)
            result.Add(12);
        else
        {
            //default value
            result.Add(10);
            ArduinoThread.instance.SendToArduino("9");
        }
        return result;
    }
}
