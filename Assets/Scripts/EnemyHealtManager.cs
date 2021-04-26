using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void HurtEnemy(float damageToGive)
    {
        currenthealth -= damageToGive;
        if (currenthealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
