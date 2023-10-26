using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public int sceneIndexToLoad = 1; // Variable p�blica para seleccionar la escena a cargar

    public void StartGame()
    {
        SceneManager.LoadScene(sceneIndexToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

