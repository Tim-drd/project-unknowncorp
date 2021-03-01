﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  MainMenu: MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;
    
    void Start()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(Screen.width, Screen.height, true);
    }
    public void StartGame()
    {
        Debug.Log("Start Game lancé");
        SceneManager.LoadScene(levelToLoad);
    }
    
    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }
    
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
