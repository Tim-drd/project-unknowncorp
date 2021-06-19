using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PhotonPlayerView : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    public bool pvpEnded;

    private void Start()
    {
        pvpEnded = false;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(pvpEnded);
        }
        else
        {
            pvpEnded = (bool) stream.ReceiveNext();
        }
    }
}