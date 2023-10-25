using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Power : MonoBehaviour
{
    public float minAngle = 0f; // �ngulo m�nimo de rotaci�n
    public float maxAngle = 180f; // �ngulo m�ximo de rotaci�n
    public GameObject rectangulo; // Referencia al objeto rect�ngulo


    void Update()
    {
        // Utiliza el puerto serie desde SerialManager
        if (SerialManager.puerto != null && SerialManager.puerto.IsOpen)
        {
            try
            {
                string data = SerialManager.puerto.ReadLine();
                float valor = float.Parse(data);
                float angle = Mathf.Lerp(minAngle, maxAngle, valor / 1023f);
                Debug.Log(angle);

                // Verificar que rectangulo no sea nulo antes de usarlo
                if (rectangulo != null)
                {
                    rectangulo.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
                }
            }
            catch (System.Exception)
            {
                // Manejar la excepci�n si es necesario
            }
        }
    }


}





