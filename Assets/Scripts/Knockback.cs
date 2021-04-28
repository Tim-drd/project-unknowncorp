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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Gluss")||other.gameObject.CompareTag("Terrus") || other.gameObject.CompareTag("Devorror")) && this.gameObject.CompareTag("MyWeapon"))//enemy est le tag de la hitbox des armes
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            Collider2D player = this.GetComponent<Collider2D>();
            if (enemy != null && enemy.GetComponent<Enemy>().currentState != EnemyState.knocked )
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.knocked;
                Debug.Log("knocked");
                //enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                //Vector2 difference = enemy.transform.position - transform.position;
                //enemy.transform.position = new Vector2(enemy.transform.position.x + difference.x, enemy.transform.position.y + difference.y);
                
                EnemyHealtManager eHealthMan;
                eHealthMan = enemy.gameObject.GetComponent<EnemyHealtManager>();
                eHealthMan.HurtEnemy(damage);
            }
        }
    }
}
