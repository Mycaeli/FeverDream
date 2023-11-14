using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform center;
    public float distance = 5f;
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 30f;
    public GameObject objectToCollide;

    private float initialAngle;
    private bool reverseRotation = false;
    private bool isBlack = false;
    private Collider myCollider;

    public float timer = 0f;
    private bool isCounting = false;

    public Door door;
    public GameObject door2;
    private GameObject pickLock;
    public GameObject objectsToDisable;
    public GameObject objectsAnuncio;

    public AudioSource audioSource;
    public AudioClip audioClip;
    private bool opened;

    private Animator animator; // Agregada referencia al Animator

    void Start()
    {
        myCollider = GetComponent<Collider>();

        Vector3 dirToCenter = center.position - transform.position;
        initialAngle = Mathf.Atan2(dirToCenter.x, dirToCenter.z) * Mathf.Rad2Deg;

        reverseRotation = true;
        opened = false;

        animator = GetComponent<Animator>(); // Obtén la referencia al Animator al inicio
    }

    void Update()
    {
        if (isBlack)
        {
            return;
        }

        float currentAngle = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;

        if (Mathf.Abs(currentAngle) >= maxRotationAngle)
        {
            reverseRotation = !reverseRotation;
        }

        if (reverseRotation)
        {
            currentAngle = -currentAngle;
        }

        float angleInRadians = (initialAngle + currentAngle) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angleInRadians) * distance, Mathf.Sin(angleInRadians) * distance, 0f);

        transform.position = center.position + offset;

        if (isCounting)
        {
            timer += Time.deltaTime;

            if (timer >= 5f)
            {
                CambiarColorDeObjetosVerde();
                StartCoroutine(DesactivarDespuesDeEsperar());
            }
        }

        // Verifica si la tecla "R" fue presionada y activa el método DesactivarElementos y el Animator
        //if (Input.GetKeyDown(KeyCode.R))
      //  {
        //    StartCoroutine(DesactivarElementos());
        //    ActivateAnimator();
       // }
    }

    private void CambiarColorDeObjetosVerde()
    {
        Invoke("ReproducirAudio", 0.6f);
    }

    private void ReproducirAudio()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }

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
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DesactivarElementos());
    }

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

        if (door != null)
        {
            opened = true;
            objectsToDisable.SetActive(false);
            objectsAnuncio.SetActive(false);
            door2.SetActive(false);
            door.operative = true;
        }

        if (pickLock != null)
        {
            pickLock.SetActive(false);
        }

        isBlack = true;
        Destroy(gameObject);

        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isBlack || other.gameObject.tag != "pick")
        {
            return;
        }

        isCounting = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (isBlack || other.gameObject.tag != "pick")
        {
            return;
        }

        isCounting = false;
        timer = 0f;
    }

    void ActivateAnimator()
    {
        // Activa el trigger "Activate" en el Animator
        if (animator != null)
        {
            animator.SetTrigger("Activate");
        }
    }
}


