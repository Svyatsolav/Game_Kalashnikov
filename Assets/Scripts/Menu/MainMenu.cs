using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject createGamePan;

    public void CreateGamePanel()
    {
        if(createGamePan.activeInHierarchy == false) createGamePan.SetActive(true);
        else if(createGamePan.activeInHierarchy == true) createGamePan.SetActive(false);
    }
    public void CreateGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
