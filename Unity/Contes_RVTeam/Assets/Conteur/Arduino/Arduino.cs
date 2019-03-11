using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class Arduino : MonoBehaviour {

    private SerialPort sp = new SerialPort("\\\\.\\COM12", 9600);

    public bool led3;
    public bool led5;
    public bool led7;
    public bool led9;
    public bool led11;
    public bool led13;



    private void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;

        if(sp.IsOpen)
        {
            Debug.Log("Port is openned");
        }
        else
        {
            Debug.Log("Port is closed");
        }

        EraseChoices();
        EnableChoices();
    }

    private void Update()
    {
        
        if(sp.IsOpen)
        {
            try
            {
                string msgFromArduino = sp.ReadByte().ToString();
                Debug.Log("detect button"+ msgFromArduino + "**");
                switch(msgFromArduino)
                {
                    case ("2"): 
                    {
                        Debug.Log("3");
                        led3 = true;
                        led5 = false;
                        break;
                    }
                    case ("4"):
                    {
                        Debug.Log("5");
                        led5 = true;
                        led3 = false;
                        break;
                    }
                    case ("6"):
                    {
                        Debug.Log("7");
                        led7 = true;
                        led9 = false;
                        break;
                    }
                    case ("8"):
                    {
                        Debug.Log("9");
                        led9 = true;
                        led7 = false;
                        break;
                    }
                    case ("10"):
                    {
                        Debug.Log("11");
                        led11 = true;
                        led13 = false;
                        break;
                    }
                    case ("12"):
                    {
                        Debug.Log("13");
                        led11 = false;
                        led13 = true;
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

    public void EraseChoices()
    {
        Debug.Log("erase");
        sp.Write("R");

        led3 = false;
        led5 = false;
        led7 = false;
        led9 = false;
        led11 = false;
        led13 = false;

        EnableChoices();
    }

    public void EnableChoices()
    {
        Debug.Log("enableChoices");
        sp.Write("E");
    }

    public void DisableChoices()
    {
        Debug.Log("disableChoices");
        sp.Write("D");

        if(!led3 && !led5)
        {
            led3 = true;
            sp.Write("3");
        }
        if (!led7 && !led9)
        {
            led7 = true;
            sp.Write("7");
        }
        if (!led11 && !led13)
        {
            //Larduino lit un chiffre
            led11 = true;
            sp.Write("9");
        }
    }

    
}
