using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Power : MonoBehaviour
{
    [Header("Configuración del movimiento")]
    public float minAngle = 0f; // Ángulo mínimo de rotación
    public float maxAngle = 180f; // Ángulo máximo de rotación
    public GameObject rectangulo; // Referencia al objeto a rotar

    [Header("Control auxiliar")]
    public bool controlPorTeclado = true; // Habilita o deshabilita el control manual
    public float velocidadTeclado = 0.5f; // Velocidad de ajuste del valor

    [Header("Estado del puerto")]
    public bool conectado = false;

    private float ultimoValor = 0f; // Para suavizar movimiento
    private float valorTeclado = 0.5f; // Valor inicial al usar teclado (0–1)

    void Update()
    {
        // -----------------------------
        // 1️⃣ Control por Arduino
        // -----------------------------
        if (SerialManager.puerto != null && SerialManager.puerto.IsOpen)
        {
            if (!conectado)
            {
                conectado = true;
                Debug.Log("✅ Conexión con Arduino activa en " + SerialManager.puertoDetectado);
            }

            try
            {
                string data = SerialManager.puerto.ReadLine().Trim();

                if (!string.IsNullOrEmpty(data))
                {
                    if (float.TryParse(data, out float valor))
                    {
                        valor = Mathf.Clamp(valor / 1023f, 0f, 1f);

                        float suavizado = Mathf.Lerp(ultimoValor, valor, Time.deltaTime * 10f);
                        ultimoValor = suavizado;

                        float angle = Mathf.Lerp(minAngle, maxAngle, suavizado);
                        if (rectangulo != null)
                            rectangulo.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
                    }
                }
            }
            catch (System.TimeoutException)
            {
                // No se recibió dato este frame
            }
            catch (System.Exception e)
            {
                Debug.LogError("❌ Error al leer el puerto: " + e.Message);
                conectado = false;
            }
        }
        else
        {
            if (conectado)
            {
                conectado = false;
                Debug.LogWarning("⚠️ Conexión con el Arduino perdida. Usando control por teclado.");
            }

            // -----------------------------
            // 2️⃣ Control auxiliar (teclado)
            // -----------------------------
            if (controlPorTeclado)
            {
                if (Input.GetKey(KeyCode.E))
                    valorTeclado += velocidadTeclado * Time.deltaTime;

                if (Input.GetKey(KeyCode.Q))
                    valorTeclado -= velocidadTeclado * Time.deltaTime;

                // Limitar entre 0 y 1
                valorTeclado = Mathf.Clamp01(valorTeclado);

                // Suavizar movimiento
                float suavizado = Mathf.Lerp(ultimoValor, valorTeclado, Time.deltaTime * 10f);
                ultimoValor = suavizado;

                // Calcular y aplicar rotación
                float angle = Mathf.Lerp(minAngle, maxAngle, suavizado);
                if (rectangulo != null)
                    rectangulo.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
            }
        }
    }
}




