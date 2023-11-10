using UnityEngine;
using UnityEngine.UI; // Add this line to access the UI Image

public class Key : MonoBehaviour, II
{
    public GameObject objectToActivate;
    public GameObject Aviso;
    public GameObject objectToDeactivate;
    public AudioSource interactAudioSource;

    public Image keyUI;
    public string texto = "tomar llave";// Reference to the UI image

    public string GetDescription()
    {
        return texto;
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
        // Activa el objeto a activar y desactiva el aviso
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        if (Aviso != null)
        {
            Aviso.SetActive(false);
            if (objectToDeactivate != null)
            {
                Aviso.SetActive(false);
                objectToDeactivate.SetActive(false);
            }
        }

        // Activate the UI image when the key is picked up
        if (keyUI != null)
        {
            keyUI.gameObject.SetActive(true);
        }

        // Desactiva y luego destruye la llave de la escena después de 1 segundo
        interactAudioSource.Play();
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}
