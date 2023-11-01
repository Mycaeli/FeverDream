using System.Configuration;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowdownFactor = 0.5f; // Factor de ralentización (0.5 significa la mitad de velocidad).
    public float slowdownDuration = 2f; // Duración de la ralentización en segundos.

    public GameObject canvas; // El Canvas que deseas activar y desactivar.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f; // Ajusta el tiempo fijo para la física.

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
            Time.fixedDeltaTime = 0.02f; // Restaura el tiempo fijo de la física.

            // Desactiva el Canvas cuando el jugador sale del trigger.
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }
    }
}


