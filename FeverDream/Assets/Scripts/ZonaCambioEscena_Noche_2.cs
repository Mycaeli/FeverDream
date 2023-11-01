using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaCambioEscena_Noche_2 : MonoBehaviour
{
    public int escenaSiguienteIndex; // Nombre de la siguiente escena
    private MeshCollider meshCollider;

    private void Start()
    {
        // Obtener el Mesh Collider adjunto al GameObject
        meshCollider = GetComponent<MeshCollider>();

        // Asegurarse de que el Mesh Collider tiene "Is Trigger" habilitado
        meshCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(escenaSiguienteIndex);
        }
    }
}

