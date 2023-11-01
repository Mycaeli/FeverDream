using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, II
{
    public List<Light> targetLights;
    public bool areLightsOn = false;
    public string texto1 = "Turn Off";
    public string texto2 = "Turn On";
    public Material lampMaterial;
    public AudioSource interactAudioSource; // Referencia al AudioSource

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Interact();
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
        areLightsOn = !areLightsOn;

        foreach (var light in targetLights)
        {
            light.enabled = areLightsOn;
        }

        if (lampMaterial != null)
        {
            if (areLightsOn)
            {
                lampMaterial.EnableKeyword("_EMISSION");
                lampMaterial.SetColor("_EmissionColor", Color.white);
            }
            else
            {
                lampMaterial.DisableKeyword("_EMISSION");
                lampMaterial.SetColor("_EmissionColor", Color.black);
            }
        }

        // Reproduce el audio al interactuar
        if (interactAudioSource != null)
        {
            interactAudioSource.enabled = true; // Asegúrate de que el AudioSource esté habilitado
            interactAudioSource.Play();
        }

        Destroy(gameObject);
    }
}



