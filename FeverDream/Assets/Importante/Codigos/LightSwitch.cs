﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSwitch : MonoBehaviour, II
{
    public List<Light> targetLights; // List of lights to turn on or off
    public Object lamp;
    public bool areLightsOn = false; // Initialize lights as off
    public string texto1 = "Turn Off";
    public string texto2 = "Turn On";
    public Material sharedLightMaterial; // Declare the shared material variable

    //private Renderer renderer; // Reference to the Renderer component

    void Update()
    {
        // Comprueba si se ha hecho clic izquierdo y si el objeto está lo suficientemente cerca
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact(); // Llama al método Interact cuando se hace clic en el interruptor
            }
        }
    }

    public string GetDescription()
    {
        if (areLightsOn)
        {
            return texto1;
        }
        else
        {
            return texto2;
        }
    }

    public void Interact()
    {
        // Change the state of the lights and update the description
        areLightsOn = !areLightsOn;

        foreach (var light in targetLights)
        {
            light.enabled = areLightsOn;
        }

            Renderer lampRenderer = lamp.GetComponent<Renderer>();

            // Check if the lamp has a Renderer component
            if (lampRenderer != null)
            {
                Material lampMaterial = lampRenderer.sharedMaterial;
                lampMaterial.DisableKeyword("_EMISSION");
                lampMaterial.SetColor("_EmissionColor", Color.black);

            }

        // Destroy the object after interaction
        Destroy(gameObject);
    }

}

