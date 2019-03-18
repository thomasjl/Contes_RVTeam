using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ArduinoThread : MonoBehaviour {

    private Thread thread;

    private Queue outputQueue;
    private Queue inputQueue;

    private string portName = "\\\\.\\COM12";
    private int baudRate = 9600;

    SerialPort stream;

    private bool threadIsRunning=false;

    public static ArduinoThread instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

   

    public void StartThread()
    {
        outputQueue = Queue.Synchronized(new Queue());
        inputQueue = Queue.Synchronized(new Queue());

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
        if(inputQueue.Count == 0)
        {
            return null;
        }

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
        //open the connetion on the serial port
        stream = new SerialPort(portName, baudRate);
        stream.ReadTimeout = 20;
        stream.Open();

        while(true && threadIsRunning)
        {
            //send to Arduino
            if(outputQueue.Count != 0)
            {
                string command = (string) outputQueue.Dequeue();
                WriteToArduino(command);

            }

            //read from Arduino
            string result = ReadFromArduino(20);
            if (result !=null)
            {
                inputQueue.Enqueue(result);
            }            
        }
    }

    private void OnDestroy()
    {
       //Put boolean, otherwis, thread still run, after exiting application
       if(threadIsRunning)
        {
            threadIsRunning = false;

            thread.Join();
        }
    }
}
