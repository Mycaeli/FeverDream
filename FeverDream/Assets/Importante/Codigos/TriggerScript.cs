using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    // Define el tag del objeto que debe tocar para activar la acción.
    public string pickableTag = "Pickable";

    // Lista de objetos que se apagarán.
    public GameObject[] objectsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró en el Trigger tiene el tag "Pickable".
        if (other.CompareTag(pickableTag))
        {
            // Itera a través de los objetos que se deben apagar y los apaga.
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }
}
