using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeQuest
{
    public string Quest_name;
    [TextArea(3, 10)]
    public string sentence;

    public MobSpawners spawners;
    public MobSpawners spawners2; //éventuellement

    public Dialogues end_sentences;

    public Dialogues neutral_dialogue; 

    public GameObject pnj;

    public GameObject pnj2;

    public GameObject pnj3;

    public Quest.Objectives Obj;

    public bool quest_over = false;

    public BoxCollider2D bc;

    public BoxCollider2D bc2;
}