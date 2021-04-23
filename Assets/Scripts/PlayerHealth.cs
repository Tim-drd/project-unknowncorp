using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10;
    public int maxHearts = 10;
    [SerializeField] HeartSystem heartSystemPrefab;
    [SerializeField] private HeartSystem hs;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            hs = Instantiate(heartSystemPrefab, new Vector3(533, -50, 0), Quaternion.identity);
            hs.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
            hs.DrawHeart(health, maxHearts);
        }
    }

    public void DamagePlayer(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
        hs.DrawHeart(health, maxHearts);
    }
    
    public void HealPlayer(float heal)
    {
        health += heal;
        if (health > maxHearts)
            health = maxHearts;
        hs.DrawHeart(health, maxHearts);
    }
}
