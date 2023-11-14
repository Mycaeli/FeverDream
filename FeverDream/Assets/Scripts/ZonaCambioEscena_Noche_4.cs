using UnityEngine;

public class EmergenceTrigger : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}