using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PausaMenu : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject pausaMenuUI;           // Panel del menú de pausa
    public GameObject botonPorDefecto;       // Botón seleccionado al pausar (opcional)

    [Tooltip("Si true, al reanudar se oculta y bloquea el cursor. Si false, el cursor queda visible.")]
    public bool hideCursorOnPlay = true;

    private bool estaPausado = false;

    void Start()
    {
        // Asegura que exista un EventSystem para navegación UI
        if (EventSystem.current == null)
        {
            GameObject es = new GameObject("EventSystem", typeof(EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
            Debug.LogWarning("No se encontró EventSystem. Se creó uno automáticamente.");
        }

        // Estado inicial del cursor según la preferencia
        if (hideCursorOnPlay)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Asegura que el panel de pausa comience desactivado
        if (pausaMenuUI != null) pausaMenuUI.SetActive(false);

        Debug.Log($"[PausaMenu] Start - hideCursorOnPlay={hideCursorOnPlay}, lockState={Cursor.lockState}, visible={Cursor.visible}");
    }

    void Update()
    {
        // Detecta tecla Escape para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Método público para alternar pausa (útil para pruebas y botones)
    public void TogglePause()
    {
        if (estaPausado) ReanudarJuego();
        else PausarJuego();
    }

    public void PausarJuego()
    {
        if (pausaMenuUI == null)
        {
            Debug.LogError("[PausaMenu] pausaMenuUI no asignado en el inspector.");
            return;
        }

        pausaMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo del juego
        estaPausado = true;

        // Primero liberar el lock, luego mostrar el cursor (orden importante)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Seleccionar botón por defecto para teclado/mando
        if (botonPorDefecto != null && EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(botonPorDefecto);
        }

        Debug.Log($"[PausaMenu] Pausado - lockState={Cursor.lockState}, visible={Cursor.visible}, Time.timeScale={Time.timeScale}");
    }

    public void ReanudarJuego()
    {
        if (pausaMenuUI == null)
        {
            Debug.LogError("[PausaMenu] pausaMenuUI no asignado en el inspector.");
            return;
        }

        pausaMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo
        estaPausado = false;

        if (hideCursorOnPlay)
        {
            // Primero bloquear, luego ocultar (mejor compatibilidad)
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Debug.Log($"[PausaMenu] Reanudado - lockState={Cursor.lockState}, visible={Cursor.visible}, Time.timeScale={Time.timeScale}");
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }

    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal"); // Cambia el nombre por el de tu escena principal
    }

    public void SalirDelJuego()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("El juego se ha cerrado (solo visible en el editor).");
    }
}
