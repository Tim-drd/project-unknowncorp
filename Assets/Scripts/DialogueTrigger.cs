using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogues dialogue;
    public TypeQuest quest;
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
            which_player_trigger();
            if (close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact) && !dialogue.triggered_once)
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
        interactUI.enabled = close_enough;
    }

    public void BeginDialogue()
    {
        DialogueManager.instance.StartD(dialogue, quest);
    }
}
