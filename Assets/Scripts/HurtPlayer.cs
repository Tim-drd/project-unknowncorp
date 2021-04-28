using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class HurtPlayer : MonoBehaviour
{
    private PlayerHealth healtMan;
    public bool isTouching;
    public float waitToHurt = 1f;
    // Start is called before the first frame update
    void Start()
    {
        healtMan = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                healtMan.DamagePlayer(UnityEngine.Random.Range(1,Convert.ToInt32(GetComponent<Enemy>().baseAttack)+1));
                waitToHurt = 2f;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerClone") && (this.gameObject.CompareTag("Gluss")||this.gameObject.CompareTag("Terrus") || this.gameObject.CompareTag("Devorror")))
        {
            //PlayerHealth ph = this.gameObject.GetComponent<PlayerHealth>();
            //ph.DamagePlayer(2);
            //other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(GetComponent<Gluss>().baseAttack);
            healtMan.DamagePlayer(UnityEngine.Random.Range(1,Convert.ToInt32(GetComponent<Enemy>().baseAttack)+1));
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerClone"))
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerClone"))
        {
            isTouching = false;
            waitToHurt = 2f;
        };
    }
}
