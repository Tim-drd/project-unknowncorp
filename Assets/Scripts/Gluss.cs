using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Gluss : Enemy
{
    private Rigidbody2D rb;
    public Transform player1;
    public float chaseRadius;
    public float attackRadius;
    public Vector3 homePosition;
    Animator gluss_anim;
    private SpriteRenderer _spriteRenderer;
    public AudioClip[] gluss_sounds;
    
    private int _time = 0;
    private Vector3 _random;
    public float initialWalkRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        homePosition = transform.position;
        currentState = EnemyState.idle;
        _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
        gluss_anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = 0.3f;
        AudioManager.instance.PlayClip(gluss_sounds[1], transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance(player1);
        SetParam();
        sound();
    }

    // Vérifie la distance avec la cible "target"
    // Si la distance est inférieure à "chaseRadius" mais supérieure à "attackRadius", l'ennemi se rapproche du joueur
    // Sinon, l'ennemi se déplace vers une position aléatoire dans le périmètre "initialWalkRadius" autour de son point d'apparition initial "homePosition"
    // La position aléatoire est modifiée toutes les 150 utilisations de CheckDistance
    void CheckDistance(Transform target)
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius)
        {
            moveSpeed = 0.4f;
            if (distance > attackRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(temp);
            }
            Flip(target.position.x);
            ChangeState(EnemyState.chase);
        }
        else
        {
            if (_time % 350 == 0)
            {
                if (Random.Range(0, 2) == 0)
                {
                    
                }
                else
                {
                    _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2),
                        Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
                }
            }

            moveSpeed = 0.3f;
            Vector3 temp = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
            rb.MovePosition(temp);
            Flip((homePosition + _random).x);
            transform.position = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
            ChangeState(EnemyState.idle);
        }
        _time++;
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    void Flip(float destination)
    {
        if (transform.position.x > destination)
        {
            _spriteRenderer.flipX = false; //gauche
        }
        else
        {
            _spriteRenderer.flipX = true; //droite
        }
    }

    void SetParam() //sert à paramétrer les animations
    {
        if (currentState == EnemyState.chase) //le joueur est target
        {
            gluss_anim.SetInteger("Gluss state", 2);
        }
        else
        {
            if (transform.position == homePosition + _random) //idle
                gluss_anim.SetInteger("Gluss state", 0);
            else
                gluss_anim.SetInteger("Gluss state", 1); //passif
        }
    }

    void sound()
    {
        if (currentState == EnemyState.chase)
            if (_time % 100 == 0) 
                AudioManager.instance.PlayClip(gluss_sounds[0], transform.position); //correspond au son du gluss qui target qui doit être placé en premier dans "gluss_sounds"
            else
            {
            }
        else
        {
            if (transform.position != homePosition + _random && _time % 350 == 0)
                AudioManager.instance.PlayClip(gluss_sounds[1], transform.position); //son de gluss passif, placé en second
        }
    }

}