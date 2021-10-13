using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float healthPoints;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerClone"))
        {
            foreach (var player in GameObject.FindGameObjectsWithTag("PlayerClone"))
            {
                player.GetComponent<PlayerHealth>().HealPlayer(healthPoints);
            }
            Destroy(gameObject);
        }
    }
}
