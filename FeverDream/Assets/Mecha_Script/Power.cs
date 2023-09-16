using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Power : MonoBehaviour
{
    public float minAngle = 0f; // Ángulo mínimo de rotación
    public float maxAngle = 180f; // Ángulo máximo de rotación
    public GameObject rectangulo; // Referencia al objeto rectángulo
    public string comPort = "COM6"; // Puerto COM por defecto, puedes cambiarlo en el Inspector

    SerialPort puerto;

    void Start()
    {
        puerto = new SerialPort(comPort, 9600);

        try
        {
            puerto.Open();
            puerto.ReadTimeout = 1;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al abrir el puerto COM: " + e.Message);
        }
    }

    void Update()
    {
        if (puerto != null && puerto.IsOpen)
        {
            try
            {
                string data = puerto.ReadLine();

                // Convierte la cadena de texto a un número (float)
                float valor = float.Parse(data);

                // Mapea el valor de Arduino al ángulo de rotación entre minAngle y maxAngle
                float angle = Mathf.Lerp(minAngle, maxAngle, valor / 1023f);

                // Aplica la rotación al objeto rectángulo en su eje local (eje X)
                rectangulo.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    private void OnDestroy()
    {
        if (puerto != null && puerto.IsOpen)
        {
            puerto.Close();
        }
    }
}
