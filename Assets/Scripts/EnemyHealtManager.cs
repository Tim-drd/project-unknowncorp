using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class EnemyHealtManager : MonoBehaviour
{
    public float currenthealth;
    public int maxhealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrentHealth()
    {
        return this.currenthealth;
    }
    
    public void HurtEnemy(float damageToGive)
    {
        currenthealth -= UnityEngine.Random.Range(1, Convert.ToInt32(damageToGive)+1);
        if (currenthealth <= 0)
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                GeneratePotion(transform.position);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void GeneratePotion(Vector3 position)
    {
        var rand = new Random();
        int _random = rand.Next(100);
        if (_random > 59) // probabilité d'aucune potion : 60%
        {
            if (_random < 68) // probabilité de la potion jaune : 8%
            {
                PhotonNetwork.Instantiate("Yellow Potion", position, Quaternion.identity);
            }
            else if (_random < 84) // probabilité de la potion bleue : 15%
            {
                PhotonNetwork.Instantiate("Blue Potion", position, Quaternion.identity);
            }
            else if (_random < 93) // probabilité de la potion verte : 10%
            {
                PhotonNetwork.Instantiate("Green Potion", position, Quaternion.identity);
            }
            
            else if (_random < 98) // probabilité de la potion rouge : 5%
            {
                PhotonNetwork.Instantiate("Red Potion", position, Quaternion.identity);
            }
            else // probabilité de la potion violette : 2%
            {
                PhotonNetwork.Instantiate("Purple Potion", position, Quaternion.identity);
            }
        }
    }
}
