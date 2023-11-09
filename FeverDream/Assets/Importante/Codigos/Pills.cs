using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Pills : MonoBehaviour, II
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public GameObject Teleport;
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
                Teleport.SetActive(false);
            }
            return "Activar el objeto.";
        }
    }

    void Update()
    {
        // Comprueba si se ha hecho clic izquierdo y si el objeto está lo suficientemente cerca
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact(); // Llama al método Interact cuando se hace clic en la llave
            }
        }
    }

    public void Interact()
    {
        // Activa el objeto a activar
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Teleport.SetActive(true);
        }

        // Desactiva y destruye el objeto a desactivar
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(true);
            Teleport.SetActive(true);
            
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
            objectToActivate.SetActive(true);
            Teleport.SetActive(true);
        }

        // Destruye este objeto después del tiempo especificado
        Destroy(gameObject);
    }
}

