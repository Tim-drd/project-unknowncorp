using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Vector3 position;
    private PhotonView myPhotonView;
    private Rigidbody2D rb;
    public AudioClip son;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerClone") && !other.isTrigger)
        {
            AudioManager.instance.PlayClip(son, other.transform.position);
            myPhotonView = other.GetComponent<PhotonView>();
            rb = other.GetComponent<Rigidbody2D>();
            rb.transform.position = position;
        }
    }
}
