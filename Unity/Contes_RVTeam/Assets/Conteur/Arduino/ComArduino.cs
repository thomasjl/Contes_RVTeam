﻿using System;
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

    private SerialPort sp;

    public bool arduinoEnable = true;


    public bool choiceDone;

    public delegate void OnConteurHasChoose();
    public static event OnConteurHasChoose onConteurHasChoose;


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

    private void Start()
    {
        choiceDone = false;

        StartCoroutine(UpdateLoop());
    }

    IEnumerator UpdateLoop()
    {
        while (true)
        {
            MakeUpdate();
            yield return new WaitForSeconds(.5f);
        }
    }

    private void MakeUpdate()
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

        if (!choiceDone && (button2 || button4) && (button6 || button6) && (button10 || button12))
        {
            if (onConteurHasChoose != null)
            {
                onConteurHasChoose();
            }
            choiceDone = true;
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
        choiceDone = false;
    }

    public void DisableLed()
    {
        Debug.Log("disableLed");
        sp.Write("D");
    }

    public List<int> GetChoices()
    {
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
            sp.Write("2");
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
            sp.Write("6");
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
            sp.Write("9");
        }

        return result;
    }


}
