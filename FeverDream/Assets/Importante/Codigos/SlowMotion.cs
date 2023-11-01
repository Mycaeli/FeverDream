using System.Configuration;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowdownFactor = 0.5f; // Factor de ralentizaci�n (0.5 significa la mitad de velocidad).
    public float slowdownDuration = 2f; // Duraci�n de la ralentizaci�n en segundos.

    public GameObject canvas; // El Canvas que deseas activar y desactivar.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f; // Ajusta el tiempo fijo para la f�sica.

            // Activa el Canvas cuando el jugador entra en el trigger.
            if (canvas != null)
            {
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f; // Restaura la velocidad del tiempo a su valor normal.
            Time.fixedDeltaTime = 0.02f; // Restaura el tiempo fijo de la f�sica.

            // Desactiva el Canvas cuando el jugador sale del trigger.
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }
    }
}


