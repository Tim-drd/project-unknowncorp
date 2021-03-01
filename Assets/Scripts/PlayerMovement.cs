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
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    public AudioClip[] character_sounds;
    public AudioSource audio_character;
    public PlayerState currentState;
    float horizontalMovement;
    float verticalMovement;
    bool attack;
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
    }
    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        if (!attack && currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            if (currentState == PlayerState.idle)
            {
                audio_character.Stop();
            }
            MovePlayer(horizontalMovement, verticalMovement);
            UpdateAnimation(horizontalMovement, verticalMovement);
        }
        if (!audio_character.isPlaying && currentState != PlayerState.idle) 
            sound();
    }
    
    // Update is called once per frame
    void Update()
    {
        attack = Input.GetButtonDown("attack");
        if (attack && currentState != PlayerState.attack)
        {
            rb.velocity = new Vector2(0, 0);
            StartCoroutine(AttackCo());
        }

        _time++;
    }
    private IEnumerator AttackCo()
    {
        _animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        _animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.5f);
        currentState = PlayerState.idle;
    }
    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }

    void UpdateAnimation(float _horizontalMovement, float _verticalMovement)
    {
        if (new Vector2(_horizontalMovement, _verticalMovement) != Vector2.zero)
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
        
        if (_horizontalMovement < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_horizontalMovement > 0)
        {
            _spriteRenderer.flipX = false;
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