using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FileMangementSystem
{
    public string CurrentDirectory;
    public string TypedDirectory;
    public List<ProgramSystemv2> PageFile = new List<ProgramSystemv2>();
    public int SelectedFile;
    public List<string> History = new List<string>();
    public int HistoryPosition;
    public List<string> BackList = new List<string>();


    public FileMangementSystem()
    {
        CurrentDirectory = "Gateway";
        TypedDirectory = "Gateway";
    }
}