﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public static PlayerHealth playerHealth;
    public float health = 10;
    public int maxHearts = 10;
    [SerializeField] HeartSystem heartSystemPrefab;
    [SerializeField] private HeartSystem hs;
    public AudioClip lowHealth;
    private bool first = true;
    private int recup_time;
    public int checkpoint_number = 0;

    private void Awake()
    {
        playerHealth = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            hs = Instantiate(heartSystemPrefab, new Vector3(510, -10, 0), Quaternion.identity);
            hs.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
            hs.DrawHeart(health, maxHearts);
        }
        checkpoint_number = LoadCheckpointSpawn.checkpointSpawn.checkpoint_number;
    }

    void LateUpdate()
    {
        Debug.Log("Checkpoint " + checkpoint_number);
        if (health <= 0)
        {
            respawn();
        }
        if (health < 2.5 && first)
        {
            first = false;
            AudioManager.instance.PlayClip(lowHealth, this.transform.position);
        }
        else
        {
            if (recup_time % 10000 == 0)
            {
                recup_time %= 10000;
                first = true;
            }

            recup_time++;
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

    public void respawn()
    {
        Checkpoint.instance.spawnTo(checkpoint_number, this.gameObject);
        health = 10;
        hs.DrawHeart(10, maxHearts);
    }
}
