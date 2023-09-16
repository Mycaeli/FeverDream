using UnityEngine;
using UnityEngine.SceneManagement;

public class SalirMaze : MonoBehaviour
{
    public int escenaSiguienteIndex;

    private void OnMouseDown()
    {
        CambiarEscena();
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(escenaSiguienteIndex);
    }
}
