using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{
    public GameObject objectToActivate; // Arrastra el objeto a activar desde el Inspector
    public GameObject objectToShowFor5Seconds;
    private bool isPlayerInsideTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = true;
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = false;
            objectToActivate.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            objectToActivate.SetActive(false);
            objectToShowFor5Seconds.SetActive(true);

            StartCoroutine(DisableObjectsAfterDelay(2.0f));
        }
    }

    private IEnumerator DisableObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        objectToActivate.SetActive(true);
        objectToShowFor5Seconds.SetActive(false);
    }
}


