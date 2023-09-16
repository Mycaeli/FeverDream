using UnityEngine;

public class Mensaje : MonoBehaviour
{
    public string message = "Puerta cerrada"; // Mensaje que se va a mostrar
    private bool enteredTrigger = false;

    private void Update()
    {
        // No se necesita c�digo en Update en este caso.
    }

    private void OnGUI()
    {
        if (enteredTrigger)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 30), message);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de ajustar la etiqueta del objeto que activar� el trigger seg�n tus necesidades
        {
            enteredTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de ajustar la etiqueta del objeto que activar� el trigger seg�n tus necesidades
        {
            enteredTrigger = false;
        }
    }
}

