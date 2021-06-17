using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class photonButtons : MonoBehaviourPunCallbacks
{
    public InputField createRoomInput, joinRoomInput;
    public GameObject mainMenu, multiMenu;
    public int level = 0;
    public string roomName = "";

    public static photonButtons photonbutton;

    private void Awake()
    {
        photonbutton = this;
    }

    public void onClickCreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("error, you are not connected to photon");
            return;
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        if (createRoomInput.text.Length == 0)
            return;

        PhotonNetwork.JoinOrCreateRoom(createRoomInput.text, options, TypedLobby.Default);
        Debug.Log("onclickcreateroom bien appellé: " + createRoomInput.text);
        PlayerPrefs.SetInt(createRoomInput.text, 0);
        roomName = createRoomInput.text;

    }


    public void onClickJoinRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom(joinRoomInput.text, options, TypedLobby.Default);
        level = PlayerPrefs.GetInt(joinRoomInput.text,0);
        roomName = joinRoomInput.text;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("we are connected to the room !");
        PhotonNetwork.LoadLevel("GameScene");
        PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);

    }

    public override void OnCreatedRoom()
    { 
        Debug.Log("created room successfully");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("error " + message, this);
    }

    public void LeaveMutliMenu()
    {
        mainMenu.SetActive(true);
        multiMenu.SetActive(false);
    }

}
