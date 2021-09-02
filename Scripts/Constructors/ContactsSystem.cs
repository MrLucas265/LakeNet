using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContactsSystem
{
    public string UserName;
    public string Alias;
    public int PID;

    public ContactsSystem(string username, string alias, int pid)
    {
        UserName = username;
        Alias = alias;
        PID = pid;
    }
}