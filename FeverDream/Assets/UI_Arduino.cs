using UnityEngine;
using TMPro;
using System.Collections;

public class UI_Arduino : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_Text estadoText;              // Texto TMP para mostrar el estado
    public SerialManager serialManager;      // Referencia al script SerialManager (opcional)
    public GameObject Connect;
    public TMP_Text Puerto;

    [Header("Configuración")]
    public float tiempoActualizacion = 2f;   // Cada cuánto se revisa el estado

    private void Start()
    {
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
            // Si no hay puertos detectados aún
            if (SerialManager.puerto == null)
            {
                Connect.SetActive(false);
                MostrarMensaje(" Buscando Arduino...");
                
            }
            else if (SerialManager.puerto != null && SerialManager.puerto.IsOpen)
            {
                Connect.SetActive(true);
                MostrarMensaje(null);
                //MostrarMensaje($" Arduino conectado en {SerialManager.puertoDetectado}");
                MostrarPuerto($"Puerto {SerialManager.puertoDetectado}");

            }
            else
            {
                Connect.SetActive(false);
                MostrarMensaje("No se encontro Arduino, Usar Q y E en su lugar");

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
        if (Puerto != null)
            Puerto.text = mensaje;

        Debug.Log(mensaje);
    }
}
