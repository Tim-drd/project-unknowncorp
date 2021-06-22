using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    public bool sleeping;
    private bool wakingUp;

    private EnemyHealtManager _healthManager;

    private int phase;
    
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

    private float knockedTimer;
    
    private PhotonBossView bossPhoton;
    
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
        phase = 1;
        _healthManager = GetComponent<EnemyHealtManager>();
        knockedTimer = 0;
        bossPhoton = GetComponent<PhotonBossView>();
/*        AudioManager.instance.PlayClip(gluss_sounds[1], transform.position);*/
    }

    public bool is_dead()
    {
        return this._healthManager.currenthealth <= 0;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerClone"))
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
                        player.GetComponent<PlayerHealth>().HealPlayer(10);
                    }
                }
            }
        }
        else
        {
            float minDistance = Vector3.Distance(players[0].transform.position, transform.position);
            GameObject target = players[0];
            foreach (var player in players)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(player.transform.position, transform.position);
                    target = player;
                }
            }
            CheckDistance(target);

            /*sound();*/
        }

        if (_healthManager.currenthealth <= 0)
        {
            gameObject.SetActive(false);
        }
    
        if (_healthManager.currenthealth < (int) (0.66f * _healthManager.maxhealth))
        {
            if (_healthManager.currenthealth > (int) (0.33f * _healthManager.maxhealth))
            {
                phase = 2;
                _animator.SetInteger("phase", 2);
            }
            else
            {
                phase = 3;
                _animator.SetInteger("phase", 3);
            }
        }
        
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
    void CheckDistance(GameObject target)
    { 
        bool inAttackRange = (target.transform.position.x - transform.position.x) > -1.35
                           && (target.transform.position.x - transform.position.x) < 1.35
                           && (target.transform.position.y - transform.position.y) > -1.65
                           && (target.transform.position.y - transform.position.y) < 0.9;
        
        float distance = Vector3.Distance(target.transform.position, transform.position);
        
        if (!sleeping && !bossPhoton.specialAttack)
        {
            if (distance <= chaseRadius && currentState != EnemyState.knocked)
            {
                if (currentState != EnemyState.attack)
                {
                    //if (distance > attackRadius)
                    if (!inAttackRange)
                    {
                        ChangeState(EnemyState.chase);
                        transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
                            moveSpeed * Time.fixedDeltaTime);
                        Vector3 temp = Vector3.MoveTowards(transform.position, target.transform.position,
                            moveSpeed * Time.fixedDeltaTime);
                        rb.MovePosition(temp);
                        _animator.SetBool("moving", true);
                    }
                    else
                    {
                        _animator.SetBool("moving", false);
                        StartCoroutine(Attack(target));
                    }

                    SetParam(target.transform.position.x - transform.position.x,
                        target.transform.position.y - transform.position.y);
                }
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

                Vector3 temp = Vector3.MoveTowards(transform.position, homePosition + _random,
                    moveSpeed / 2 * Time.fixedDeltaTime);
                rb.MovePosition(temp);
                transform.position = Vector3.MoveTowards(transform.position, homePosition + _random,
                    moveSpeed / 2 * Time.fixedDeltaTime);

                if (transform.position == homePosition)
                {
                    _animator.SetBool("moving", false);
                    _animator.SetFloat("moveX", 0);
                    _animator.SetFloat("moveY", 0);
                }
                else
                {
                    _animator.SetBool("moving", true);
                    SetParam(homePosition.x - transform.position.x, homePosition.y - transform.position.y);
                }
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
    
    IEnumerator Attack(GameObject target)
    {
        ChangeState(EnemyState.attack);

        if (phase == 2 && bossPhoton.countToSpecialAttack > 11)
        {
            bossPhoton.specialAttack = true;
            
            _animator.SetBool("specialAttack", true);

            yield return new WaitForSeconds(0.1f);
            
            _animator.SetBool("specialAttack", false);
            
            yield return new WaitForSeconds(1.2f);

            foreach (var player in players)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 2)
                {
                    player.GetComponent<PlayerHealth>().DamagePlayer(2f);
                }
            }
            
            yield return new WaitForSeconds(1.2f);

            bossPhoton.countToSpecialAttack = 0;
            
            bossPhoton.specialAttack = false;
        }
        else if (phase == 3 && bossPhoton.countToSpecialAttack > 5)
        {
            bossPhoton.specialAttack = true;

            _animator.SetBool("specialAttack", true);

            yield return new WaitForSeconds(0.1f);
            
            _animator.SetBool("specialAttack", false);
            
            yield return new WaitForSeconds(1.2f);

            foreach (var player in players)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 3)
                {
                    player.GetComponent<PlayerHealth>().DamagePlayer(3f);
                }
            }
            
            yield return new WaitForSeconds(1.2f);
                
            bossPhoton.countToSpecialAttack = 0;
            
            bossPhoton.specialAttack = false;
        }
        else
        {
            _animator.SetBool("attacking", true);

            yield return new WaitForSeconds(0.1f);

            _animator.SetBool("attacking", false);

            int damage = Random.Range(0, 100);
            if (damage > 60)
                target.GetComponent<PlayerHealth>().DamagePlayer(1f);
            else if (damage > 20)
                target.GetComponent<PlayerHealth>().DamagePlayer(.5f);
            
            yield return new WaitForSeconds(1.5f);
            
            bossPhoton.countToSpecialAttack++;
        }
        
        if (currentState != EnemyState.knocked)
            ChangeState(EnemyState.idle);
    
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
