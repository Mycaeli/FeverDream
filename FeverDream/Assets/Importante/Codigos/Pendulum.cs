using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform center; // Referencia al objeto central alrededor del cual gira
    public float distance = 5f; // Distancia entre el objeto y el centro (variable p�blica)
    public float rotationSpeed = 30f; // Velocidad de rotaci�n (variable p�blica)
    public float maxRotationAngle = 30f; // �ngulo m�ximo de rotaci�n hacia arriba y hacia abajo (variable p�blica)
    public GameObject objectToCollide; // GameObject con el que se va a colisionar (variable p�blica)

    private float initialAngle;
    private bool reverseRotation = false;
    private bool isBlack = false; // Flag para verificar si el objeto ya es negro
    private Collider myCollider; // Referencia al componente Collider

    public float timer = 0f;
    private bool isCounting = false;

    public GameObject door; // Referencia al objeto "door"
    private GameObject pickLock; // Referencia al objeto "pick lock"
    public GameObject objectsToDisable;
    public GameObject objectsAnuncio;

    public AudioSource audioSource; // Asigna el objeto AudioSource en el Inspector
    public AudioClip audioClip; // Asigna el audio que deseas reproducir en el Inspector


    void Start()
    {
        // Obt�n una referencia al componente Collider
        myCollider = GetComponent<Collider>();

        // Calcula el �ngulo inicial basado en la posici�n inicial con respecto al centro
        Vector3 dirToCenter = center.position - transform.position;
        initialAngle = Mathf.Atan2(dirToCenter.z, dirToCenter.y) * Mathf.Rad2Deg;

        // Invierte la direcci�n de rotaci�n al inicio
        reverseRotation = true;

        
    }

    void Update()
    {
        // Si el objeto ya es negro, no realizar m�s cambios
        if (isBlack)
        {
            return;
        }

        // Calcula el �ngulo actual basado en el tiempo y la velocidad de rotaci�n
        float currentAngle = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;

        if (Mathf.Abs(currentAngle) >= maxRotationAngle)
        {
            // Cambia la direcci�n de rotaci�n al llegar al l�mite del �ngulo
            reverseRotation = !reverseRotation;
        }

        if (reverseRotation)
        {
            // Invierte la direcci�n de rotaci�n
            currentAngle = -currentAngle;
        }

        // Calcula la nueva posici�n del objeto en el eje X
        float angleInRadians = (initialAngle + currentAngle) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(0f, Mathf.Sin(angleInRadians) * distance, Mathf.Cos(angleInRadians) * distance);

        // Establece la posici�n del objeto basado en el centro y el offset
        transform.position = center.position + offset;

        // Si est� en contacto con un objeto con el tag "pick", comienza a contar el tiempo
        if (isCounting)
        {
            timer += Time.deltaTime;
            

            // Si el contador llega a 5 segundos, cambia el color del objeto con el tag "verde" a verde
            if (timer >= 5f)
            {
                CambiarColorDeObjetosVerde();
                // Espera 1 segundo antes de desactivar los elementos "door" y "pick lock"
                StartCoroutine(DesactivarDespuesDeEsperar());
                
            }
        }
    }

    private void CambiarColorDeObjetosVerde()
    {
        // Programa la reproducci�n del audio despu�s de 1 segundo
        Invoke("ReproducirAudio", 0.6f);
    }

    private void ReproducirAudio()
    {
        // Reproduce el audio
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip); // Reproduce el audio una vez
        }

        // Encuentra y cambia el color de los objetos con la etiqueta "verde" a verde
        GameObject[] greenObjects = GameObject.FindGameObjectsWithTag("verde");
        foreach (GameObject greenObject in greenObjects)
        {
            Renderer renderer = greenObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }
        }
    }




    private IEnumerator DesactivarDespuesDeEsperar()
    {
        // Espera 1 segundo
        yield return new WaitForSeconds(0.1f);

        // Ahora puedes desactivar los elementos
        StartCoroutine(DesactivarElementos());
    }
    // M�todo para desactivar elementos despu�s de esperar 1 segundo
    private IEnumerator DesactivarElementos()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject[] greenObjects = GameObject.FindGameObjectsWithTag("verde");
        foreach (GameObject greenObject in greenObjects)
        {
            Renderer renderer = greenObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }
        }

        // Desactiva el objeto "door" si existe
        if (door != null)
        {
            door.SetActive(false);
            objectsToDisable.SetActive(false);
            objectsAnuncio.SetActive(false);
        }

        // Desactiva el objeto "pick lock" si existe
        if (pickLock != null)
        {
            pickLock.SetActive(false);
        }

        // Destruye este objeto despu�s de desactivar los elementos
        isBlack = true;
        Destroy(gameObject);

        // Aseg�rate de que la funci�n siempre termine con una sentencia de retorno
        yield return null;
    }


    // M�todo que se llama cuando el objeto entra en contacto con un trigger
    void OnTriggerEnter(Collider other)
    {
        // Si el objeto ya es negro o no es el objeto con el que queremos colisionar, no realizar m�s cambios
        if (isBlack || other.gameObject.tag != "pick")
        {
            return;
        }

        // Comienza a contar el tiempo cuando entra en contacto con un objeto con el tag "pick"
        isCounting = true;
    }

    // M�todo que se llama cuando el objeto sale del trigger
    void OnTriggerExit(Collider other)
    {
        // Si el objeto ya es negro o no es el objeto con el que queremos colisionar, no realizar m�s cambios
        if (isBlack || other.gameObject.tag != "pick")
        {
            return;
        }

        // Reinicia el contador cuando sale del contacto con un objeto con el tag "pick"
        isCounting = false;
        timer = 0f;
    }

  
}


