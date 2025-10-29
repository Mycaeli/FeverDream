using UnityEngine;
using TMPro;
using System.Collections;

public class UI_Arduino : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_Text estadoText;              // Texto TMP para mostrar el estado
    public SerialManager serialManager;      // Referencia al script SerialManager (opcional)
    public TMP_Text SerialCode;
    public GameObject UIPort;
    public bool IsTextOnly;

    [Header("Configuración")]
    public float tiempoActualizacion = 2f;   // Cada cuánto se revisa el estado

    private void Start()
    {
        UIPort.SetActive(false);
        if (estadoText == null)
        {
            Debug.LogError("⚠️ No se asignó el componente TMP_Text en UI_Arduino.");
            return;
        }

        if (serialManager == null)
            serialManager = FindObjectOfType<SerialManager>();

        StartCoroutine(MonitorearConexion());
    }

    private IEnumerator MonitorearConexion()
    {
        while (true)
        {
            // Si no hay puerto detectado aún
            if (SerialManager.puerto == null)
            {
                MostrarMensaje("Buscando Arduino...");
                UIPort.SetActive(false);
            }
            else if (SerialManager.puerto.IsOpen)
            {
                if (IsTextOnly)
                {
                    MostrarMensaje($"El arduino fue encontrado en el puerto: {SerialManager.puertoDetectado}");
                    UIPort.SetActive(false);
                }
                else
                {
                    MostrarMensaje(null);
                    UIPort.SetActive(true);
                    MostrarPuerto(SerialManager.puertoDetectado);
                }
            }
            else
            {
                MostrarMensaje("No se detectó Arduino. Puedes usar Q y E en su lugar.");
                UIPort.SetActive(false);
            }

            yield return new WaitForSeconds(tiempoActualizacion);
        }

    }

    private void MostrarMensaje(string mensaje)
    {
        if (estadoText != null)
            estadoText.text = mensaje;

        Debug.Log(mensaje);
    }
    private void MostrarPuerto(string mensaje)
    {
        if (SerialCode != null)
            SerialCode.text = mensaje;

        Debug.Log(mensaje);
    }
}

