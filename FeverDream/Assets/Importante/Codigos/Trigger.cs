using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{
    public GameObject objectToActivate; // Arrastra el objeto a activar desde el Inspector
    public GameObject objectToShowFor5Seconds;
    private bool isPlayerInsideTrigger = false;

    private Coroutine disableCoroutine; // Almacena una referencia a la corrutina

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = true;
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = false;
            objectToActivate.SetActive(false);

            // Detiene la corrutina si el jugador sale del trigger
            if (disableCoroutine != null)
            {
                StopCoroutine(disableCoroutine);
            }

            // Desactiva ambos objetos
            objectToShowFor5Seconds.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInsideTrigger && Input.GetMouseButtonDown(0))
        {
            objectToActivate.SetActive(false);
            objectToShowFor5Seconds.SetActive(true);

            // Inicia la corrutina y almacena una referencia para poder detenerla si es necesario
            disableCoroutine = StartCoroutine(DisableObjectsAfterDelay(2.0f));
        }
    }

    private IEnumerator DisableObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Asegúrate de que el jugador aún esté dentro del trigger antes de desactivar los objetos
        if (isPlayerInsideTrigger)
        {
            objectToActivate.SetActive(true);
            objectToShowFor5Seconds.SetActive(false);
        }
    }
}

