using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private float distance;
    private GameObject player;
    private Ray ray;
    private bool opened;
    public bool operative = true; // New variable to control door state

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        opened = false;
    }

    void Update()
    {
        if (opened == false)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetButtonDown("Fire1") && distance < 2 && Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Door" && operative) // Check "enabled" variable
            {
                gameObject.GetComponent<Animation>().Play("DoorOpen");
                opened = true;
            }
        }
        else
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetButtonDown("Fire1") && distance < 2 && Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Door" && operative) // Check "enabled" variable
            {
                gameObject.GetComponent<Animation>().Play("DoorClose");
                opened = false;
            }
        }
    }

    // Function to toggle the "enabled" variable
    public void ToggleDoor()
    {
        operative = !operative;
    }
}
