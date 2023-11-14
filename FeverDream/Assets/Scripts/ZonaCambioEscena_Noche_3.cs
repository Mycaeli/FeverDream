using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Reflection;

public class ZonaCambioEscena_Noche_3 : MonoBehaviour
{
    public int escenaSiguienteIndex;
    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga un tag "Player".
        {
            audioSource.Play();

            StartCoroutine(LoadSceneAfterDelay(2.5f)); // Carga la siguiente escena después de 10 sec.
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(escenaSiguienteIndex);
    }
}
