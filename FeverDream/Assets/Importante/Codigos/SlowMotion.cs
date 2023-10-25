using UnityEngine;

public class SlowMotion: MonoBehaviour
{
    public float slowdownFactor = 0.5f; // Factor de ralentizaci�n (0.5 significa la mitad de velocidad).
    public float slowdownDuration = 2f; // Duraci�n de la ralentizaci�n en segundos.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f; // Ajusta el tiempo fijo para la f�sica.

            // Puedes restaurar el tiempo a su valor normal en la OnTriggerExit o despu�s de un cierto tiempo.
            // Puedes usar una corrutina o un temporizador para eso.
        }
    }

    // Aseg�rate de restaurar el tiempo cuando el jugador salga del trigger.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f; // Restaura la velocidad del tiempo a su valor normal.
            Time.fixedDeltaTime = 0.02f; // Restaura el tiempo fijo de la f�sica.
        }
    }
}

