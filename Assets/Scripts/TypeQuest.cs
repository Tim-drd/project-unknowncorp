using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeQuest
{
    public string Quest_name;
    [TextArea(3, 10)]
    public string sentence;

    public MobSpawners spawners; //éventuellement

    public Dialogues end_sentences;

    public Dialogues neutral_dialogue; 

    public Transform pnj;

    public Quest.Objectives Obj;

    public bool quest_over = false;
}