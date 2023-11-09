using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    string Code = "3481000";
    string CodeInc = "44442244";
    string CodeInc2 = "30537700";
    string CodeEaster = "4448300";
    string Nr = "";
    int NrIndex = 0;
    public GameObject[] Fungus;
    string alpha;
    public Text UiText = null;

    private void Update()
    {
        // Captura la entrada del teclado numérico
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CodeFunction("0");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CodeFunction("1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CodeFunction("2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CodeFunction("3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CodeFunction("4");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CodeFunction("5");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            CodeFunction("6");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            CodeFunction("7");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            CodeFunction("8");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CodeFunction("9");
        }

        // Captura la tecla "Enter"
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
        }

        // Captura la tecla "Backspace"
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Delete();
        }
    }

    public void CodeFunction(string Numbers)
    {
        NrIndex++;
        Nr = Nr + Numbers;
        UiText.text = Nr; // Actualiza el texto en el objeto UiText
        Debug.Log("Número oprimido: " + Numbers); // Muestra el número en la consola
    }

    public void Enter()
    {
        if (Nr == Code)
        {
            Debug.Log("Código correcto: " + Nr);
            Fungus[0].SetActive(true);
        }
        else if (Nr == CodeInc)
        {
            Debug.Log("Código Incorrecto 1: " + Nr);
            Fungus[1].SetActive(true);
        }
        else if (Nr == CodeInc2)
        {
            Debug.Log("Código Incorrecto 2: " + Nr);
            Fungus[2].SetActive(true);
        }
        else if (Nr == CodeEaster)
        {
            Debug.Log("Código Sospechoso: " + Nr);
            Fungus[3].SetActive(true);
        }
        else
        {
            Debug.Log("Número no apto: " + Nr);
            Fungus[4].SetActive(false);
        }
    }

    public void Delete()
    {
        NrIndex++;
        Nr = "";
        UiText.text = Nr; // Limpia el texto en el objeto UiText
    }
}


