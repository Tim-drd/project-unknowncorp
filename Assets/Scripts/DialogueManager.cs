using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public Animator animator;
    public TypeQuest q;

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Plus de DialogueManager dans la scène");
        }
        instance = this;
        sentences = new Queue<string>();
    }

    public void StartD(Dialogues dialogue, TypeQuest quest)
    {
        PlayerMovement.instance.enabled = false;
        animator.SetBool("IsOpen",true);
        dialogue.triggered_once = true;
        q = quest;
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Typing(sentence));
    }

    IEnumerator Typing(string sentence) //juste pour donner de l'effet "dialogue" avec le pnj;
    {
        dialogueText.text = "";
        foreach (var letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

    void endDialogue()
    {
        PlayerMovement.instance.enabled = true;
        animator.SetBool("IsOpen",false);
        if (q.Obj != Quest.Objectives.NONE) 
            BeginQuest();
    }
    
    public void BeginQuest()
    {
        Quest.instance.StartQ(q, animator);
    }
}