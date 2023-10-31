using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour
{
    public static SerialPort puerto;
    public List<string> comPortNames = new List<string>(); // List of COM port names

    private bool portDetected = false;

    void Start()
    {
        if (!portDetected)
        {
            // Check if the comPortNames list is not empty
            if (comPortNames.Count > 0)
            {
                // Use the first available COM port if any exist in the list
                string lastDetectedPort = comPortNames[0];
                portDetected = true;

                // Configure and open the selected port
                puerto = new SerialPort(lastDetectedPort, 9600);
                puerto.ReadTimeout = 1;

                if (!puerto.IsOpen)
                {
                    puerto.Open();
                }
            }
            else
            {
                Debug.LogWarning("No COM ports available.");
            }
        }
    }

    void OnApplicationQuit()
    {
        if (puerto != null && puerto.IsOpen)
        {
            puerto.Close();
        }
    }
}

