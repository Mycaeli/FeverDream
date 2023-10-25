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
            objectToDeactivate.SetActive(false);
        }

        // Desactiva y luego destruye la llave de la escena después de 1 segundo
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}



