using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class QThreadBackup 
{
    public static List<Action> MainThreadRequests = new List<Action>();

    public static Thread NewThread;

    public static void AddThreadTask(Action Task)
    {
        MainThreadRequests.Add(Task);
    }

    void Update()
    {
        while (MainThreadRequests.Count > 0)
        {
            Action CurrentFunctionToDo = MainThreadRequests[0];
            NewThread.Abort(MainThreadRequests);
            MainThreadRequests.RemoveAt(0);

            CurrentFunctionToDo();
        }
    }

    public static void MakeThread(Action Task)
    {
        NewThread = new Thread(new ThreadStart(Task));
        NewThread.Start();
        AddThreadTask(Task);
    }
}
