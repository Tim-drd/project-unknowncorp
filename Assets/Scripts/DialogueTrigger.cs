using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogues dialogue;
    public bool close_enough;
    private Text interactUI;
    public Transform player1;
    public Transform player2;
    public int numberOfPlayers;
    public Vector3 Pos;

    private void Awake()
    {
        interactUI = GameObject.FindWithTag("InteractUI").GetComponent<Text>();
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfPlayers > 0)
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
            if (close_enough && Input.GetKeyDown(KeyCode.I))
                BeginDialogue();
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
    
    private void OnTriggerKey(Transform target)
    {
        if (Vector3.Distance(target.position, Pos) < 1)
        { 
            close_enough = true;
        }
        else
        {
            close_enough = false;
        }

        interactUI.enabled = close_enough;
    }

    public void BeginDialogue()
    {
        DialogueManager.instance.StartD(dialogue);
    }
}
