using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights2 : MonoBehaviour, II
{
    public Light targetLight; // Referencia a la luz que se va a apagar
    public bool isLightOn = false; // Inicialmente apagada
    public string texto1 = "Encender Linterna";
    public string texto2 = "Apagar Linterna";

    private bool isObjectHidden = false; // Controla si el objeto está oculto

    void Start()
    {
        // Asegura que la luz esté apagada al inicio
        targetLight.enabled = isLightOn;
    }

    void Update()
    {
        // Comprueba si se ha hecho clic izquierdo y si el objeto está lo suficientemente cerca
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact(); // Llama al método Interact cuando se hace clic en la linterna
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
        // Cambia el estado de la luz y actualiza su descripción
        isLightOn = !isLightOn;
        targetLight.enabled = isLightOn;

        // Cambia el estado del objeto
        isObjectHidden = !isObjectHidden;

        if (isObjectHidden)
        {
            // Mueve el objeto debajo del suelo (ajusta la posición Y según tu escenario)
            Vector3 newPosition = transform.position;
            newPosition.y = -10f; // Ajusta esta posición a la profundidad deseada
            transform.position = newPosition;
        }
        else
        {
            // Devuelve el objeto a su posición original (ajusta la posición Y)
            Vector3 newPosition = transform.position;
            newPosition.y = 0f; // Ajusta esta posición a la altura original
            transform.position = newPosition;
        }
    }
}

