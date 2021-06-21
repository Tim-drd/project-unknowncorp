using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Gluss : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public Vector3 homePosition;
    Animator gluss_anim;
    private SpriteRenderer _spriteRenderer;
    public AudioClip[] gluss_sounds;

    private int _time = 0;
    private Vector3 _random;
    public float initialWalkRadius;

    private float knockedTimer;
    private GameObject[] players;

    public float attack1;
    public float attack2;
    public float attack3;

    /*void OnCollisionStay2D(Collision2D other) 
    { 
        if (other.gameObject.CompareTag("PlayerClone") && currentState != EnemyState.knocked) 
            rb.constraints = RigidbodyConstraints2D.FreezeAll; 
        else if (other.gameObject.CompareTag("MyWeapon"))
            rb.constraints = RigidbodyConstraints2D.None; 
    } 
 
    private void OnCollisionExit(Collision other) 
    { 
        if (other.gameObject.CompareTag("PlayerClone")) 
            rb.constraints = RigidbodyConstraints2D.None; 
    }*/
    
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerClone");
        rb = GetComponent<Rigidbody2D>();
        homePosition = transform.position;
        currentState = EnemyState.idle;
        _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
        gluss_anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        AudioManager.instance.PlayClip(gluss_sounds[1], transform.position);
        knockedTimer = 0;
    }
    
    void LateUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerClone");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float minDistance = Vector3.Distance(players[0].transform.position, transform.position);
        GameObject closestPlayer = players[0];
        foreach (var player in players)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(player.transform.position, transform.position);
                closestPlayer = player;
            }
        }
        
        CheckDistance(closestPlayer.transform);
        SetParam();
        sound();

        if (currentState == EnemyState.knocked)
        {
            if (knockedTimer > .4f)
            {
                rb.velocity = Vector2.zero;
                currentState = EnemyState.idle;
                knockedTimer = 0f;
            }
            else
            {
                knockedTimer += Time.deltaTime;
            }
        }
    }

    // Vérifie la distance avec la cible "target"
    // Si la distance est inférieure à "chaseRadius" mais supérieure à "attackRadius", l'ennemi se rapproche du joueur
    // Sinon, l'ennemi se déplace vers une position aléatoire dans le périmètre "initialWalkRadius" autour de son point d'apparition initial "homePosition"
    // La position aléatoire est modifiée toutes les 150 utilisations de CheckDistance
    void CheckDistance(Transform target)
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && currentState != EnemyState.knocked)
        {
            if (distance > attackRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position,
                    moveSpeed * Time.fixedDeltaTime);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position,
                    moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(temp);
            }
            else if (currentState != EnemyState.attack)
            {
                StartCoroutine(Attack(target.gameObject));
            }

            Flip(target.position.x);
        }
        else if (currentState != EnemyState.knocked)
        {
            if (_time % 350 == 0)
            {
                if (Random.Range(0, 2) != 0)
                {
                    _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2),
                        Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
                }
            }

            Vector3 temp = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
            rb.MovePosition(temp);
            Flip((homePosition + _random).x);
            transform.position = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
            ChangeState(EnemyState.idle);
        }
        _time++;
    }
    
    IEnumerator Attack(GameObject target)
    {
        ChangeState(EnemyState.attack);
        
        yield return new WaitForSeconds(0.1f);
        
        int damage = Random.Range(0, 100);
        if (damage > 66)
            target.GetComponent<PlayerHealth>().DamagePlayer(attack1);
        else if (damage > 33)
            target.GetComponent<PlayerHealth>().DamagePlayer(attack2);
        else
            target.GetComponent<PlayerHealth>().DamagePlayer(attack3);

        yield return new WaitForSeconds(0.9f);
        
        if (currentState != EnemyState.knocked)
            ChangeState(EnemyState.idle);
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
            gluss_anim.SetInteger("Enemy state", 2);
        }
        else
        {
            if (transform.position == homePosition + _random) //idle
                gluss_anim.SetInteger("Enemy state", 0);
            else
                gluss_anim.SetInteger("Enemy state", 1); //passif
        }
    }

    void sound()
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
    }

}