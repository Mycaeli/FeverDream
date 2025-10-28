using UnityEngine;

public class AnimatorButtonController : MonoBehaviour
{
    // Asigna el Animator desde el Inspector
    public Animator animator;
    public GameObject MenuUI;
    public GameObject ControlUI;

    [Header("Hyperlink Settings")]
    public string url = "https://www.google.com";

    public void Start()
    {
        // Mostrar el cursor del mouse al iniciar
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        MenuUI.SetActive(true);
        ControlUI.SetActive(false);
    }

    // Métodos públicos para conectar con botones UI
    public void MoveIn()
    {
        if (animator != null)
        {
            animator.SetTrigger("MoveIn");
            MenuUI.SetActive(false);
            ControlUI.SetActive(true);
        }
    }

    public void MoveOut()
    {
        if (animator != null)
        {
            animator.SetTrigger("MoveOut");
            MenuUI.SetActive(true);
            ControlUI.SetActive(false);
        }
    }

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
}

