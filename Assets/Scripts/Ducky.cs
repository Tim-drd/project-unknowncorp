using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ducky : MonoBehaviour
{
    public float speed = 5f;
    Animator anim;

    private int _time = 0;
    public Vector3 homePosition;
    public float initialWalkRadius;
    private Vector3 _random;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        homePosition = transform.position;
        _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        SetParam();
    }

    void move() //se déplace simplement aléatoirement (a une chance de ne pas bouger)
    {
        if (_time % 300 == 0 && Random.Range(0,2) == 0)
        {
            _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, homePosition + _random, speed / 2 * Time.fixedDeltaTime);
        _time++;
    }

    void SetParam() //sert à paramétrer les animations
    {
        if (transform.position == homePosition + _random) 
            anim.SetBool("Toggle move", false); //"Toggle move" la condition de l'animator "anim" pour passer de animé à idle 
        else
            anim.SetBool("Toggle move", true); //Ducky en mouvement
        if (_random.x > 0) //droite
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else //gauche
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
