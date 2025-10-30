using System.Collections;
using UnityEngine;
using TMPro;

public class Pendulum2 : MonoBehaviour
{
    [Header("Movimiento del péndulo")]
    public Transform center;
    public float distance = 5f;
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 30f;

    [Header("Elementos del juego")]
    public GameObject door;
    public GameObject Phone;
    public GameObject objectsToDisable;
    public GameObject objectsAnuncio;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    [Header("UI")]
    public TMP_Text timerText; // ← TextMeshPro para mostrar el temporizador

    private float initialAngle;
    private bool reverseRotation = false;
    private bool isBlack = false;
    private bool isCounting = false;
    private float timer = 0f;
    private bool opened = false;
    private Collider myCollider;
    private GameObject pickLock;

    void Start()
    {
        myCollider = GetComponent<Collider>();

        Vector3 dirToCenter = center.position - transform.position;
        initialAngle = Mathf.Atan2(dirToCenter.x, dirToCenter.z) * Mathf.Rad2Deg;

        reverseRotation = true;
        opened = false;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (timerText != null)
            timerText.text = "0.0 / 5.0"; // valor inicial
    }

    void Update()
    {
        if (isBlack) return;

        // Movimiento oscilante del péndulo
        float currentAngle = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;

        if (Mathf.Abs(currentAngle) >= maxRotationAngle)
            reverseRotation = !reverseRotation;

        if (reverseRotation)
            currentAngle = -currentAngle;

        float angleInRadians = (initialAngle + currentAngle) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angleInRadians) * distance, Mathf.Sin(angleInRadians) * distance, 0f);
        transform.position = center.position + offset;

        // Conteo del temporizador
        if (isCounting)
        {
            timer += Time.deltaTime;

            if (timerText != null)
                timerText.text = $"{timer:F1} / 5.0";

            if (timer >= 5f)
            {
                CambiarColorDeObjetosVerde();
                StartCoroutine(DesactivarDespuesDeEsperar());
            }
        }
        else
        {
            if (timerText != null)
                timerText.text = "0.0 / 5.0";
        }

        // Activar con tecla "R"
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(DesactivarElementos());
            ActivateAnimator();
        }
    }

    private void CambiarColorDeObjetosVerde()
    {
        Invoke("ReproducirAudio", 0.6f);
    }

    private void ReproducirAudio()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(audioClip);

        GameObject[] greenObjects = GameObject.FindGameObjectsWithTag("verde");
        foreach (GameObject greenObject in greenObjects)
        {
            Renderer renderer = greenObject.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material.color = Color.green;
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
                renderer.material.color = Color.green;
        }

        if (door != null)
        {
            door.SetActive(true);
            Phone.SetActive(true);
            opened = true;
            objectsToDisable.SetActive(false);
            objectsAnuncio.SetActive(false);
        }

        if (pickLock != null)
            pickLock.SetActive(false);

        if (timerText != null)
        {
            timerText.text = "¡Completado!";
            timerText.color = Color.green;
        }

        isBlack = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isBlack || other.gameObject.tag != "pick")
            return;

        isCounting = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (isBlack || other.gameObject.tag != "pick")
            return;

        isCounting = false;
        timer = 0f;
    }

    void ActivateAnimator()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Activate");
    }
}

