using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool game_paused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsWindow;

    // Update is called once per frame
    void Update()
    {
        if (KeyBindingManager.GetKeyDown(KeyAction.pause))
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
        if (players.Length > 1) //le jeu sera mis en pause que si le joueur joue en solo sinon le temps défilera encore
        {
            PlayerMovement.instance.enabled = false; //pour corriger un petit problème
            pauseMenuUI.SetActive(true);
            Time.timeScale = 1;
            game_paused = true;
        }
        else
        {
            PlayerMovement.instance.enabled = false; //pour corriger un petit problème
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            game_paused = true;
        }
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
        PhotonNetwork.Disconnect();
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
