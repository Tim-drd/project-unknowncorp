using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    [SerializeField] private Camera camera;
    private PhotonView myPhotonView;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myPhotonView = GetComponent<PhotonView>();
        if (!myPhotonView.IsMine)
            camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
     
        }
    }
    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change.normalized * speed * Time.fixedDeltaTime);
    }
}
