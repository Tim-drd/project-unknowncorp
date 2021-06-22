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
    private bool finished = false;
    private GameObject[] pnj;


    private void Awake()
    {
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfPlayers > 0)
        {
            which_player_trigger();
            if (close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact) && !dialogue.triggered_once && !quest.quest_over)
            {
                if (quest.Obj != Quest.Objectives.PVP || GameObject.FindGameObjectsWithTag("PlayerClone").Length > 1)
                    BeginDialogue();
            }
            else
            {
                if (quest.quest_over)
                {
                    if (close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact) && finished)
                    {
                        dialogue = quest.neutral_dialogue;
                        BeginDialogue();
                    }

                    finished = true;
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

    public Transform which_player_trigger()
    {
        if (numberOfPlayers == 1 || Vector3.Distance(player1.position, Pos) <=
            Vector3.Distance(player2.position, Pos))
        {
            OnTriggerKey(player1);
            return player1;
        }
        else
        { 
            OnTriggerKey(player2);
            return player2;
        }
    }
    
    private void OnTriggerKey(Transform target)
    {
        close_enough = Vector3.Distance(target.position, Pos) < 2;
    }

    public void BeginDialogue()
    {
        DialogueManager.instance.StartD(dialogue, quest);
    }


    public void setQuestOver(TypeQuest tq)
    {
        tq.quest_over = true;
    }
}