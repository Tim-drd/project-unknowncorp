using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class  MainMenu: MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow, mainWindow, multiWindow, keyWindow;
    public Toggle myToggle;



    void Start()
    {
        keyWindow.SetActive(false);
        myToggle.isOn = Screen.fullScreen;
        Screen.SetResolution(Screen.width, Screen.height, myToggle.isOn);
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

    public void BackToSettingsWindow()
    {
        settingsWindow.SetActive(true);
        keyWindow.SetActive(false);
    }

    public void GoToKeyWindow()
    {
        settingsWindow.SetActive(false);
        keyWindow.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
