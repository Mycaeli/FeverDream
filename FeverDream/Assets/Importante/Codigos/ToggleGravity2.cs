using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ToggleGravity2 : MonoBehaviour
{
    private bool isGravityEnabled = false;
    public GameObject objetoAActivarDesactivar;
    public Rigidbody objeto1Rigidbody; // Referencia al Rigidbody del objeto 1
    private bool canInteract = false;
    public GameObject flowchart; // Reference to the Fungus flowchart game object
    public GameObject cable;

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
            canInteract = true; // Allow interaction when the player is near.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivarDesactivar.SetActive(false);
            canInteract = false; // Prevent interaction when the player leaves.
        }
    }

    private void Update()
    {
        if (canInteract)
        {
            if (isGravityEnabled && Input.GetKeyDown(KeyCode.E))
            {
                objeto1Rigidbody.isKinematic = true;
                objeto1Rigidbody.useGravity = false;
                isGravityEnabled = false;

                // Desplaza el objeto hacia abajo 20 unidades en el eje Y.
                gameObject.SetActive(false);
                Destroy(gameObject, 1f);

                if (cable.CompareTag("Cable"))
                {
                    TriggerFungusBlock();
                }
            }
            else if (!isGravityEnabled && Input.GetKeyDown(KeyCode.E))
            {
                objeto1Rigidbody.isKinematic = false;
                objeto1Rigidbody.useGravity = true;
                isGravityEnabled = true;

                if (cable.CompareTag("Cable"))
                {
                    TriggerFungusBlock();
                }
            }
        }
    }

    private void TriggerFungusBlock()
    {
        // Set the flowchart game object to active
        flowchart.SetActive(true);
    }
}



