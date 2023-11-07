using UnityEngine;
using UnityEngine.UI;

public class Door_closeth : MonoBehaviour
{
    private bool isOpen;
    private Quaternion startRotation;
    public Quaternion endRotation; // Make it public so you can set it from the Unity Inspector
    public float rotationSpeed = 90.0f; // Adjust the rotation speed as needed
    public float maxRayLength = 2.0f; // Maximum ray length for interaction
    public float maxDistanceToShowIndicator = 1.5f; // Adjust the distance threshold as needed
    public float rayLength = 2.0f;

    // Public GameObject for the clickable indicator
    public GameObject clickableIndicator;

    private bool isInteractable = false;
    public GameObject firstHitClosetDoor; // Public reference to the first hit closet door

    void Start()
    {
        isOpen = false;
        startRotation = transform.rotation;
        // You can set the default value for endRotation here or in the Inspector
    }

    void Update()
    {
        if (IsMouseOverCloset())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (isOpen)
                {
                    // If the door is open, close it
                    OpenOrCloseDoor();
                }
                else
                {
                    // If the door is closed, open it
                    OpenOrCloseDoor();
                }
            }

            // Show the clickable indicator when the player is near the closet
            ShowClickableIndicator();
        }
        else
        {
            // Hide the clickable indicator if the player is not near the closet
            HideClickableIndicator();
        }

        if (isOpen)
        {
            // Smoothly interpolate between the start and end rotations
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, step);
        }
        else
        {
            // Smoothly interpolate back to the closed position
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, step);
        }
    }

    bool IsMouseOverCloset()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayLength) && hit.collider.CompareTag("Closeth"))
        {
            // If the first hit closet door is not set, store the reference
            if (firstHitClosetDoor == null)
            {
                firstHitClosetDoor = hit.collider.gameObject;
            }

            // Check if the hit object is the same as the first hit closet door
            return hit.collider.gameObject == firstHitClosetDoor;
        }

        return false;
    }

    void OpenOrCloseDoor()
    {
        isOpen = !isOpen;
    }

    // Helper methods for showing and hiding clickable indicator
    private void ShowClickableIndicator()
    {
        if (!isInteractable)
        {
            isInteractable = true;
            clickableIndicator.SetActive(true);
        }
    }

    private void HideClickableIndicator()
    {
        if (isInteractable)
        {
            isInteractable = false;
            clickableIndicator.SetActive(false);
        }
    }
}
