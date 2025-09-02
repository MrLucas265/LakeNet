using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class QThread
{
    public static List<Action> MainThreadRequests = new List<Action>();

    public static void AddThreadTask(Action Task)
    {
        MainThreadRequests.Add(Task);
    }

    void Update()
    {
        while (MainThreadRequests.Count > 0)
        {
            Action CurrentFunctionToDo = MainThreadRequests[0];
            MainThreadRequests.RemoveAt(0);

            //MainThreadRequests[0]();

            //GC.Collect();
        }
    }

    public static void MakeThread(Action Task)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Task.Invoke();
            AddThreadTask(Task);
        });
    }
}
