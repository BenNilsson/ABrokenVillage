using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject howToPlay;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void DisplayHowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void DisplayMainMenu()
    {
        howToPlay.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
