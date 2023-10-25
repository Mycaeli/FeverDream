using UnityEngine;

public class SlowMotion: MonoBehaviour
{
    public float slowdownFactor = 0.5f; // Factor de ralentización (0.5 significa la mitad de velocidad).
    public float slowdownDuration = 2f; // Duración de la ralentización en segundos.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f; // Ajusta el tiempo fijo para la física.

            // Puedes restaurar el tiempo a su valor normal en la OnTriggerExit o después de un cierto tiempo.
            // Puedes usar una corrutina o un temporizador para eso.
        }
    }

    // Asegúrate de restaurar el tiempo cuando el jugador salga del trigger.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f; // Restaura la velocidad del tiempo a su valor normal.
            Time.fixedDeltaTime = 0.02f; // Restaura el tiempo fijo de la física.
        }
    }
}

