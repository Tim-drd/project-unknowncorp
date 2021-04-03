using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
public enum PlayerState
{
    idle,
    walk,
    attack
}
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera camera;
    private PhotonView myPhotonView;
    public float speed;
    private Vector3 change;
    public Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    public AudioClip[] character_sounds;
    public AudioSource audio_character;
    public PlayerState currentState;
    float horizontalMovement;
    float verticalMovement;
    bool attack;
    int weaponIndex; 
    
    private int _time = 0; //à termes y faudra un game time générique
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        if (!myPhotonView.IsMine)
            camera.enabled = false;
        currentState = PlayerState.idle;
        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        horizontalMovement = 0;
        verticalMovement = 0;
        attack = false;
        weaponIndex = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        horizontalMovement = change.x;
        verticalMovement = change.y;
        attack = Input.GetButtonDown("attack");
        if (attack && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }

        if (Input.GetButtonDown("change weapon"))
        {
            if (weaponIndex == 3)
                weaponIndex = 0;
            else weaponIndex++;
        }
        
        _time++;
        _animator.SetInteger("weaponIndex", weaponIndex); // test à supprimer
    }
    
    private void FixedUpdate()
    {
        if (!attack && currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            if (change != Vector3.zero)
            {
                MoveCharacter();
            }
            if (currentState == PlayerState.idle)
            {
                audio_character.Stop();
            }
            UpdateAnimation(horizontalMovement, verticalMovement);
        }
        if (!audio_character.isPlaying && currentState != PlayerState.idle) 
            sound();
    }
    
    private IEnumerator AttackCo()
    {
        _animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return new WaitForSeconds(.1f);
        _animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.4f);
        currentState = PlayerState.idle;
    }
    void MoveCharacter()
    {
        rb.MovePosition(transform.position + change.normalized * speed * Time.fixedDeltaTime);
    }
    
    void UpdateAnimation(float _horizontalMovement, float _verticalMovement)
    {
        if (new Vector3(_horizontalMovement, _verticalMovement) != Vector3.zero)
        {
            _animator.SetFloat("moveX", _horizontalMovement);
            _animator.SetFloat("moveY", _verticalMovement);
            _animator.SetBool("moving", true);
            currentState = PlayerState.walk;
        }
        else
        {
            _animator.SetBool("moving", false);
            currentState = PlayerState.idle;
        }
    }
    void sound()
    {
        if (currentState == PlayerState.walk)
        {
            audio_character.clip = character_sounds[0]; //bruits de pas
            audio_character.Play();
        }
        else
        {
            if (currentState == PlayerState.attack)
            {
                audio_character.clip = character_sounds[1];
                audio_character.Play(); //bruit d'attaque
            }
        }
            
    }
}