using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_open_Fungus : MonoBehaviour
{
    public Door door; // Reference to the Door script

    public GameObject yourGameObject; // Reference to the GameObject you want to activate

    void Start()
    {
        // Make sure to assign the Door script and the target GameObject in the Unity Inspector.
        // You should assign the "Door" script component to the "door" field.
        // You should assign the GameObject you want to activate to the "yourGameObject" field.
    }

    void Update()
    {
        if (door != null && yourGameObject != null)
        {
            if (door.operative) // Check if the operative variable is true
            {
                yourGameObject.SetActive(true); // Activate the GameObject
            }
            else
            {
                yourGameObject.SetActive(false); // Deactivate the GameObject
            }
        }
    }
}
