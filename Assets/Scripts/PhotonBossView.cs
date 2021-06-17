using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PhotonBossView : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    public bool specialAttack;
    [SerializeField]
    public int countToSpecialAttack;
    
    private void Start()
    {
        specialAttack = false;
        countToSpecialAttack = 0;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(specialAttack);
            stream.SendNext(countToSpecialAttack);
        }
        else
        {
            specialAttack = (bool) stream.ReceiveNext();
            countToSpecialAttack = (int) stream.ReceiveNext();
        }
    }
}