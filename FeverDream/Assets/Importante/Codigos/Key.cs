using UnityEngine;

public class Key : MonoBehaviour, II
{
    public GameObject objectToActivate;
    public GameObject Aviso;
    public GameObject objectToDeactivate;

    public string GetDescription()
    {
        return "Tomar llave.";
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
                objectToDeactivate.SetActive(false);
            }
        }

        // Desactiva y luego destruye la llave de la escena después de 1 segundo
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}




