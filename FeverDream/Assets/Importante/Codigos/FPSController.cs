using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 2.0f;

    private float rotacionX = 0;

    void Update()
    {
        // Movimiento hacia adelante y atr�s
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;

        // Movimiento hacia los lados
        float movimientoLateral = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;

        // Rotaci�n horizontal
        float rotacionHorizontal = Input.GetAxis("Mouse X") * velocidadRotacion;

        // Aplicar rotaci�n horizontal
        transform.Rotate(0, rotacionHorizontal, 0);

        // Aplicar movimiento
        Vector3 movimiento = new Vector3(movimientoLateral, 0, movimientoAdelanteAtras);
        transform.Translate(movimiento);

        // Limitar la rotaci�n vertical entre -90� y 90� para evitar giros excesivos
        rotacionX -= Input.GetAxis("Mouse Y") * velocidadRotacion;
        rotacionX = Mathf.Clamp(rotacionX, -90, 90);

        // Aplicar rotaci�n vertical a la c�mara (si tienes una c�mara en la c�psula)
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);
    }
}