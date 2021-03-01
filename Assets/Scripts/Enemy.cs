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
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public bool targeted; //booléen servant à savoir si les ennemy sont en mode target du joueur

    // Start is called before the first frame update
    void Start()
    {
        targeted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
