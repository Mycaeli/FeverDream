using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour
{
    public static SerialPort puerto; // Esta propiedad ser� accesible de forma est�tica
    public string COM;

    void Start()
    {
        // Configurar el puerto serie
        puerto = new SerialPort(COM, 9600);
        puerto.ReadTimeout = 1;

        // Abrir el puerto
        puerto.Open();
    }

    void OnApplicationQuit()
    {
        // Cerrar el puerto serie antes de salir de la aplicaci�n
        if (puerto != null && puerto.IsOpen)
        {
            puerto.Close();
        }
    }
}
