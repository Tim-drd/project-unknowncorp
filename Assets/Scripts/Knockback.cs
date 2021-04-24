using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

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
        if (other.gameObject.CompareTag("Gluss"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.isKinematic = false;
                enemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy, other));
            }
        }
    }
    private IEnumerator KnockCo(Rigidbody2D enemy, Collider2D other)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockbacktime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
            enemy.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
