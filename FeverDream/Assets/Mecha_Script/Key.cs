using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject objectToActivate;// Referencia al GameObject que se activará al recoger la llave
    public GameObject Decoy;
    public GameObject Aviso;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        {
            // Activa el GameObject cuando el jugador recoja la llave
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                Decoy.SetActive(false);
                Aviso.SetActive(false);
            }

            // Desactiva y luego destruye la llave de la escena
            gameObject.SetActive(false);
            Destroy(gameObject, 1f); // Desactiva después de 1 segundo
        }
    }
}


