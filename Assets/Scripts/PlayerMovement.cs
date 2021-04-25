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
    
    private int _time = 0; //à termes y faudra un game time générique
    
    
    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }
    
    
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
    
    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        if (KeyBindingManager.GetKey(KeyAction.up))
            change.y = 0.8f;

        if (KeyBindingManager.GetKey(KeyAction.down))
            change.y = -0.8f;

        if (KeyBindingManager.GetKey(KeyAction.left))
            change.x = -0.8f;


        if (KeyBindingManager.GetKey(KeyAction.right))
            change.x = 0.8f;
        
        horizontalMovement = change.x;
        verticalMovement = change.y;
        attack = KeyBindingManager.GetKeyDown(KeyAction.attack);
        if (attack && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }

        _time++;
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