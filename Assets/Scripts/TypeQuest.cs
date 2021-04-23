using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeQuest
{
    public string Quest_name;
    [TextArea(3, 10)]
    public string sentence;

    public Quest.Objectives Obj;
}
