using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Text nameQuestText;
    public Text QuestText;
    private string sentence = "";
    public Animator animator;
    private Objectives type;
    private int counter = 0;
    private int counter2 = 0;

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
        type = quest.Obj;
        if (type != Objectives.NONE)
        {
            nameQuestText.text = quest.Quest_name;
            QuestText.text = quest.sentence;
            animator.SetBool("BeginQ", true);
        }
        else endQuest();
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
                if (counter2 == 10)
                    endQuest();
                GameObject[] gluss = GameObject.FindGameObjectsWithTag("Gluss");
                if (gluss.Length < counter)
                {
                    counter2++;
                }
                counter = gluss.Length;
                type = Objectives.NONE;
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

    public void endQuest()
    {
        animator.SetBool("BeginQ", false);
    }
    
    
}
