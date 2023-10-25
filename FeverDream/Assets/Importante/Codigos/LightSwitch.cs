using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, II
{
    public List<Light> targetLights; // Lista de luces a apagar o encender
    public bool areLightsOn = false; // Inicializa las luces apagadas
    public string texto1 = "Apagar";
    public string texto2 = "Encender";

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
        // Cambia el estado de las luces y actualiza su descripción
        areLightsOn = !areLightsOn;

        foreach (var light in targetLights)
        {
            light.enabled = areLightsOn;
        }

        // Destruye el objeto después de la interacción
        Destroy(gameObject);
    }
}
