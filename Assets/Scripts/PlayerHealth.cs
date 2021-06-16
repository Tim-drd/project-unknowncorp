using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10;
    public int maxHearts = 10;
    [SerializeField] HeartSystem heartSystemPrefab;
    [SerializeField] private HeartSystem hs;
    public AudioClip lowHealth;
    private bool first = true;
    private int recup_time;

    public PhotonView photonView;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            
            /* hs = Instantiate(heartSystemPrefab, new Vector3(510, -10, 0), Quaternion.identity);
            hs.tag = "HeartContainer" + i;
            hs.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);*/
            hs = transform.Find("Canvas").Find("HeartContainer").GetComponent<HeartSystem>();
            hs.DrawHeart(health, maxHearts);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    { 
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
        if (photonView.IsMine)
            hs.DrawHeart(health, maxHearts);
    }
    
    public void HealPlayer(float heal)
    {
        health += heal;
        if (health > maxHearts)
            health = maxHearts;
        if (photonView.IsMine)
            hs.DrawHeart(health, maxHearts);
    }
}
