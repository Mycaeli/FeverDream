using UnityEngine;
using System.Collections;

public class Door_closeth : MonoBehaviour
{
    private float distance;
    private GameObject player;
    private bool isMoving;
    private bool isOpen;
    private Quaternion startRotation;
    private Quaternion endRotation;
    public float rotationSpeed = 90.0f; // Adjust the rotation speed as needed

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isMoving = false;
        isOpen = false;

        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(0, 180, 0); // Rotate -80 degrees around the Y-axis when open
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (!isMoving && Input.GetButtonDown("Fire1") && distance < 2)
        {
            if (isOpen)
            {
                // If the door is open, close it
                isMoving = true;
            }
            else
            {
                // If the door is closed, open it
                isMoving = true;
            }
        }

        if (isMoving)
        {
            // Smoothly interpolate between the start and end rotations
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, isOpen ? startRotation : endRotation, step);

            // Check if the door has reached the desired rotation
            if (Quaternion.Angle(transform.rotation, isOpen ? startRotation : endRotation) < 0.01f)
            {
                isOpen = !isOpen;
                isMoving = false;
            }
        }
    }
}

