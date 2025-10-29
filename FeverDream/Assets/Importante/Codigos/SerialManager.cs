using UnityEngine;
using System.IO.Ports;
using System.Collections;
using System.Linq;
using System;

public class SerialManager : MonoBehaviour
{
    public static SerialPort puerto;
    public static string puertoDetectado = "";

    [Header("Configuración de conexión")]
    public int baudRate = 9600;
    public float tiempoEspera = 1.0f; // Tiempo máximo para leer datos
    public float intervaloReconexion = 2.0f; // Segundos entre reintentos

    private bool buscando = false;
    private bool conectado = false;

    void Start()
    {
        StartCoroutine(GestionConexion());
    }

    private IEnumerator GestionConexion()
    {
        while (true)
        {
            if (!conectado)
            {
                yield return StartCoroutine(DetectarPuertos());
            }
            else
            {
                // Verificar si el puerto sigue abierto
                if (puerto == null || !puerto.IsOpen)
                {
                    Debug.LogWarning("⚠️ Conexión con Arduino perdida. Intentando reconectar...");
                    conectado = false;
                }
                else
                {
                    try
                    {
                        // Leer datos para confirmar conexión
                        string data = puerto.ReadExisting();
                        if (data.Length == 0)
                        {
                            // No hay datos, pero el puerto sigue estable
                        }
                    }
                    catch
                    {
                        Debug.LogWarning("⚠️ Error al leer desde el puerto. Intentando reconectar...");
                        conectado = false;
                        try { puerto.Close(); } catch { }
                    }
                }
            }

            yield return new WaitForSeconds(intervaloReconexion);
        }
    }

    private IEnumerator DetectarPuertos()
    {
        if (buscando) yield break;
        buscando = true;

        string[] puertosDisponibles = SerialPort.GetPortNames();

        if (puertosDisponibles.Length == 0)
        {
            Debug.Log("🚫 No se encontraron puertos COM disponibles.");
            buscando = false;
            yield break;
        }

        Debug.Log("🔍 Buscando Arduino en los siguientes puertos:");
        foreach (string port in puertosDisponibles)
            Debug.Log("  • " + port);

        foreach (string port in puertosDisponibles)
        {
            SerialPort testPort = new SerialPort(port, baudRate)
            {
                ReadTimeout = 200
            };

            try
            {
                testPort.Open();
            }
            catch
            {
                continue; // pasa al siguiente puerto
            }

            yield return new WaitForSeconds(2f); // esperar a que Arduino se reinicie

            float startTime = Time.time;
            while (Time.time - startTime < tiempoEspera)
            {
                string data = "";
                try
                {
                    data = testPort.ReadLine().Trim();
                }
                catch { }

                if (!string.IsNullOrEmpty(data) && data.All(char.IsDigit))
                {
                    Debug.Log($"✅ Arduino detectado en {port}, dato leído: {data}");
                    puerto = testPort;
                    puertoDetectado = port;
                    conectado = true;
                    buscando = false;
                    yield break;
                }

                yield return null;
            }

            // Cierra si no se detectó nada válido
            testPort.Close();
        }

        Debug.Log("🚫 No se detectó ningún Arduino activo.");
        buscando = false;
    }

    void OnApplicationQuit()
    {
        if (puerto != null && puerto.IsOpen)
        {
            puerto.Close();
            Debug.Log("🔒 Puerto cerrado correctamente.");
        }
    }
}

