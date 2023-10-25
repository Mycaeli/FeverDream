using UnityEngine;

public class Activar : MonoBehaviour
{
    public GameObject objetoAActivarDesactivar;

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
}

