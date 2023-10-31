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
    public bool hasBeenInteracted = false; // Flag to track if the object has been interacted with

    void Start()
    {
        // Asegura que la luz esté apagada al inicio
        targetLight.enabled = isLightOn;
    }

    void Update()
    {
        // Check if the object has been interacted with before allowing "L" key to toggle the light
        if (hasBeenInteracted && Input.GetKeyDown(KeyCode.L))
        {
            isLightOn = !isLightOn;
            targetLight.enabled = isLightOn;
        }

        // Check for left mouse click and object proximity
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact(); // Call the Interact method when clicking on the flashlight
            }
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
        // Change the state of the light and update its description
        isLightOn = !isLightOn;
        targetLight.enabled = isLightOn;

        // Change the state of the object
        isObjectHidden = !isObjectHidden;

        if (isObjectHidden)
        {
            // Move the object below the ground (adjust the Y position based on your scenario)
            Vector3 newPosition = transform.position;
            newPosition.y = -10f; // Adjust this position to the desired depth
            transform.position = newPosition;

            // Set the flag to indicate that the object has been interacted with
            hasBeenInteracted = true;
        }
        else
        {
            // Return the object to its original position (adjust the Y position)
            Vector3 newPosition = transform.position;
            newPosition.y = 0f; // Adjust this position to the original height
            transform.position = newPosition;
        }
    }
}

