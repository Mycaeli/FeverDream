using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to access the UI Image

public class Lights2 : MonoBehaviour, II
{
    public Light targetLight;
    public bool isLightOn = false;
    public string texto1 = "Encender Linterna";
    public string texto2 = "Apagar Linterna";

    private bool isObjectHidden = false;
    public bool hasBeenInteracted = false;
    public AudioSource interactAudioSource;

    public Image flashlightUI; // Reference to the UI image

    void Start()
    {
        targetLight.enabled = isLightOn;
    }

    void Update()
    {
        if (hasBeenInteracted && Input.GetKeyDown(KeyCode.Alpha1))
        {
            isLightOn = !isLightOn;
            targetLight.enabled = isLightOn;
            interactAudioSource.Play();
        }

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
        isLightOn = !isLightOn;
        targetLight.enabled = isLightOn;

        isObjectHidden = !isObjectHidden;

        if (isObjectHidden)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = -10f;
            transform.position = newPosition;

            hasBeenInteracted = true;

            // Activate the UI image when the flashlight is picked up
            flashlightUI.gameObject.SetActive(true);
        }
        else
        {
            Vector3 newPosition = transform.position;
            newPosition.y = 0f;
            transform.position = newPosition;
        }
    }
}


