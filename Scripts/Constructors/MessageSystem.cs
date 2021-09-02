using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageSystem
{
    public string SentBy;
    public string Message;
    public int TimeSent;
    public int MessageState;
    public bool Read;

    public MessageSystem(string sentby,string message,int timesent,int messagestate,bool read)
    {
        SentBy = sentby;
        Message = message;
        TimeSent = timesent;
        MessageState = messagestate;
        Read = read;
    }
}