using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractAction : MonoBehaviour
{
    private Text interactUI;
    public bool close_enough;
    public Transform player1;
    public Transform player2;
    public int numberOfPlayers;
    public Vector3 Pos;
    private bool finished = false;
    private bool one_second = true;

    
    // Start is called before the first frame update
    void Start()
    {
        interactUI = GameObject.FindWithTag("InteractUI").GetComponent<Text>();
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        interactUI.text = "Appuyer sur " + KeyBindingManager.GetKeyCode(KeyAction.interact);
        if (numberOfPlayers > 0)
        {
            which_player_trigger();
            if (close_enough && this.gameObject.activeSelf)
            {
                one_second = true;
                interactUI.enabled = true;
            }
            else
            {
                if (one_second)
                {
                    interactUI.enabled = false;
                    one_second = false;
                }
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
    
    public void which_player_trigger()
    {
        if (numberOfPlayers == 1 || Vector3.Distance(player1.position, Pos) <=
            Vector3.Distance(player2.position, Pos))
        {
            OnTriggerKey(player1);
        }
        else
        { 
            OnTriggerKey(player2);
        }
    }
    
    private void OnTriggerKey(Transform target)
    {
        close_enough = Vector3.Distance(target.position, Pos) < 2;
    }
}
