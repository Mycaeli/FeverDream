using UnityEngine;

public class PausaMenu : MonoBehaviour
{
    public GameObject pausaMenuUI;
    private bool estaPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        pausaMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo en el juego
        estaPausado = true;
    }

    public void ReanudarJuego()
    {
        pausaMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo en el juego
        estaPausado = false;
    }
}

