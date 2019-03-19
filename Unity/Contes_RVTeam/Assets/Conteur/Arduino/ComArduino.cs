using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ComArduino : MonoBehaviour {

    public bool button2;
    public bool button4;
    public bool button6;
    public bool button8;
    public bool button10;
    public bool button12;

    //private SerialPort sp;

    public bool arduinoEnable = true;


    public bool choiceDone;

    public delegate void OnConteurHasChoose();
    public static event OnConteurHasChoose onConteurHasChoose;

    public bool listenToInput;

    /*
    private void Awake()
    {
        
        try
        {
            sp = new SerialPort("\\\\.\\COM12", 9600);
            sp.Open();
            sp.ReadTimeout = 25;

            if (sp.IsOpen)
            {
                Debug.Log("Port is openned");
            }
            else
            {
                Debug.Log("Port is closed");
            }

            EnableLed();
        }
        catch (Exception)
        {
            arduinoEnable = false;

        }
        


    }
    */

    private void Awake()
    {
        listenToInput = false;
    }

    private void Start()
    {

        //Launch Thread
        //ArduinoThread.instance.StartThread();

        Debug.Log("start com");
        //EnableLed();

        choiceDone = false;

    }

   
    public IEnumerator ListenForMessages()
    {
        while(true)
        {
            if(arduinoEnable)
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
        String msg = ArduinoThread.instance.ReadFromArduino();

        if(msg!=null)
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
                if (onConteurHasChoose != null)
                {
                    onConteurHasChoose();
                }
                choiceDone = true;

                
            }
        }
       
    }


    private void Update()
    {
        if(listenToInput)
        {
            Debug.Log("valide");
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Debug.Log("press 1");
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
                Debug.Log("conteur a choisi");
                if (onConteurHasChoose != null)
                {
                    onConteurHasChoose();
                }
                choiceDone = true;
                listenToInput = false;

                List<int> tempKeyboard = new List<int>();
                tempKeyboard.Add((button2) ? 2 : 4);
                tempKeyboard.Add((button6) ? 6 : 8);
                tempKeyboard.Add((button10) ? 10 : 12);

                if (SceneInstance.instance.currentSceneId == 0)
                {

                    Debug.Log("taille du keykey1 " + tempKeyboard.Count);
                    ConteurManager.instance.keyboardChoices1 = tempKeyboard;
                }
                else if (SceneInstance.instance.currentSceneId == 2)
                {
                    Debug.Log("taille du keykey2 " + tempKeyboard.Count);
                    ConteurManager.instance.keyboardChoices2 = tempKeyboard;
                }
                else if (SceneInstance.instance.currentSceneId == 4)
                {

                    Debug.Log("taille du keykey3 " + tempKeyboard.Count);
                    ConteurManager.instance.keyboardChoices3 = tempKeyboard;
                }
            }
        }
       
    }

    /*
    private void Update()
    {
        
    
        if (sp.IsOpen)
        {
            try
            {
                string msgFromArduino = sp.ReadByte().ToString();
                // Debug.Log("detect button" + msgFromArduino + "**");

                switch (msgFromArduino)
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
            }
            catch (Exception e)
            {
                //Debug.LogException(e, this);
            }
        }

        if (!choiceDone && (button2 || button4) && (button6 || button8) && (button10 || button12))
        {
            Debug.Log("conteur a choisi");
            if (onConteurHasChoose != null)
            {
                onConteurHasChoose();
            }
            choiceDone = true;
        }
    }
    */

    public void ErraseLed()
    {
        Debug.Log("erraseLed");

        //sp.Write("R");

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

    public void ErraseLedKeyboard()
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
        //sp.Write("E");
        //Send "E" to Arduino
        ArduinoThread.instance.SendToArduino("E");

        choiceDone = false;
    }

    public void DisableLed()
    {
        Debug.Log("disableLed");
        //sp.Write("D");
        // Send "D" to Arduino
        ArduinoThread.instance.SendToArduino("D");

    }

    public List<int> GetChoices()
    {
        StopAllCoroutines();

        Debug.Log("get choices");
        List<int> result = new List<int>();

        if (button2)
        {
            result.Add(2);
        }
        else if (button4)
        {
            result.Add(4);
        }
        else
        {
            //default value
            result.Add(2);

            //sp.Write("2");
            ArduinoThread.instance.SendToArduino("2");

        }

        if (button6)
        {
            result.Add(6);
        }
        else if (button8)
        {
            result.Add(8);
        }
        else
        {
            //default value
            result.Add(6);
            //sp.Write("6");
            ArduinoThread.instance.SendToArduino("6");

        }

        if (button10)
        {
            result.Add(10);
        }
        else if (button12)
        {
            result.Add(12);
        }
        else
        {
            //default value
            result.Add(10);
            //sp.Write("9");
            ArduinoThread.instance.SendToArduino("9");

        }

        return result;
    }


}
