using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public List<EventSystem> Event = new List<EventSystem>();
    // Use this for initialization
    void Start ()
    {
        Event.Add(new EventSystem("Moon Landing", "This is the day the first man planted hes feet on the moon", 20, 7, 1969));
	}
}
