using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Knockback : MonoBehaviour
{
    public float thrust;

    //public float knockbacktime;
    public float damage;
    public bool pvpStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pvpStarted = GetComponentInParent<PlayerHealth>().pvpStarted;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.CompareTag("MyWeapon"))
        {
            if ((other.gameObject.CompareTag("Gluss") || other.gameObject.CompareTag("Terrus") ||
                 other.gameObject.CompareTag("Devorror"))) //enemy est le tag de la hitbox des armes
            {
                Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
                Collider2D player = this.GetComponent<Collider2D>();

                if (enemy != null && enemy.GetComponent<Enemy>().currentState != EnemyState.knocked)
                {
                    enemy.GetComponent<Enemy>().currentState = EnemyState.knocked;
                    Vector2 difference = enemy.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    enemy.AddForce(difference, ForceMode2D.Impulse);
                    EnemyHealtManager eHealthMan;
                    eHealthMan = enemy.gameObject.GetComponent<EnemyHealtManager>();
                    eHealthMan.HurtEnemy(damage);
                }
            }
            else if (other.gameObject.CompareTag("Boss"))
            {
                Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
                
                if (enemy != null && enemy.GetComponent<Enemy>().currentState != EnemyState.knocked &&
                    enemy.GetComponent<PhotonBossView>().specialAttack == false && enemy.GetComponent<Boss>().sleeping == false)
                {
                    enemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                    enemy.GetComponent<Enemy>().currentState = EnemyState.knocked;
                    Vector2 difference = enemy.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    enemy.AddForce(difference, ForceMode2D.Impulse);
                    EnemyHealtManager eHealthMan;
                    eHealthMan = enemy.gameObject.GetComponent<EnemyHealtManager>();
                    eHealthMan.HurtEnemy(damage);
                }
            }
            else if (pvpStarted == true && other.gameObject.CompareTag("PlayerClone"))
            {
                Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();

                int random = UnityEngine.Random.Range(0, 10);

                float _damage = 0;
                if (random < 3)
                    _damage = 1f;
                else if (damage < 7)
                    _damage = .5f;

                if (enemy.GetComponent<PlayerHealth>().health - _damage <= 0)
                {
                    enemy.GetComponent<PlayerHealth>().DamagePlayer(_damage);
                    GetComponentInParent<PhotonPlayerView>().pvpEnded = true;
                    enemy.GetComponent<PhotonPlayerView>().pvpEnded = true;
                }
                else
                {
                    enemy.GetComponent<PlayerHealth>().DamagePlayer(_damage);
                }
            }
        }
    }
}