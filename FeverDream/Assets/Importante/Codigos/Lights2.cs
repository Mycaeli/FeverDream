using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights2 : MonoBehaviour, II
{
    public Light targetLight; // Referencia a la luz que se va a apagar
    public bool isLightOn = false; // Inicialmente apagada
    public string texto1 = "Encender Linterna";
    public string texto2 = "Apagar Linterna";

    private bool isObjectHidden = false; // Controla si el objeto est� oculto

    void Start()
    {
        // Asegura que la luz est� apagada al inicio
        targetLight.enabled = isLightOn;
    }

    void Update()
    {
        // Comprueba si se ha hecho clic izquierdo y si el objeto est� lo suficientemente cerca
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact(); // Llama al m�todo Interact cuando se hace clic en la linterna
            }
        }

        // Verifica si se presiona la tecla "L" para cambiar el estado de la luz
        if (Input.GetKeyDown(KeyCode.L))
        {
            isLightOn = !isLightOn;
            targetLight.enabled = isLightOn;
        }
    }

    public string GetDescription()
    {
        if (isLightOn)
        {
            return texto2;
        }
        else
        {
            return texto1;
        }
    }

    public void Interact()
    {
        // Cambia el estado de la luz y actualiza su descripci�n
        isLightOn = !isLightOn;
        targetLight.enabled = isLightOn;

        // Cambia el estado del objeto
        isObjectHidden = !isObjectHidden;

        if (isObjectHidden)
        {
            // Mueve el objeto debajo del suelo (ajusta la posici�n Y seg�n tu escenario)
            Vector3 newPosition = transform.position;
            newPosition.y = -10f; // Ajusta esta posici�n a la profundidad deseada
            transform.position = newPosition;
        }
        else
        {
            // Devuelve el objeto a su posici�n original (ajusta la posici�n Y)
            Vector3 newPosition = transform.position;
            newPosition.y = 0f; // Ajusta esta posici�n a la altura original
            transform.position = newPosition;
        }
    }
}

