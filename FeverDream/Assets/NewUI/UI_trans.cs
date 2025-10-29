using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_trans : MonoBehaviour
{
    // Asigna el Animator desde el Inspector
    public Animator animator;
    public GameObject Menu;
    public GameObject controls;

    [Header("Hyperlink Settings")]
    public string url = "https://www.google.com";

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        animator.SetTrigger("MoveOut");
        Menu.SetActive(true);
        controls.SetActive(false);
    }

    // 🔹 Activa el trigger MoveIn del Animator
    public void MoveIn()
    {
        if (animator != null)
        {
            animator.SetTrigger("MoveIn");
            Menu.SetActive(false);
            controls.SetActive(true);

        }
    }

    // 🔹 Activa el trigger MoveOut del Animator
    public void MoveOut()
    {
        if (animator != null)
        {
            animator.SetTrigger("MoveOut");
            Cursor.lockState = CursorLockMode.None;
            Menu.SetActive(true);
            controls.SetActive(false);
        }
    }

    // 🔗 Abre un hipervínculo en el navegador
    public void Hyperlink()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("No se ha asignado una URL al hipervínculo.");
        }
    }

    // 🔁 Reinicia el nivel actual
    public void RestartLevel()
    {
        // Obtiene el nombre de la escena actual y la recarga
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
