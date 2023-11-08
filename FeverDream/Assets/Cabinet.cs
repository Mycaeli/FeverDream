using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private bool isOpen;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isMoving;
    public float moveSpeed = 2.0f; // Adjust the movement speed as needed
    public float maxRayLength = 2.0f; // Maximum ray length for interaction
    public float maxDistanceToShowIndicator = 1.5f; // Adjust the distance threshold as needed
    public float gabinet_operture_x;
    public float gabinet_operture_z;
    public GameObject clickableIndicator;
    public GameObject cabinetToInteract; // Public reference to the specific cabinet

    private bool isInteractable = false;

    void Start()
    {
        isMoving = false;
        isOpen = false;
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x + gabinet_operture_x, transform.position.y, transform.position.z + gabinet_operture_z); // Adjust the Z-coordinate as needed
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the drawer towards the end position along the Z-axis
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, isOpen ? startPosition : endPosition, step);

            // Check if the drawer has reached the desired position
            if (Vector3.Distance(transform.position, isOpen ? startPosition : endPosition) < 0.01f)
            {
                isOpen = !isOpen;
                isMoving = false;
            }
        }
        else
        {
            if (IsMouseOverCabinet())
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    isMoving = true;
                }
                // Show the clickable indicator when the player is near the cabinet
                ShowClickableIndicator();
            }
            else
            {
                // Hide the clickable indicator if the player is not near the cabinet
                HideClickableIndicator();
            }
        }
    }

    bool IsMouseOverCabinet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayLength) && hit.collider.CompareTag("Cabinet") && hit.collider.gameObject == cabinetToInteract)
        {
            return true;
        }

        return false;
    }

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
