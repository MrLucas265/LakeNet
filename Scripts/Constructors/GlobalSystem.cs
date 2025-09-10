using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalSystem 
{
	public DateSystem DateTime;
    public AutosaveSystem Autosave;
    public float Timer;
    public bool DevMode;

    public GlobalSystem()
    {

    }
}