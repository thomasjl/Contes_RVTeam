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

    private SerialPort sp = new SerialPort("\\\\.\\COM12", 9600);


    private void Start()
    {
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
    }

    public void ErraseLed()
    {
        Debug.Log("erraseLed");
        sp.Write("R");
        button2 = false;
        button4 = false;
        button6 = false;
        button8 = false;
        button10 = false;
        button12 = false;

        EnableLed();
    }

    public void EnableLed()
    {
        Debug.Log("enableChoices");
        sp.Write("E");
    }

    public void DisableLed()
    {
        Debug.Log("disableLed");
        sp.Write("D");
    }



}
