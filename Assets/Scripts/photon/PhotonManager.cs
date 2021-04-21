using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to photon ...");
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("We are connected to master !");
    }
    
    public override void OnJoinedLobby()
    {

        /*PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 2},TypedLobby.Default);*/
        Debug.Log("On joined lobby called");
    }

    /*public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
}*/
}
