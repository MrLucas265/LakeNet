using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacterSystem
{
    public string CharacterName;
    public int CharacterID;
    public List<DialogueMessageSystem> Messages = new List<DialogueMessageSystem>();

    public DialogueCharacterSystem(string charactername,int characterid, List<DialogueMessageSystem> messages)
    {
        CharacterName = charactername;
        CharacterID = characterid;
        Messages = messages;
    }
}
