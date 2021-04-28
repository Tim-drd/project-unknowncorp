using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyHealtManager : MonoBehaviour
{
    public float currenthealth;
    public int maxhealth;
    
    [SerializeField] GameObject greenPotionPrefab;
    [SerializeField] GameObject bluePotionPrefab;
    [SerializeField] GameObject yellowPotionPrefab;
    [SerializeField] GameObject redPotionPrefab;
    [SerializeField] GameObject purplePotionPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtEnemy(float damageToGive)
    {
        currenthealth -= UnityEngine.Random.Range(1, Convert.ToInt32(damageToGive)+1);
        if (currenthealth <= 0)
        {
            GeneratePotion(transform.position);
            Destroy(gameObject);
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
                GameObject YellowPotion = Instantiate(yellowPotionPrefab, position, Quaternion.identity);
            }
            else if (_random < 84) // probabilité de la potion verte : 15%
            {
                GameObject GreenPotion = Instantiate(greenPotionPrefab, position, Quaternion.identity);
            }
            else if (_random < 93) // probabilité de la potion bleue : 10%
            {
                GameObject BluePotion = Instantiate(bluePotionPrefab, position, Quaternion.identity);
            }
            
            else if (_random < 98) // probabilité de la potion rouge : 5%
            {
                GameObject RedPotion = Instantiate(yellowPotionPrefab, position, Quaternion.identity);
            }
            else // probabilité de la potion violette : 2%
            {
                GameObject PurplePotion = Instantiate(purplePotionPrefab, position, Quaternion.identity);
            }
        }
    }
}
