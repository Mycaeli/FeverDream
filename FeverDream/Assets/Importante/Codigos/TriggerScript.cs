using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    // Define el tag del objeto que debe tocar para activar la acci�n.
    public string pickableTag = "Pickable";

    // Lista de objetos que se apagar�n.
    public GameObject[] objectsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entr� en el Trigger tiene el tag "Pickable".
        if (other.CompareTag(pickableTag))
        {
            // Itera a trav�s de los objetos que se deben apagar y los apaga.
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }
}
