using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private bool isOpen;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isMoving;
    public float moveSpeed = 2.0f; // Adjust the movement speed as needed
    public float rayLength = 2.0f; // Adjust the ray length as needed
    public float gabinet_operture_x;
    public float gabinet_operture_z;


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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        isMoving = true;
                    }
                }
            }

            // Debug.DrawLine to visualize the ray
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.red);
        }
    }
}

