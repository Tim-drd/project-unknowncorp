using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusQuest : MonoBehaviour
{
    public Dialogues openingD;
    public Dialogues neutralD;
    public Dialogues endingD;
    public Text nameQuestText;
    public Text QuestText;
    public Text display_counter;
    public Animator animator;
    public Animator animator2;
    public AudioClip opening;
    public Transform player1;
    public Transform player2;
    public int numberOfPlayers;
    
    public GameObject pnj;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    private int counter = 0;

    public bool first = true;
    public bool has_talked = false;
    public bool endGame = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!endGame) 
        {
            if (first && Quest.instance.isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact)) 
            { 
                DialogueManager.instance.StartD(openingD, new TypeQuest());
                has_talked = true;
            }

            if (first && !animator2.GetBool("IsOpen") && has_talked)
            {
                first = false;
            }

            if (!first)
            {
                animator.SetBool("BeginQ", true);
                obj1.SetActive(true);
                obj2.SetActive(true);
                obj3.SetActive(true);
                obj4.SetActive(true);
                obj5.SetActive(true);
                obj6.SetActive(true);
                if (counter == 6)
                {
                    display_counter.text = "Finished";
                    if (Quest.instance.isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        endGame = true;
                        DialogueManager.instance.StartD(endingD, new TypeQuest());
                    }
                }
                else
                {
                    if (Quest.instance.isTalking(pnj.transform) && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        DialogueManager.instance.StartD(neutralD, new TypeQuest());
                    }
                    InteractAction i1 = obj1.GetComponent<InteractAction>();
                    if (i1.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        Destroy(obj1);
                        counter++;
                    }

                    InteractAction i2 = obj2.GetComponent<InteractAction>();
                    if (i2.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        Destroy(obj2);
                        counter++;
                    }

                    InteractAction i3 = obj3.GetComponent<InteractAction>();
                    if (i3.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        Destroy(obj3);
                        counter++;
                    }

                    InteractAction i4 = obj4.GetComponent<InteractAction>();
                    if (i4.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        Destroy(obj4);
                        counter++;
                    }

                    InteractAction i5 = obj5.GetComponent<InteractAction>();
                    if (i5.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        Destroy(obj5);
                        counter++;
                    }

                    InteractAction i6 = obj6.GetComponent<InteractAction>();
                    if (i6.close_enough && KeyBindingManager.GetKeyDown(KeyAction.interact))
                    {
                        Destroy(obj6);
                        counter++;
                    }
                    display_counter.text = "Fish Balls: " + counter + " / 6";
                }
            }

            nameQuestText.text = "Vrais Heros";
            QuestText.text = "Allez trouver les 6 boules et revenez voir San Fish";
        }
        else
        {
            animator.SetBool("BeginQ", false);
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
}
