using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState
{
    walk,
    attack
}

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    public PlayerState currentState;
    float horizontalMovement;
    float verticalMovement;
    bool attack;
    
    void Start()
    {
        currentState = PlayerState.walk;
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
        if (!attack && currentState == PlayerState.walk)
        {
            MovePlayer(horizontalMovement, verticalMovement);
            UpdateAnimation(horizontalMovement, verticalMovement);
        }
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
    }

    private IEnumerator AttackCo()
    {
        _animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        _animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.5f);
        currentState = PlayerState.walk;
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
        }
        else
        {
            _animator.SetBool("moving", false);
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
}
