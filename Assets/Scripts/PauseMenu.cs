using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool game_paused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsWindow;
    public GameObject KeyWindow;
    public Slider soundSlider;
	public Slider musicSlider;
	public AudioMixer audioMixer;

    // Update is called once per frame
    void Update()
    {
        /*pauseMenuUI.transform.position = new Vector3(Screen.width, Screen.height, 0);
        settingsWindow.transform.position = new Vector3(Screen.width, Screen.height, 0);
        KeyWindow.transform.position = new Vector3(Screen.width, Screen.height, 0);*/


        if (KeyBindingManager.GetKeyDown(KeyAction.pause))
        {
            if (game_paused && settingsWindow.activeSelf == false && KeyWindow.activeSelf == false)
            {
                Resume();
            }
            else
            {
                if (settingsWindow.activeSelf == false && KeyWindow.activeSelf == false)
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
        string roomName = photonButtons.photonbutton.roomName;
        int level = LoadCheckpointSpawn.checkpointSpawn.checkpoint_number;
        PlayerPrefs.SetInt(roomName, level);

        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MenuPrincipal");
    }
    
    public void SettingsButton()
    {
        pauseMenuUI.SetActive(false);
        settingsWindow.SetActive(true);

        float valueSound;
        bool result = audioMixer.GetFloat("sound", out valueSound);
        soundSlider.value = valueSound;

        float valueMusic;
        bool result2 = audioMixer.GetFloat("music", out valueMusic);
        musicSlider.value = valueMusic;
    }
    
    public void CloseSettingsWindow_paused()
    {
        settingsWindow.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void GoToKeyManager()
    {
        settingsWindow.SetActive(false);
        KeyWindow.SetActive(true);
    }

    public void GoBackToSettingsWindow()
    {
        settingsWindow.SetActive(true);
        KeyWindow.SetActive(false);
    }
}
