using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ToggleGravity2 : MonoBehaviour
{
    private bool isGravityEnabled = false;
    public GameObject objetoAActivarDesactivar;
    public Rigidbody objeto1Rigidbody;
    private bool canInteract = false;
    public GameObject flowchart;
    public GameObject cable;

    private Transform playerTransform; // referencia al jugador
    public float maxInteractionDistance = 3f; // distancia máxima para mantener la UI activa

    private void Start()
    {
        if (objeto1Rigidbody == null)
        {
            Debug.LogError("No se ha asignado el Rigidbody del objeto 1 en el Inspector.");
            enabled = false;
            return;
        }

        // Aseguramos que la UI comience desactivada
        if (objetoAActivarDesactivar != null)
            objetoAActivarDesactivar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            MostrarUI(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OcultarUI();
            canInteract = false;
            playerTransform = null;
        }
    }

    private void Update()
    {
        // Si el jugador ya no está cerca, ocultar la UI (aunque OnTriggerExit no se haya llamado)
        if (canInteract && playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance > maxInteractionDistance)
            {
                OcultarUI();
                canInteract = false;
                playerTransform = null;
                return;
            }
        }

        if (canInteract && Input.GetMouseButtonDown(0))
        {
            if (isGravityEnabled)
            {
                objeto1Rigidbody.isKinematic = true;
                objeto1Rigidbody.useGravity = false;
                isGravityEnabled = false;

                gameObject.SetActive(false);
                Destroy(gameObject, 1f);

                if (cable != null && cable.CompareTag("Cable"))
                    TriggerFungusBlock();
            }
            else
            {
                objeto1Rigidbody.isKinematic = false;
                objeto1Rigidbody.useGravity = true;
                isGravityEnabled = true;

                if (cable != null && cable.CompareTag("Cable"))
                    TriggerFungusBlock();
            }
        }
    }

    private void MostrarUI(bool estado)
    {
        if (objetoAActivarDesactivar != null)
            objetoAActivarDesactivar.SetActive(estado);
    }

    private void OcultarUI()
    {
        if (objetoAActivarDesactivar != null)
            objetoAActivarDesactivar.SetActive(false);
    }

    private void TriggerFungusBlock()
    {
        if (flowchart != null)
            flowchart.SetActive(true);
    }
}




