using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,  // au repos
    chase,  // poursuit le joueur
    attack  // attaque le joueur
}

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public EnemyState currentState;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.currentState = EnemyState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
