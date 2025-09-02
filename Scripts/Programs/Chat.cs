//using System;
//using UnityEngine;
//using System.Collections.Generic;
//using UnityEngine.Networking;

//public class Chat : NetworkBehaviour {

//    //Chat History
//    public List<string> chatHistory = new List<string>();

//    //Keeps track of current message
//    private string currentMessage = String.Empty;

//    void SendMessage() {
//        if (string.IsNullOrEmpty(currentMessage.Trim())) return;
//        {
//            CmdChatMessage(currentMessage);
//            currentMessage = "";
//        }
//    }

//    private void BottomChat() {
//        currentMessage = GUI.TextField(new Rect(0, Screen.height - 20, 175, 20), currentMessage);
//        if (GUI.Button(new Rect(200, Screen.height - 20, 75, 20), "Send"))
//        {
//            SendMessage();
//        }
//        GUILayout.Space(15);
//        for (int i = chatHistory.Count - 1; i >= 0; i--)
//            GUILayout.Label(chatHistory[i]);
//    }

//    private void TopChat() {
//        GUILayout.Space(15);
//        GUILayout.BeginHorizontal(GUILayout.Width(250));
//        currentMessage = GUILayout.TextField(currentMessage);
//        if (GUILayout.Button("Send"))
//        {
//            SendMessage();
//        }
//        GUILayout.EndHorizontal();
//        foreach (string c in chatHistory)
//            GUILayout.Label(c);
//    }


//    //Chatbox
//    private void OnGUI() {
//        BottomChat();
//        //TopChat();
//    }

//    [Command]
//    void CmdChatMessage(string message)
//    {
//        RpcReceiveChatMessage(message);
//        chatHistory.Add(message);
//        CmdChatMessage(message);
//    }

//    [ClientRpc]
//    void RpcReceiveChatMessage(string message)
//    {
//        chatHistory.Add(message);
//    }
//}
