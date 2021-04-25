using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Text nameQuestText;
    public Text QuestText;
    public Text display_counter;
    private string sentence = "";
    public Animator animator;
    private Objectives type;
    private int counter = 0;
    private int counter2 = 0;
    public AudioClip quest_opening;
    public Dialogues endQuestText;
    public Dialogues neutral;
    private Transform pnj;
    public Transform player1;
    public Transform player2;
    public int numberOfPlayers;
    private MobSpawners mobspawner;
    private TypeQuest q;
    public bool quest_over = false;

    public static Quest instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Plus de Quest dans la scène");
        }
        instance = this;
    }

    public enum Objectives
    {
        NONE,
        QUEST1,
        QUEST2,
        QUEST3,
        QUEST4,
        QUEST5
    }


    // Start is called before the first frame update
    public void StartQ(TypeQuest quest)
    {
        AudioManager.instance.PlayClip(quest_opening, transform.position);
        q = quest;
        type = quest.Obj;
        if (type == Objectives.NONE)
            quest.quest_over = true;
        nameQuestText.text = quest.Quest_name;
        QuestText.text = quest.sentence;
        endQuestText = quest.end_sentences;
        mobspawner = quest.spawners;
        neutral = quest.neutral_dialogue;
        animator.SetBool("BeginQ", true);
        pnj = quest.pnj;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case Objectives.NONE:
                return;
            case Objectives.QUEST1: //quête de Roger, le marin; 
            {
                TypeQuest t = new TypeQuest();
                t.Obj = Objectives.NONE;
                if (counter2 == 10 && isTalking(pnj) && KeyBindingManager.GetKeyDown(KeyAction.interact)
                ) //conditions necessaires a la fin de la quete 1;
                {
                    DialogueManager.instance.StartD(endQuestText, t);
                    endQuest();
                    GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
                    players[0].GetComponent<Animator>().SetInteger("weaponIndex", 2); //ne marche pas pour l'instant;
                    players[1].GetComponent<Animator>().SetInteger("weaponIndex", 2);
                    players[0].GetComponent<PlayerHealth>().HealPlayer(10);
                    players[1].GetComponent<PlayerHealth>().HealPlayer(10);
                }
                else
                {
                    if (counter != 10 && isTalking(pnj) && KeyBindingManager.GetKeyDown(KeyAction.interact) && q.Obj != Objectives.NONE)
                    {
                        DialogueManager.instance.StartD(neutral, t);
                    }
                }
                int gluss = mobspawner.enemyCounter;
                if (gluss < counter && counter2 < 10)
                {
                    counter2++;
                }
                counter = gluss;
                if (counter2 == 10)
                    display_counter.text = "Finished : 10 / 10";
                else
                    display_counter.text = counter2 + " / 10";
                return;
            }
            case Objectives.QUEST2:
                
                return;
            case Objectives.QUEST3:
                return;
            case Objectives.QUEST4:
                return;
            case Objectives.QUEST5:
                return;
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

    
    public bool isTalking(Transform charac)
    {
        if (numberOfPlayers == 1 || Vector3.Distance(player1.position, charac.position) <=
            Vector3.Distance(player2.position, charac.position))
        {
            return OnTriggerKey(player1, charac);
        }
        else
        {
            return OnTriggerKey(player2, charac);
        }
    }
    
    private bool OnTriggerKey(Transform target, Transform character)
    {
        if (numberOfPlayers > 0)
            return Vector3.Distance(target.position, character.position) < 2;
        else
        {
            return false;
        }
    }
    
    public void endQuest()
    {
        counter = 0;
        counter2 = 0;
        animator.SetBool("BeginQ", false);
        q.bc.enabled = false;
        q.Obj = Objectives.NONE;
        q.quest_over = true;
    }
}