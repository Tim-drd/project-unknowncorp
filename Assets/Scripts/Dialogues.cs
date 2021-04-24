using UnityEngine;

[System.Serializable]
public class Dialogues
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
    public bool triggered_once = false;
}