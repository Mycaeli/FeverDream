using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Reflection;

public class ZonaCambioEscena_Noche_3 : MonoBehaviour
{
    public int escenaSiguienteIndex;
    public AudioClip audioClip;
    private AudioSource audioSource;
    public GameObject finTxt;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
        finTxt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga un tag "Player".
        {
            audioSource.Play();
            finTxt.SetActive(true);
            StartCoroutine(LoadSceneAfterDelay(10.0f)); // Carga la siguiente escena después de 10 sec.
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        finTxt.SetActive(false);
        SceneManager.LoadScene(escenaSiguienteIndex);
    }
}
