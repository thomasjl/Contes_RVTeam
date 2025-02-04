﻿using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ArduinoThread : MonoBehaviour {

    Thread thread;

    Queue outputQueue;
    Queue inputQueue;

    int defaultPortNumber = 12;
    string portName;
    int baudRate = 9600;

    SerialPort stream;

    private bool threadIsRunning = false;

    public static ArduinoThread instance;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;

        // Set port name.
        if (!PlayerPrefs.HasKey(DebugInterface.COMKey))
            PlayerPrefs.SetInt(DebugInterface.COMKey, defaultPortNumber);
        string portNumber = PlayerPrefs.GetInt(DebugInterface.COMKey).ToString();
        portName = (portNumber.Length > 1 ? "\\\\.\\" : "") + "COM" + portNumber;
        print("Using " + portName);

        StartThread();
    }


    public void StartThread()
    {
        outputQueue = Queue.Synchronized(new Queue());
        inputQueue = Queue.Synchronized(new Queue());

        Debug.Log("Starting the Arduino thread");

        threadIsRunning = true;

        // Creates and starts the thread
        thread = new Thread(ThreadLoop);
        thread.Start();
    }

    public void SendToArduino(string command)
    {
        outputQueue.Enqueue(command);
    }

    public string ReadFromArduino()
    {
        if (inputQueue.Count == 0)
            return null;

        return (string)inputQueue.Dequeue();
    }

    private void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    private string ReadFromArduino(int timeout = 0)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException e)
        {
            return null;
        }
    }

    public void ThreadLoop()
    {
        Debug.Log("Starting the Arduino thread loop");
        //open the connetion on the serial port
        try
        {
            stream = new SerialPort(portName, baudRate);
            stream.ReadTimeout = 20;
            stream.Open();


            while (threadIsRunning)
            {
                //send to Arduino
                if (outputQueue.Count != 0)
                {
                    string command = (string)outputQueue.Dequeue();
                    WriteToArduino(command);
                }

                //read from Arduino
                string result = ReadFromArduino(20);
                if (result != null)
                {
                    inputQueue.Enqueue(result);
                }
            }
        }
        catch (Exception)
        {
            Debug.Log("/!\\ Cannot open the SerialPort");
            ConteurManager.instance.arduino.arduinoEnable = false;
            threadIsRunning = false;
        }

    }

    private void OnDestroy()
    {
        //Put boolean, otherwis, thread still run, after exiting application
        if (threadIsRunning)
        {
            threadIsRunning = false;

            thread.Join();
        }
    }
}
