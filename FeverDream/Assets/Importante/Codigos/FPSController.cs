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
        // Movimiento hacia adelante y atrás
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;

        // Movimiento hacia los lados
        float movimientoLateral = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;

        // Rotación horizontal
        float rotacionHorizontal = Input.GetAxis("Mouse X") * velocidadRotacion;

        // Aplicar rotación horizontal
        transform.Rotate(0, rotacionHorizontal, 0);

        // Aplicar movimiento
        Vector3 movimiento = new Vector3(movimientoLateral, 0, movimientoAdelanteAtras);
        transform.Translate(movimiento);

        // Limitar la rotación vertical entre -90° y 90° para evitar giros excesivos
        rotacionX -= Input.GetAxis("Mouse Y") * velocidadRotacion;
        rotacionX = Mathf.Clamp(rotacionX, -90, 90);

        // Aplicar rotación vertical a la cámara (si tienes una cámara en la cápsula)
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);
    }
}