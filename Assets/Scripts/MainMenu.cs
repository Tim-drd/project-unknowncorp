using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  MainMenu: MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow, mainWindow, multiWindow;
    
    void Start()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(Screen.width, Screen.height, true);
    }
    public void StartGame()
    {
        mainWindow.SetActive(false);
        multiWindow.SetActive(true);
        /* Debug.Log("Start Game lancé");
        SceneManager.LoadScene(levelToLoad);*/
    }
    
    public void SettingsButton()
    {
        mainWindow.SetActive(false);
        settingsWindow.SetActive(true);
    }
    
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
        mainWindow.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
