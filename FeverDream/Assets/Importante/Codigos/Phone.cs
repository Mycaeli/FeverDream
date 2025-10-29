using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [Header("Códigos válidos")]
    [SerializeField] private string codeCorrecto = "3481";
    [SerializeField] private string codeIncorrecto1 = "4444";
    [SerializeField] private string codeIncorrecto2 = "3053";
    [SerializeField] private string codeEasterEgg = "4448";

    [Header("Configuración")]
    [SerializeField] private int maxLongitud = 4; // Límite de dígitos
    [SerializeField] private Text uiText;
    [SerializeField] private AudioSource audioError;
    [SerializeField] private GameObject[] fungus; // 0: correcto, 1: incorrecto1, 2: incorrecto2, 3: easter, 4: error genérico

    private string codigoActual = "";

    void Update()
    {
        // Detectar teclas del 0 al 9
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                AgregarNumero(i.ToString());
            }
        }

        // Borrar con Backspace
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            BorrarCodigo();
        }
    }

    public void AgregarNumero(string numero)
    {
        if (codigoActual.Length >= maxLongitud)
            return;

        codigoActual += numero;
        uiText.text = codigoActual;
        Debug.Log($"Número ingresado: {numero}");

        // Si se alcanzó la longitud máxima, validar automáticamente
        if (codigoActual.Length == maxLongitud)
        {
            ValidarCodigo();
        }
    }

    public void ValidarCodigo()
    {
        // Desactivar todos los Fungus antes de activar el correspondiente
        foreach (GameObject f in fungus)
        {
            if (f != null) f.SetActive(false);
        }

        // Comparar códigos
        if (codigoActual == codeCorrecto)
        {
            Debug.Log("✅ Código correcto");
            ActivarFungus(0);
        }
        else if (codigoActual == codeIncorrecto1)
        {
            Debug.Log("❌ Código incorrecto 1");
            ActivarFungus(1);
        }
        else if (codigoActual == codeIncorrecto2)
        {
            Debug.Log("❌ Código incorrecto 2");
            ActivarFungus(2);
        }
        else if (codigoActual == codeEasterEgg)
        {
            Debug.Log("🎉 Código especial encontrado!");
            ActivarFungus(3);
        }
        else
        {
            Debug.Log("⚠️ Código no válido");
            ActivarFungus(4);
            if (audioError != null)
                audioError.Play();
        }

        // Reiniciar código y texto después de validar
        codigoActual = "";
        uiText.text = "";
    }

    private void ActivarFungus(int index)
    {
        if (index >= 0 && index < fungus.Length && fungus[index] != null)
            fungus[index].SetActive(true);
    }

    public void BorrarCodigo()
    {
        codigoActual = "";
        uiText.text = "";
        Debug.Log("🧹 Código borrado");
    }
}


