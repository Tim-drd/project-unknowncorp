using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockbacktime;
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
        if (other.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("MyWeapon"))//enemy est le tag de la hitbox des armes
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            Collider2D player = this.GetComponent<Collider2D>();
            if (enemy != null && enemy.GetComponent<Enemy>().currentState != EnemyState.knocked )
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.knocked;
                Debug.Log("knocked");
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                //Vector2 difference = enemy.transform.position - transform.position;
                //enemy.transform.position = new Vector2(enemy.transform.position.x + difference.x, enemy.transform.position.y + difference.y);
                StartCoroutine(KnockCo(enemy, other));
            }
        }
    }
    
    private IEnumerator KnockCo(Rigidbody2D enemy, Collider2D other)
    {
        EnemyHealtManager eHealthMan;
        eHealthMan = enemy.gameObject.GetComponent<EnemyHealtManager>();
        eHealthMan.HurtEnemy(damage);
        yield return new WaitForSeconds(knockbacktime);
        enemy.velocity = Vector2.zero;
        enemy.isKinematic = true;
        enemy.velocity = Vector2.zero;
        //enemy.constraints = RigidbodyConstraints2D.FreezeRotation;
        enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        Debug.Log("idled");
    }
}
