using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGravity1 : MonoBehaviour
{
    private bool isGravityEnabled = false;
    private bool isDisabled = false;
    public GameObject objetoAActivarDesactivar;
    public GameObject correspondingObject; // Reference to the corresponding object

    private Rigidbody objetoRigidbody; // Reference to the Rigidbody of the game object

    private void Start()
    {
        objetoRigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component from the game object

        if (objetoRigidbody == null)
        {
            Debug.LogError("No se ha encontrado el componente Rigidbody en el objeto.");
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (correspondingObject != null && gameObject == correspondingObject)
            {
                objetoAActivarDesactivar.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (correspondingObject != null && gameObject == correspondingObject)
            {
                objetoAActivarDesactivar.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (objetoAActivarDesactivar != null)
        {
            if (!isDisabled)
            {
                if (isGravityEnabled && Input.GetKeyDown(KeyCode.E))
                {
                    objetoRigidbody.isKinematic = true;
                    objetoRigidbody.useGravity = false;
                    isGravityEnabled = false;

                    if (correspondingObject != null && gameObject == correspondingObject)
                    {
                        objetoAActivarDesactivar.SetActive(false);
                    }
                }
                else if (!isGravityEnabled && Input.GetKeyDown(KeyCode.E))
                {
                    objetoRigidbody.isKinematic = false;
                    objetoRigidbody.useGravity = true;
                    isGravityEnabled = true;
                }
            }
        }
    }
}



