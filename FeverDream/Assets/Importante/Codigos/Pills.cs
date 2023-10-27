using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pills : MonoBehaviour, II
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public float destroyDelay = 5.0f; // Tiempo en segundos antes de destruir el objeto
    private bool isInteracted = false; // Controla si el jugador ha interactuado

    public string GetDescription()
    {
        if (isInteracted)
        {
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
            }
            return "";
        }
        else
        {
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(true);
            }
            return "Activar el objeto.";
        }
    }

    public void Interact()
    {
        // Activa el objeto a activar
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // Desactiva y destruye el objeto a desactivar
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
            Destroy(objectToDeactivate);
        }

        isInteracted = true; // El jugador ha interactuado, el mensaje se desactiva

        // Comienza el conteo de tiempo para destruir este objeto
        StartCoroutine(DestroyObjectAfterDelay(destroyDelay));
    }

    private IEnumerator DestroyObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Desactiva el objeto que se activó
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }

        // Destruye este objeto después del tiempo especificado
        Destroy(gameObject);
    }
}

