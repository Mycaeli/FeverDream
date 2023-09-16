using System.Collections;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public Camera camera;
    public GameObject Lock;
    public GameObject pickLock; // Referencia al objeto "Pick Lock"

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Lock)
        {
            // Activar el objeto "Pick Lock" cuando el jugador entre en el collider.
            pickLock.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Lock)
        {
            // Desactivar el objeto "Pick Lock" cuando el jugador salga del collider.
            pickLock.SetActive(false);
        }
    }

}
