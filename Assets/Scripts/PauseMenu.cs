using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool game_paused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsWindow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (game_paused && settingsWindow.activeSelf == false)
            {
                Resume();
            }
            else
            {
                if (settingsWindow.activeSelf == false) 
                    Paused();
            }
        }
    }

    void Paused()
    {
        PlayerMovement.instance.enabled = false; //pour corriger un petit problème
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        game_paused = true;
    }
    
    public void Resume()
    {
        PlayerMovement.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        game_paused = false;
    }

    public void Main_menu_Button()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MenuPrincipal");
    }
    
    public void SettingsButton()
    {
        pauseMenuUI.SetActive(false);
        settingsWindow.SetActive(true);
    }
    
    public void CloseSettingsWindow_paused()
    {
        settingsWindow.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
