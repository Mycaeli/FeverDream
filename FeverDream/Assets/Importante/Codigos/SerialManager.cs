using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour
{
    public static SerialPort puerto;
    public List<string> comPortNames = new List<string>(); // List of COM port names

    private bool portDetected = false;

    private string _com; // Campo privado para almacenar el valor de com

    public string com
    {
        get { return _com; } // Getter público
        set { _com = value; } // Setter público
    }

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

                // Guardar el valor de com en PlayerPrefs (o en otro lugar según tus necesidades)
                _com = lastDetectedPort;
                PlayerPrefs.SetString("com", lastDetectedPort);
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

