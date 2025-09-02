using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static UnityMainThreadDispatcher instance = null;
    private static readonly Queue<System.Action> executionQueue = new Queue<System.Action>();

    public static UnityMainThreadDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UnityMainThreadDispatcher>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("UnityMainThreadDispatcher");
                    instance = obj.AddComponent<UnityMainThreadDispatcher>();
                }
            }

            return instance;
        }
    }

    private void Update()
    {
        lock (executionQueue)
        {
            while (executionQueue.Count > 0)
            {
                System.Action action = executionQueue.Dequeue();
                action.Invoke();
            }
        }
    }

    public static void Enqueue(System.Action action)
    {
        lock (executionQueue)
        {
            executionQueue.Enqueue(action);
        }
    }
}
