using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour
{
    public static SerialPort puerto; // This property will be accessible statically
    private string lastDetectedPort; // Store the last detected port
    private bool portDetected = false; // Flag to check if port is detected

    void Start()
    {
        if (!portDetected)
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                // Use the first available COM port if any exist
                lastDetectedPort = ports[0];
                portDetected = true;
            }
            else
            {
                Debug.LogWarning("No COM ports available.");
                return;
            }
        }

        // Configure the serial port
        puerto = new SerialPort(lastDetectedPort, 9600);
        puerto.ReadTimeout = 1;

        // Open the port if it's not already open
        if (!puerto.IsOpen)
        {
            puerto.Open();
        }
    }

    void OnApplicationQuit()
    {
        // Close the serial port before quitting the application
        if (puerto != null && puerto.IsOpen)
        {
            puerto.Close();
        }
    }
}

