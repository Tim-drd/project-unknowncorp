using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infirmerie : MonoBehaviour
{
    public InteractAction i;
    public Transform t;
    private int numberOfPlayers;
    private Transform player1;
    private Transform player2;
    
    // Update is called once per frame
    void Update()
    {
        if (i.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
        {
            if (numberOfPlayers == 2)
            {
                if (Vector3.Distance(player1.position, t.position) < Vector3.Distance(player2.position, t.position)) 
                { 
                    player1.GetComponent<PlayerHealth>().HealPlayer(10); 
                }
                else 
                { 
                    player2.GetComponent<PlayerHealth>().HealPlayer(10); 
                }
            }
            else
            {
                player1.GetComponent<PlayerHealth>().HealPlayer(10); 
            }
        }
    }
    
    void LateUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
        numberOfPlayers = players.Length;
        if (numberOfPlayers > 0)
        {
            player1 = players[0].transform;
            if (numberOfPlayers > 1)
            {
                player2 = players[1].transform;
            }
        }
    }
    
    
}
