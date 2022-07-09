using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueMessageSystem
{
    public string CharacterName;
    public string DialogueMessage;
    public int LineNumber;
    public string Pos;

    public DialogueMessageSystem(string charactername, string dialoguemessage,int linenumber,string pos)
    {
        CharacterName = charactername;
        DialogueMessage = dialoguemessage;
        LineNumber = linenumber;
        Pos = pos;
    }
}
