using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    private bool sleeping;
    private bool wakingUp;
    
    private Rigidbody2D rb;
    public GameObject[] players;
    public float chaseRadius;
    public float attackRadius;
    public Vector3 homePosition;
    Animator _animator;
    /*public AudioClip[] gluss_sounds;*/
    
    private int _time = 0;
    private Vector3 _random;
    public float initialWalkRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerClone");
        rb = GetComponent<Rigidbody2D>();
        homePosition = transform.position;
        currentState = EnemyState.idle;
        _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
        _animator = GetComponent<Animator>();
        sleeping = true;
        wakingUp = false;
/*        AudioManager.instance.PlayClip(gluss_sounds[1], transform.position);*/
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerClone"))
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionExit(Collision other)
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    void LateUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerClone");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleeping)
        {
            if (!wakingUp)
            {
                foreach (var player in players)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < 4)
                    {
                        StartCoroutine(WakeUpCo());
                    }
                }
            }
        }
        else
        {
            float minDistance = Vector3.Distance(players[0].transform.position, transform.position);
            Transform target = players[0].transform;
            foreach (var player in players)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(player.transform.position, transform.position);
                    target = player.transform;
                }
            }
            CheckDistance(target);

            /*sound();*/
        }
    }
    
    private IEnumerator WakeUpCo()
    {
        wakingUp = true;
        _animator.SetBool("sleeping", false);
        yield return new WaitForSeconds(7f);
        sleeping = false;
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
            ChangeState(EnemyState.chase);
            if (distance > attackRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(temp);
                _animator.SetBool("moving",true);
            }
            else
            {
                _animator.SetBool("moving",false);
            }

            SetParam(target.position.x - transform.position.x, target.position.y - transform.position.y);
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

            Vector3 temp = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
            rb.MovePosition(temp);
            transform.position = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
            
            if (transform.position == homePosition)
            {
                _animator.SetBool("moving",false);
                _animator.SetFloat("moveX", 0);
                _animator.SetFloat("moveY", 0);
            }
            else
            {
                _animator.SetBool("moving",true);
                SetParam(homePosition.x - transform.position.x, homePosition.y - transform.position.y);
            }
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

    void SetParam(float destinationX, float destinationY) //sert à paramétrer les animations
    {
        if (destinationY > 0)
        {
            if (destinationX > destinationY)
            {
                _animator.SetFloat("moveX", 1);
                _animator.SetFloat("moveY", 0);
            }
            else
            {
                if (-destinationX > destinationY)
                {
                    _animator.SetFloat("moveX", -1);
                    _animator.SetFloat("moveY", 0);
                }
                else
                {
                    _animator.SetFloat("moveX", 0);
                    _animator.SetFloat("moveY", 1);
                }
            }
        }
        else
        {
            if (destinationX < destinationY)
            {
                _animator.SetFloat("moveX", -1);
                _animator.SetFloat("moveY", 0);
            }
            else
            {
                if (-destinationX < destinationY)
                {
                    _animator.SetFloat("moveX", 1);
                    _animator.SetFloat("moveY", 0);
                }
                else
                {
                    _animator.SetFloat("moveX", 0);
                    _animator.SetFloat("moveY", -1);
                }
            }
        }
    }

    /*void sound()
    {
        if (currentState == EnemyState.chase)
            if (_time % 50 == 0) 
                AudioManager.instance.PlayClip(gluss_sounds[0], transform.position); //correspond au son du gluss qui target qui doit être placé en premier dans "gluss_sounds"
            else
            {
            }
        else
        {
            if (transform.position != homePosition + _random && _time % 100 == 0)
                AudioManager.instance.PlayClip(gluss_sounds[1], transform.position); //son de gluss passif, placé en second
        }
    }*/

}
