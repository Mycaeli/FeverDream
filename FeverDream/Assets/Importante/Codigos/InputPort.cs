using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPort : MonoBehaviour
{
    private string input;

    void Start()
    {
        // Acceder al SerialManager y establecer el valor de com
        SerialManager serialManager = FindObjectOfType<SerialManager>();
        if (serialManager != null)
        {
            serialManager.com = input;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Tu lógica de actualización aquí
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }
}
