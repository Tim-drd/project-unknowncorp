using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ducky : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.3f;
    Animator anim;
    public AudioClip couack;

    private int _time = 0;
    public Vector3 homePosition;
    public float initialWalkRadius;
    private Vector3 _random;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        homePosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
        AudioManager.instance.PlayClip(couack, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        SetParam();
        sound();
        
    }

    void move() //se déplace simplement aléatoirement (a une chance de ne pas bouger)
    {
        if (_time % 600 == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                
            }
            else
            {
                _random = new Vector3(Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2),
                    Random.Range(-initialWalkRadius / 2, initialWalkRadius / 2), 0);
            }
        }
        Vector3 temp = Vector3.MoveTowards(transform.position, homePosition + _random, speed / 2 * Time.fixedDeltaTime);
        rb.MovePosition(temp);
        transform.position = Vector3.MoveTowards(transform.position, homePosition + _random, speed / 2 * Time.fixedDeltaTime);
        _time++;
    }

    void SetParam() //sert à paramétrer les animations
    {
        if (transform.position == homePosition + _random) 
            anim.SetBool("Toggle move", false); //"Toggle move" la condition de l'animator "anim" pour passer de animé à idle 
        else
        {
            anim.SetBool("Toggle move", true); //Ducky en mouvement
        }
        if (_random.x > 0) //droite
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else //gauche
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void sound()
    {
        if (_time % 700 == 0 && Random.Range(0,3) == 0)
        {
            AudioManager.instance.PlayClip(couack, transform.position);
        }
    }
}
