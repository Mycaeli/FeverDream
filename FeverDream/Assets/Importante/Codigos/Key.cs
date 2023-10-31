using UnityEngine;
using UnityEngine.UI; // Add this line to access the UI Image

public class Key : MonoBehaviour, II
{
    public GameObject objectToActivate;
    public GameObject Aviso;
    public GameObject objectToDeactivate;

    public Image keyUI; // Reference to the UI image

    public string GetDescription()
    {
        return "Tomar llave.";
    }

    void Update()
    {
        // Comprueba si se ha hecho clic izquierdo y si el objeto est� lo suficientemente cerca
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact(); // Llama al m�todo Interact cuando se hace clic en la llave
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
                objectToDeactivate.SetActive(false);
            }
        }

        // Activate the UI image when the key is picked up
        if (keyUI != null)
        {
            keyUI.gameObject.SetActive(true);
        }

        // Desactiva y luego destruye la llave de la escena despu�s de 1 segundo
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}
