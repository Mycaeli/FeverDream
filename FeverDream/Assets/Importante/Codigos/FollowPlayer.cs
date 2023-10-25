using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float range = 2f;
    public float moveSpeed = 5f; // Velocidad de movimiento de la caja
    private Transform player; // Referencia al transform del jugador
    private bool isPlayerInRange = false; // Variable para rastrear si el jugador está en rango

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador por su etiqueta
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            // Verificar si el jugador está en rango y presiona la tecla "Espacio"
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Teletransportar al jugador sobre la caja
                Vector3 newPosition = transform.position + Vector3.up * range; // Ajusta la altura según tu necesidad
                player.position = newPosition;
            }

            // Verificar si el jugador está en rango y presiona la tecla "E"
            if (Input.GetKey(KeyCode.E))
            {
                // Obtener la dirección en la que el jugador está mirando
                Vector3 moveDirection = player.forward.normalized;

                // Calcular la nueva posición de la caja
                Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

                // Mover la caja a la nueva posición
                transform.position = newPosition;
            }
        }
    }
}




