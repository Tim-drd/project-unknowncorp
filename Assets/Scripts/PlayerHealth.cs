using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10;
    public int maxHearts = 10;
    [SerializeField] HeartSystem heartSystemPrefab;
    [SerializeField] private HeartSystem hs;

    private void Start()
    {
        hs = Instantiate(heartSystemPrefab, new Vector3(533, -50, 0), Quaternion.identity);
        hs.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
        hs.DrawHeart(health, maxHearts);
    }

    public void DamagePlayer(float damage)
    {
        health -= damage;
        hs.DrawHeart(health, maxHearts);
    }
    
    public void HealPlayer(float heal)
    {
        if (health + heal > maxHearts)
            health = maxHearts;
        else
            health += heal;
        hs.DrawHeart(health, maxHearts);
    }
}
