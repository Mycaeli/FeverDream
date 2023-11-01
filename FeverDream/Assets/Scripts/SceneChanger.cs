using UnityEngine;
using UnityEngine.UI; // Add this line to access the UI Image
using UnityEngine.SceneManagement; // Add this line to access scene management

public class SceneChanger : MonoBehaviour, II
{


    public GameObject keyUI; // Reference to the UI image

    private string sceneToLoad = "Start Menu"; // Cambia "NombreDeTuEscena" al nombre de la escena que deseas cargar

    public string interactText = ""; // Cambia este texto según tus necesidades

    void Update()
    {
        // Comprueba si se ha hecho clic izquierdo y si el objeto está lo suficientemente cerca
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact(); // Llama al método Interact cuando se hace clic en el objeto
            }
        }
    }

    public string GetDescription()
    {
        return interactText;
    }

    public void Interact()
    {
        // Activa el objeto a activar y desactiva el aviso
        

        // Activate the UI image when the key is picked up
        if (keyUI != null)
        {
            keyUI.gameObject.SetActive(true);
        }

        // Cambia a la nueva escena después de 1 segundo
        Invoke("ChangeScene", 1f);
    }

    // Método para cambiar de escena
    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
