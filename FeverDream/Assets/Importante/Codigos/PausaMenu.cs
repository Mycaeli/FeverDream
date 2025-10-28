using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaMenu : MonoBehaviour
{
    public GameObject pausaMenuUI;
    private bool estaPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    public void PausarJuego()
    {
        pausaMenuUI.SetActive(true);
        Time.timeScale = 0f; // ⏸️ Congela todo el juego
        estaPausado = true;

        // Mostrar el cursor (opcional)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReanudarJuego()
    {
        pausaMenuUI.SetActive(false);
        Time.timeScale = 1f; // ▶️ Reanuda el juego
        estaPausado = false;

        // Ocultar el cursor (opcional)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté activo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


