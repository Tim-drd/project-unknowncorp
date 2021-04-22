using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
    }
}
