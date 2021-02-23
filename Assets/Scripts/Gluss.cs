using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gluss : Enemy
{
    public Transform player1;
    public float chaseRadius;
    public float attackRadius;
    public Vector3 homePosition;
    
    private int _time = 0;
    private Vector3 _random;
    public float initialWalkRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindWithTag("Player").transform;
        homePosition = transform.position;
        _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance(player1);
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
            if (distance > attackRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            if (_time % 150 == 0)
            {
                _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
            }

            transform.position = Vector3.MoveTowards(transform.position, homePosition + _random, moveSpeed / 2 * Time.fixedDeltaTime);
        }
        _time++;
    }
    
}
