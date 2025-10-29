using UnityEngine;
using System.IO.Ports;

public class Power : MonoBehaviour
{
    [Header("Configuración del movimiento")]
    public float minAngle = 0f; // Ángulo mínimo de rotación
    public float maxAngle = 180f; // Ángulo máximo de rotación
    public GameObject rectangulo; // Objeto a rotar

    [Header("Control por teclado (modo auxiliar)")]
    public bool modoTeclado = false;
    public float velocidadTeclado = 0.5f;

    private bool conectado = false;
    private float ultimoValor = 0f; // Valor suavizado
    private float valorTeclado = 0.5f; // Valor inicial cuando se usa teclado

    void Update()
    {
        // Si no hay puerto disponible o cerrado
        if (SerialManager.puerto == null || !SerialManager.puerto.IsOpen)
        {
            if (conectado)
            {
                conectado = false;
                Debug.LogWarning("⚠️ Conexión con Arduino perdida. Cambiando a modo teclado...");
            }

            modoTeclado = true; // activar control por teclado
        }
        else
        {
            // Si el puerto está abierto, usar Arduino
            if (!conectado)
            {
                conectado = true;
                modoTeclado = false;
                Debug.Log("✅ Conexión con Arduino activa en " + SerialManager.puertoDetectado);
            }
        }

        // --- CONTROL PRINCIPAL ---
        if (modoTeclado)
        {
            ControlPorTeclado();
        }
        else
        {
            LeerDesdeArduino();
        }
    }

    private void LeerDesdeArduino()
    {
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
                else
                {
                    Debug.LogWarning("⚠️ Valor recibido inválido: " + data);
                }
            }
        }
        catch (System.TimeoutException) { }
        catch (System.Exception e)
        {
            Debug.LogError("❌ Error al leer el puerto: " + e.Message);
        }
    }

    private void ControlPorTeclado()
    {
        if (Input.GetKey(KeyCode.E))
            valorTeclado += velocidadTeclado * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q))
            valorTeclado -= velocidadTeclado * Time.deltaTime;

        valorTeclado = Mathf.Clamp01(valorTeclado);
        float angle = Mathf.Lerp(minAngle, maxAngle, valorTeclado);

        if (rectangulo != null)
            rectangulo.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }
}
