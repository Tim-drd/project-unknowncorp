﻿using System.Collections;
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
    public Animator animator2;
    private Objectives type;
    private int counter = 0;
    private int counter2 = 0;
    private int counter3 = 0;
    private int counter4 = 0;
    private int counter5 = 0;
    private int counter6 = 0;
    public AudioClip quest_opening;
    public AudioClip upgrade_trident;
    public Dialogues endQuestText;
    public Dialogues neutral;
    private GameObject pnj;
    private GameObject pnj2;
    private GameObject pnj3;
    private GameObject pnj4;
    private GameObject pnj5;
    private GameObject pnj6;
    private GameObject pnj7;
    public Transform player1;
    public Transform player2;
    public int numberOfPlayers;
    private MobSpawners mobspawner;
    private MobSpawners mobspawner2;
    private TypeQuest q;
    private bool spoken = false;
    private bool first = true;
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
    public void StartQ(TypeQuest quest, Animator anim2)
    {
        if (numberOfPlayers > 0)
        {
            AudioManager.instance.PlayClip(quest_opening, player1.transform.position);
        }
        if (numberOfPlayers > 1)
        {
            AudioManager.instance.PlayClip(quest_opening, player2.transform.position);
        }
        
        q = quest;
        if (q.Obj == Objectives.QUEST2 || q.Obj == Objectives.QUEST3 || q.Obj == Objectives.QUEST4)
        {
            q.bc.enabled = false;
        }

        if (q.Obj == Objectives.QUEST5)
        {
            pnj2.SetActive(true);
            pnj3.SetActive(true);
            pnj4.SetActive(true);
            pnj5.SetActive(true);
            pnj6.SetActive(true);
            pnj7.SetActive(true);
        }
        type = quest.Obj;
        if (type == Objectives.NONE)
            quest.quest_over = true;
        nameQuestText.text = quest.Quest_name;
        QuestText.text = quest.sentence;
        endQuestText = quest.end_sentences;
        mobspawner = quest.spawners;
        mobspawner2 = quest.spawners2;
        neutral = quest.neutral_dialogue;
        animator.SetBool("BeginQ", true);
        animator2 = anim2;
        pnj = quest.pnj;
        pnj2 = quest.pnj2;
        pnj3 = quest.pnj3;
        pnj4 = quest.pnj4;
        pnj5 = quest.pnj5;
        pnj6 = quest.pnj6;
        pnj7 = quest.pnj7;
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
                if (counter2 == 10 && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact)
                ) //conditions necessaires a la fin de la quete 1;
                {
                    q.bc.enabled = false;
                    DialogueManager.instance.StartD(endQuestText, t);
                    mobspawner.enemyMaxCount = 2;
                    endQuest();
                    GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
                    foreach (var player in players)
                    {
                        player.GetComponent<PlayerHealth>().checkpoint_number = 1;
                        player.GetComponent<Animator>().SetInteger("weaponIndex", 1);
                        player.GetComponent<PlayerHealth>().HealPlayer(10);
                    }
                    if (numberOfPlayers > 0)
                    {
                        AudioManager.instance.PlayClip(upgrade_trident, player1.transform.position);
                    }
                    if (numberOfPlayers > 1)
                    {
                        AudioManager.instance.PlayClip(upgrade_trident, player2.transform.position);
                    }
                    
                }
                else
                {
                    if (counter2 != 10 && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact) && q.Obj != Objectives.NONE)
                    {
                        DialogueManager.instance.StartD(neutral, t);
                    }
                }
                int gluss = mobspawner.enemyCounter;
                if (gluss < counter && counter2 < 10)
                {
                    counter2 += counter - gluss;
                }
                counter = gluss;
                if (counter2 == 10)
                    display_counter.text = "Finished : 10 / 10";
                else
                    display_counter.text = counter2 + " / 10";
                return;
            }
            case Objectives.QUEST2:
                {
                    TypeQuest t = new TypeQuest();
                    t.Obj = Objectives.NONE;
                    if (counter4 >= 3 && counter6 >= 4 && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact)) //conditions necessaires a la fin de la quete 1;
                    { //conditions necessaires à la fin de la quete 2;
                        DialogueManager.instance.StartD(endQuestText, t);
                        endQuest();
                        q.bc2.enabled = false;
                        mobspawner.enemyMaxCount = 2;
                        mobspawner2.enemyMaxCount = 2;
                        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
                        foreach (var player in players)
                        {
                            player.GetComponent<PlayerHealth>().checkpoint_number = 2;
                            player.GetComponent<Animator>().SetInteger("weaponIndex", 2);
                            player.GetComponent<PlayerHealth>().HealPlayer(10);
                        }
                        if (numberOfPlayers > 0)
                        {
                            AudioManager.instance.PlayClip(upgrade_trident, player1.transform.position);
                        }
                        if (numberOfPlayers > 1)
                        {
                            AudioManager.instance.PlayClip(upgrade_trident, player2.transform.position);
                        }
                    }
                    else
                    {
                        if ((counter4 != 3 || counter6 != 4) && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact) && q.Obj != Objectives.NONE)
                        {
                            DialogueManager.instance.StartD(neutral, t);
                        }
                    }
                    int devorror = mobspawner.enemyCounter;
                    if (devorror < counter3 && counter4 < 3)
                    {
                        counter4 += counter3 - devorror;
                    }
                    counter3 = devorror;

                    int terrus = mobspawner2.enemyCounter;
                    if (terrus < counter5 && counter6 < 4)
                    {
                        counter6+= counter5 - terrus;
                    }
                    counter5 = terrus;

                    if (counter4 >= 3 && counter6 >= 4)
                        display_counter.text = "Finished";
                    else
                        display_counter.text = "Devorror: " + counter4 + " / 3" + " Terrus: " + counter6 + " / 4";
                    return;
                }
            case Objectives.QUEST3:
            {
                display_counter.text = "";
                TypeQuest t = new TypeQuest();
                t.Obj = Objectives.NONE;
                if (spoken && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    q.bc2.enabled = false;
                    DialogueManager.instance.StartD(endQuestText, t);
                    endQuest();
                    GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
                    foreach (var player in players)
                    {
                        player.GetComponent<PlayerHealth>().checkpoint_number = 3;
                        player.GetComponent<Animator>().SetInteger("weaponIndex", 3);
                        player.GetComponent<PlayerHealth>().HealPlayer(10);
                    }
                    if (numberOfPlayers > 0)
                    {
                        AudioManager.instance.PlayClip(upgrade_trident, player1.transform.position);
                    }
                    if (numberOfPlayers > 1)
                    {
                        AudioManager.instance.PlayClip(upgrade_trident, player2.transform.position);
                    }
                }
                else
                {
                    if (first && spoken && !animator2.GetBool("IsOpen"))
                    {
                        VisualEffects.fadeOut = true;
                        pnj2.SetActive(false);
                        pnj3.SetActive(true);
                        first = false;
                    }
                    if (!spoken)
                        spoken = isTalking(pnj2.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact);
                    if (!spoken && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact) && q.Obj != Objectives.NONE)
                    {
                        DialogueManager.instance.StartD(neutral, t);
                    }
                }
                if (spoken)
                    QuestText.text = "Retournez au village";
                return;
            }
            case Objectives.QUEST4:
            {
                TypeQuest t = new TypeQuest();
                t.Obj = Objectives.NONE;
                bool over = pnj2.GetComponent<Boss>().is_dead();
                if (over) ;
                {
                    endQuest();
                    GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerClone");
                    foreach (var player in players)
                    {
                        player.GetComponent<PlayerHealth>().checkpoint_number = 4;
                        player.GetComponent<PlayerHealth>().HealPlayer(10);
                    }
                    pnj3.SetActive(false);
                    pnj4.SetActive(true);
                    pnj5.SetActive(true);
                    //y faudra faire d'autre choses ici comme lancer des crédits etc;
                }
                if (isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact))
                { 
                    DialogueManager.instance.StartD(neutral, t); 
                }
                return;
            }
            case Objectives.QUEST5:
            {
                TypeQuest t = new TypeQuest();
                t.Obj = Objectives.NONE;
                counter = 0;
                if (counter == 6 && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    DialogueManager.instance.StartD(endQuestText, t);
                    endQuest();
                }
                else
                {
                    if (counter < 6 && isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        DialogueManager.instance.StartD(neutral, t); 
                    }

                    if (counter == 6)
                        display_counter.text = "Finished";
                    else
                    {
                        display_counter.text = "Boules de cristal: " + counter + "/6";
                    }
                }

                InteractAction i1 = pnj2.GetComponent<InteractAction>();
                if (i1.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    Destroy(pnj2);
                    counter++;
                }
                InteractAction i2 = pnj3.GetComponent<InteractAction>();
                if (i2.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    Destroy(pnj3);
                    counter++;
                }
                InteractAction i3 = pnj4.GetComponent<InteractAction>();
                if (i3.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    Destroy(pnj4);
                    counter++;
                }
                InteractAction i4 = pnj5.GetComponent<InteractAction>();
                if (i4.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    Destroy(pnj5);
                    counter++;
                }
                InteractAction i5 = pnj6.GetComponent<InteractAction>();
                if (i5.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    Destroy(pnj6);
                    counter++;
                }
                InteractAction i6 = pnj7.GetComponent<InteractAction>();
                if (i6.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                {
                    Destroy(pnj7);
                    counter++;
                }
                return;
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
        animator.SetBool("BeginQ", false);
        display_counter.text = "";
        counter = 0;
        counter2 = 0;
        counter3 = 0;
        counter4 = 0;
        counter5 = 0;
        counter6 = 0;
        q.Obj = Objectives.NONE;
        q.quest_over = true;
    }
}