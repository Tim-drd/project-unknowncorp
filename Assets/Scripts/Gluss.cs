using System.Collections;
using System.Collections.Generic;
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
    
    private int _time = 0;
    private Vector3 _random;
    public float initialWalkRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        homePosition = transform.position;
        _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);

        gluss_anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance(player1);
        SetParam();
    }

    // Vérifie la distance avec la cible "target"
    // Si la distance est inférieure à "chaseRadius" mais supérieure à "attackRadius", l'ennemi se rapproche du joueur
    // Sinon, l'ennemi se déplace vers une position aléatoire dans le périmètre "initialWalkRadius" autour de son point d'apparition initial "homePosition"
    // La position aléatoire est modifiée toutes les 150 utilisations de CheckDistance
    // Renvoie un booléen indiquant si l'enemy a target le joueur
    bool CheckDistance(Transform target)
    {
        float distance = Vector3.Distance(target.position, transform.position);
        bool engage = false;
        if (distance <= chaseRadius)
        {
            if (distance > attackRadius)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(temp);
            }
            Flip(target.position.x);
            engage = true;
        }
        else
        {
            if (_time % 150 == 0)
            {
                _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
            }
            Vector3 temp = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
            rb.MovePosition(temp);
            Flip((homePosition + _random).x);
            engage = false;
        }
        _time++;
        return engage;
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
        if (CheckDistance(player1)) //le joueur est target
        {
            gluss_anim.SetBool("New Bool", true);
        }
        else //gluss passif
        {
            gluss_anim.SetBool("New Bool", false);
        }
    }
    
}