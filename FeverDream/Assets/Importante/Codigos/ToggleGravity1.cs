using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGravity1 : MonoBehaviour
{
    private bool isGravityEnabled = false;
    private bool isDisabled = false; // Variable para rastrear si el objeto está desactivado permanentemente
    public GameObject objetoAActivarDesactivar;
    public Rigidbody objeto1Rigidbody; // Referencia al Rigidbody del objeto 1

    private void Start()
    {
        // Asegúrate de que objeto1Rigidbody esté asignado en el Inspector.
        if (objeto1Rigidbody == null)
        {
            Debug.LogError("No se ha asignado el Rigidbody del objeto 1 en el Inspector.");
            enabled = false; // Deshabilita el script si no se asigna el Rigidbody.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivarDesactivar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivarDesactivar.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isDisabled) // Verificar si el objeto no está desactivado permanentemente
        {
            if (isGravityEnabled && Input.GetKeyDown(KeyCode.E))
            {
                objeto1Rigidbody.isKinematic = true;
                objeto1Rigidbody.useGravity = false;
                isGravityEnabled = false;

                // Desactiva permanentemente el objeto
                objetoAActivarDesactivar.SetActive(false);
                isDisabled = true;
            }
            else if (!isGravityEnabled && Input.GetKeyDown(KeyCode.E))
            {
                objeto1Rigidbody.isKinematic = false;
                objeto1Rigidbody.useGravity = true;
                isGravityEnabled = true;
            }
        }
    }
}


