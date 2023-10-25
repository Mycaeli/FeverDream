using UnityEngine;
using System.Collections;

public class Trigger2 : MonoBehaviour
{
    public GameObject objectToActivate; // Arrastra el objeto a activar desde el Inspector
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


}

